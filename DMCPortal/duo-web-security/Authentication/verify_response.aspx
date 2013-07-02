<%@ Page Language="C#" AutoEventWireup="true" CodeFile="verify_response.aspx.cs" Inherits="Authentication_verify_response" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        result = "<%=ok%>"
    if (result==false){
        window.location = "Default.aspx";
    }else{
    window.location = "/Authentication/AuthenticatedContent.aspx?username=" + "<%=username %>";
    }
    </script>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
