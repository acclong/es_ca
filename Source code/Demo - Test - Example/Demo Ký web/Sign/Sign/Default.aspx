<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Sign._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:TextBox ID="txtDataSigned" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Link path file"></asp:Label>

    <asp:TextBox ID="txtPath" runat="server"></asp:TextBox>

    <asp:Button ID="btnDownload" runat="server" Text="Download" onclick="btnDownload_Click" />

    <asp:Button ID="btnSign" runat="server" Text="Sign" onclick="btnSign_Click" />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Serial Number"></asp:Label>

    <asp:TextBox ID="txtSerialNumber" runat="server"></asp:TextBox>
</asp:Content>
