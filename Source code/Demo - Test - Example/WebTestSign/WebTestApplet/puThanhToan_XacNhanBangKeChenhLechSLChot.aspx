<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="puThanhToan_XacNhanBangKeChenhLechSLChot.aspx.cs"
    Inherits="WebTTD.UserControls.Popup.puThanhToan_XacNhanBangKeChenhLechSLChot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="vnptSignatureFunctions.js"></script>
    <script src="vnptSignatureUtils.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <div style="text-align: right;">
            <asp:Button ID="btnOK" runat="server" CssClass="buttonStyle" Text="Chấp nhận" Width="100px"
                OnClick="btnOK_Click" />
            </div>

            <div style="display: block" runat="server" id="divDigSig">
                <asp:TextBox ID="txtClientTime" runat="server" CssClass="essign_txtClientTime"></asp:TextBox>
                <asp:TextBox ID="txtBase64" runat="server" CssClass="essign_txtBase64" TextMode="MultiLine" Text=""></asp:TextBox>
                <asp:Button ID="btnUpload" runat="server" CssClass="essign_btnUpload" Text="Upload" OnClick="btnUpload_Click" />
            </div>
            <div>
                <asp:Label ID="lblErr" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
