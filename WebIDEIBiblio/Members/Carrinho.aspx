<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="WebIDEIBiblio.Members.Carrinho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <asp:GridView ID="grdCarrinho" runat="server" DataKeyNames="Id" 
        AutoGenerateColumns="False" Width="100%" 
        onrowdeleting="grdCarrinho_RowDeleting">
        <Columns>
            <asp:CommandField DeleteImageUrl="~/Images/delete.png" 
                ShowDeleteButton="True" >
            <ItemStyle Width="50px" />
            </asp:CommandField>
            <asp:BoundField DataField="Categoria" HeaderText="Categoria" >
            <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="Preco" HeaderText="Preço" />
        </Columns>
        <HeaderStyle BackColor="#996600" ForeColor="White" />
    </asp:GridView>

    <asp:LinkButton runat="server" ID="btFinalizar" onclick="btFinalizar_Click">Finalizar encomenda</asp:LinkButton>
</asp:Content>
