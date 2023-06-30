<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebTestSign._Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>MY REPORT:</h2>

    <br />
    
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
      <script type="text/jscript">
          function clientClose(oWnd) {
              var arg = oWnd.argument;
              //alert(arg);
              if (arg == "YES") {

              }
          }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindowManager ID="RadWindowManager1" RestrictionZoneID="body_html" runat="server"
        Style="z-index: 90000;" Skin="Default">
    </telerik:RadWindowManager>

    <asp:Label ID="Label1" runat="server" Text="File on server: "></asp:Label>

    <asp:TextBox ID="txtPath" runat="server">74</asp:TextBox>

    <asp:Button ID="btnReady" runat="server" Text="Ký văn bản" OnClientClick="fillClientTime();" OnClick="btnReady_Click" />

<%--    <asp:Button ID="Button1" runat="server" Text="Popup" OnClick="btnPopup_Click" />

    <asp:Button ID="Button2" runat="server" Text="DownFile" OnClick="Button2_Click" />--%>

    <br />
    <br />
    <asp:Label ID="lblOutput" runat="server" Text="Output" ForeColor="Red"></asp:Label>

    <br />
    <asp:GridView ID="grvListFile" runat="server" BorderStyle="Solid" BorderWidth="1px" CellPadding="5">
        <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>

    <script>
        function OnClientFileSelected(sender, args) {
            //get the file from the input field
            var file = args.get_fileInputField().files[0];

            var reader = new FileReader();
            reader.onload = function () {
                var dataURL = reader.result;
                var base64 = dataURL.split(";")[1].split(",")[1];
                //alert(base64);
                document.getElementsByClassName("essign_txtBase64")[0].value = base64;
            };
            reader.readAsDataURL(file);
        }
    </script>
    <telerik:RadUpload ID="ruFile" runat="server" OnClientFileSelected="OnClientFileSelected"/>    
    
    </div>
    <%--=== Thêm đoạn code này vào để ký qua windows app ===--%>
    <div style="display: block" runat="server" id="divDigSig">
        <asp:TextBox ID="txtClientTime" runat="server" CssClass="essign_txtClientTime"></asp:TextBox>
        <asp:TextBox ID="txtBase64" runat="server" CssClass="essign_txtBase64" TextMode="MultiLine" Text=""></asp:TextBox>
        <asp:Button ID="btnUpload" runat="server" CssClass="essign_btnUpload" Text="Upload" OnClick="btnUpload_Click" />
        <a id="aSign" runat="server" Class="essign_btnSign" href="javascript:void(0);">Sign</a>
    </div>
    <%--=== Thêm đoạn code này vào để ký qua windows app ===--%>
</asp:Content>
