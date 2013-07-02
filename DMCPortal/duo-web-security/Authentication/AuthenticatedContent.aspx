<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthenticatedContent.aspx.cs" Inherits="Authenticated_AuthenticatedContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
    client="<%=client%>";
    </script>
    <form id="form1" runat="server">
    <div>
    You have been Authenticated from 
    <script type="text/javascript">
        document.write(':' + client);
    </script>

    </div>
    </form>
</body>
</html>
