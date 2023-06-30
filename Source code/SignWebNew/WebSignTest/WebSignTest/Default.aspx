<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebSignTest._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link href="CSS/Table.css" rel="stylesheet" />
    <link href="CSS/Label.css" rel="stylesheet" />
    <script src="Scripts/esSignatureFunctions.js"></script>
    
    <div>
        <asp:Label ID="lblErr" runat="server" CssClass="LabelError"></asp:Label>
        <h2>Test ký nhé!!!</h2>
        <asp:Button ID="btnSign" runat="server" Text="Ký nào" Width="100px" OnClick="btnSign_Click" OnClientClick="fillClientTime();" />
        <asp:GridView ID="grvFile" runat="server">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="SelectedFile" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <asp:Button ID="btnSignBKN" runat="server" Text="Ký nào" Width="100px" OnClick="btnSignBKN_Click" OnClientClick="fillClientTime();" />
        <asp:Calendar ID="Ngay" runat="server" Width="20px" Height="8px"></asp:Calendar>
        <asp:TextBox ID="txtMa_NM" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtFileID" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btnSignDKCA" runat="server" Text="Ký nào" Width="100px" OnClick="btnSignDKCA_Click" OnClientClick="fillClientTime();" />
        <asp:Label ID="lblContent" runat="server">Để chơi Pokemon GO, game thủ sẽ phải cầm chiếc smartphone chạy ra ngoài đường và đi khắp nơi để bắt được những chú Pokemon vốn đã được đặt vào trong thế giới thực dựa vào các thuật toán về địa điểm nơi chốn.</asp:Label>
    </div>

    <div>
        <asp:Button ID="btnSignULSK" runat="server" Text="Ký nào" Width="100px" OnClick="btnSignULSK_Click" OnClientClick="fillClientTime();" />
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
    <div style="visibility: hidden; display: block; height: 0px;" runat="server" id="divDigSig">
        <a id="aSign" runat="server" class="essign_btnSign" href="javascript:void(0);">Sign</a>
        <asp:TextBox ID="txtClientTime" runat="server" CssClass="essign_txtClientTime"></asp:TextBox>
        <%--<asp:TextBox ID="txtBase64" runat="server" CssClass="essign_txtBase64" TextMode="MultiLine" Text=""></asp:TextBox>--%>
        <%--<asp:Button ID="btnUpload" runat="server" CssClass="essign_btnUpload" Text="Upload" OnClick="btnUpload_Click" />--%>
    </div>
</asp:Content>

