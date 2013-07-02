using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using DMCIdentityManagement;
public partial class _Default : System.Web.UI.Page
{
    public String iKey;
    public String aKey;
    public String sKey;
    public String hostname;
    public String username;
    public String client;
    public String sig_request;
    protected void Page_Load(object sender, EventArgs e)
    {
        DMCIndentityManagement id = new DMCIndentityManagement();
        iKey = id.iKey;
        aKey = id.aKey;
        sKey = id.sKey;
        hostname = id.hostname;
        username = Page.User.Identity.Name;
        client = GetCompany(username);
        sig_request = Duo.Web.SignRequest(iKey, sKey, aKey, username);
    }
    public string GetCompany(string username)
    {
        string result = string.Empty;

        // if you do repeated domain access, you might want to do this *once* outside this method, 
        // and pass it in as a second parameter!
        PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

        // find the user
        UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, username);

        // if user is found
        if (user != null)
        {
            // get DirectoryEntry underlying it
            DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);

            if (de != null)
            {
                if (de.Properties.Contains("Company"))
                {
                    result = de.Properties["Company"][0].ToString();
                }
            }
        }

        return result;
    }
}