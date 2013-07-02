using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMCIdentityManagement;
public partial class Authentication_verify_response : System.Web.UI.Page
{
    public String iKey;
    public String aKey;
    public String sKey;
    public String hostname;
    public String username;
    public bool ok;

    protected void Page_Load(object sender, EventArgs e)
    {
        NameValueCollection nvc = Request.Form;
        if (!string.IsNullOrEmpty(nvc["sig_response"]))
        {
            DMCIndentityManagement id = new DMCIndentityManagement();
            iKey = id.iKey;
            aKey = id.aKey;
            sKey = id.sKey;
            hostname = id.hostname;
            username = Duo.Web.VerifyResponse(iKey,sKey,aKey,nvc["sig_response"]);
            if (username == "")
            {
                // bail out
                ok = false;
            }
            else
            {
                // Auth ok
                ok = true;
            }
        }
    }
}