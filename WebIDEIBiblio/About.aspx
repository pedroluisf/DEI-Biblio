<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="WebIDEIBiblio.About" %>

<asp:Content ID="Content 1" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content 2" runat="server" ContentPlaceHolderID="MainContent">
    <div class="title" style="width:490px"><span class="title_icon"><img src="images/bullet1.gif" alt="" title="" /></span><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></div>
    <asp:Label ID="lblTexto" runat="server" Text="Label"></asp:Label>
</asp:Content>
