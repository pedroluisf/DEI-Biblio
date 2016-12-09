<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="WebIDEIBiblio.Categorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title" style="width:490px"><span class="title_icon"><img src="images/bullet1.gif" alt="" title="" /></span><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></div>
    <asp:Label ID="lblTexto" runat="server" Text="Label"></asp:Label>
    <asp:PlaceHolder ID="plLstCategorias" runat="server"></asp:PlaceHolder>
</asp:Content>
