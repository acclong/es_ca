<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebTestApplet._Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <h2>TEST SIGN BY JAVA APPLET</h2>

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

    <asp:Button ID="Button1" runat="server" Text="Popup" OnClick="btnPopup_Click" />

    <asp:Button ID="Button2" runat="server" Text="DownFile" OnClick="Button2_Click" />

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

    <br />
    <br />
    <%--=== Thêm đoạn code này vào để ký applet ===--%>
    <div style="display: block" runat="server" id="divDigSig">
        <asp:TextBox ID="txtClientTime" runat="server" CssClass="essign_txtClientTime"></asp:TextBox>
        <asp:TextBox ID="txtBase64" runat="server" CssClass="essign_txtBase64" TextMode="MultiLine" Text=""></asp:TextBox>
        <asp:Button ID="btnUpload" runat="server" CssClass="essign_btnUpload" Text="Upload" OnClick="btnUpload_Click" />

<%--        <applet archive="VnptTokenApplet.jar"
            name="VNPTCA Token Applet" id="vnptTokenApplet"
            code="com.vnpt.VnptTokenApplet.class" height="0" width="0"> 
			<param name="separate_jvm" value="true" /> 
			<param name="dll" value="vnpt-ca_csp11.dll,VNPT-CA_v34.dll,vnptca_p11_v6.dll,vnpt-ca_cl_v1.dll,vnptcamobile.dll,gclib.dll,viettel-ca_v4.dll" />	
			<a href="http://java.sun.com/webapps/getjava/BrowserRedirect?host=java.com" target="_blank">JRE Download</a><br/> 
		</applet>--%>
    </div>
    <%--=== Thêm đoạn code này vào để ký applet ===--%>
</asp:Content>
