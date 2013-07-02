using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Authenticated_AuthenticatedContent : System.Web.UI.Page
{
    public string client;
    protected void Page_Load(object sender, EventArgs e)
    {
        client = Request.QueryString["username"];
    }

}