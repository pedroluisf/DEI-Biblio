<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebIDEIBiblio._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="title" style="width:490px"><span class="title_icon"><img src="images/bullet1.gif" alt="" title="" /></span><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></div>
    <asp:PlaceHolder ID="plCustomFront" runat="server"></asp:PlaceHolder>
</asp:Content>
