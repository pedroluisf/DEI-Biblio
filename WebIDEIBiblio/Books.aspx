<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="WebIDEIBiblio.Books" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title"><span class="title_icon">
    <img src="/images/bullet2.gif" alt="" title="" border="0" /></span>Pesquisar Artigos
    </div>             	   
	<div class="new_products">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtPesquisa" 
            ErrorMessage="Preencha uma expressão a pesquisar:"></asp:RequiredFieldValidator>
            <br />
        <asp:TextBox ID="txtPesquisa" runat="server" Width="360px"></asp:TextBox>&nbsp;&nbsp;
        <asp:ImageButton src="/Images/pesquisar.gif" ID="btnPesquisa" runat="server" onclick="btnPesquisa_Click"/>
	</div> 
    <div class="title" style="width:490px"><span class="title_icon"><img src="images/bullet1.gif" alt="" title="" /></span><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></div>
    <asp:Label ID="lblTexto" runat="server" Text=""></asp:Label>    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" 
        onpageindexchanging="GridView1_PageIndexChanging" PageSize="5">
        <Columns>
            <asp:ImageField DataImageUrlField="thumburl" HeaderText="Imagem" >
                <ControlStyle Height="70px" Width="60px" />
                <ItemStyle Height="70px" HorizontalAlign="Center" VerticalAlign="Middle" 
                    Width="60px" Wrap="False" />
            </asp:ImageField>
            <asp:BoundField DataField="titulo" HeaderText="Título" >
            <ItemStyle Width="180px" />
            </asp:BoundField>
            <asp:BoundField DataField="datapublicacao" HeaderText="Data Publicação" 
                DataFormatString="{0:d}">
            <ItemStyle Width="70px" HorizontalAlign="Center" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="preco" HeaderText="Preço">
            <ItemStyle Width="40px" HorizontalAlign="Right" Wrap="False" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="ID" 
                DataNavigateUrlFormatString="Detalhe.aspx?ID={0}" Text="detalhes...">
            <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:HyperLinkField>
        </Columns>
        <HeaderStyle BackColor="#996600" ForeColor="White" />
    </asp:GridView>
</asp:Content>
