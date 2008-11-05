<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebApp.aspx.cs" Inherits="WebApp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Database Webpage</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="btn_toggleDB" runat="server" Text="Toggle DB:" 
            onclick="btn_toggleDB_Click" />&nbsp;
        <asp:Label ID="lbl_currentDB" runat="server" Text="Local"></asp:Label>
    
    </div>
    <p>
        Name:<br />
        <asp:TextBox ID="txt_name" runat="server" Width="85%" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        Phone:<br />
        <asp:TextBox ID="txt_phone" runat="server" Width="85%" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        Room:<br />
        <asp:TextBox ID="txt_room" runat="server" Width="85%" TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btn_search" runat="server" Text="search" 
            onclick="btn_search_Click" />&nbsp;
        <asp:Button ID="btn_enter" runat="server" Text="enter" 
            onclick="btn_enter_Click" />&nbsp;
        <asp:Button ID="btn_remove" runat="server" Text="remove" 
            onclick="btn_remove_Click" />&nbsp;
        <asp:Label ID="lbl_recordCount" runat="server" Text="0"></asp:Label>
    </p>
    </form>
</body>
</html>
