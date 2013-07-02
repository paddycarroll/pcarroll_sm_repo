<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <script type= "text/javascript" src="/js/Duo-Web-v1.bundled.js"></script>
    <script type= "text/javascript" src="/js/Duo-Web-v1.js"></script>

<head>
<!--<head id="Head1" runat="server">-->

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Welcome to Deluxe Media Cloud</div>
    </form>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            You are not logged in
        </AnonymousTemplate>
    </asp:LoginView>
    <script type= "text/javascript">
    hostname="<%=hostname%>";
    sig_request="<%=sig_request%>";
    client = "<%=client%>";
    post_action = "Authentication/verify_response.aspx?client=" + client;
    Duo.init({
            'host': hostname,
            'sig_request': sig_request,
            'post_action': post_action
        });
    </script>
    <iframe id="duo_iframe" width="100%" height="500" frameborder="0"></iframe>
</body>
</html>
