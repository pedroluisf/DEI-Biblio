<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="WebIDEIBiblio.Backend.Produtos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <asp:Label ID="Label2" runat="server" Text="Produtos" Width="100%"></asp:Label>
    </h2>
    <br />
    <asp:GridView ID="grdProdutos" runat="server" CellPadding="4" AllowPaging="True" 
        AllowSorting="True" DataKeyNames="id" 
        onselectedindexchanged="grdProdutos_SelectedIndexChanged" PageSize="5" 
        onpageindexchanging="grdProdutos_PageIndexChanging" 
        onpageindexchanged="grdProdutos_PageIndexChanged" 
        onrowdeleting="grdProdutos_RowDeleting" BackColor="White" 
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" Width="100%">
        <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Select" 
                ImageUrl="~/Images/info.png" Text="info" >
            <ItemStyle Width="35px" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="Delete" 
                ImageUrl="~/Images/delete.png" Text="delete" >
            <ItemStyle Width="35px" />
            </asp:ButtonField>
        </Columns>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <PagerSettings PageButtonCount="5" />
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#330099" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <SortedAscendingCellStyle BackColor="#FEFCEB" />
        <SortedAscendingHeaderStyle BackColor="#AF0101" />
        <SortedDescendingCellStyle BackColor="#F6F0C0" />
        <SortedDescendingHeaderStyle BackColor="#7E0000" />
    </asp:GridView>
    <asp:ImageButton ID="btNew" runat="server" ImageUrl="~/Images/add.png" 
        onclick="btNew_Click" />
    <br />
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server">
        <asp:ValidationSummary ID="validationSummary" runat="server" />
        <asp:Label ID="Label7" runat="server" Text="Id" Width="150px"></asp:Label>
        <asp:TextBox ID="txtProdId" runat="server" BackColor="Silver" Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Categoria" Width="150px"></asp:Label>
        <asp:DropDownList ID="cboCategorias" runat="server" DataTextField="Descricao" 
            DataValueField="Id" Width="128px">
        </asp:DropDownList>
        <br />
        <asp:Label ID="lblDesc" runat="server" Text="Descrição" Width="150px"></asp:Label>
        <asp:TextBox ID="txtProdDesc" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="validatorDescricao" runat="server" 
            ControlToValidate="txtProdDesc" ErrorMessage="Este campo é obrigatório"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Autor" Width="150px"></asp:Label>
        <asp:TextBox ID="txtProdAutor" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="validatorAutor" runat="server" 
            ControlToValidate="txtProdAutor" ErrorMessage="Este campo é obrigatório"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Preço" Width="150px"></asp:Label>
        <asp:TextBox ID="txtProdPreco" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="validatorPreco" runat="server" 
            ControlToValidate="txtProdPreco" 
            ErrorMessage="O preço é um campo obrigatório. Valores possíveis acima de 0." 
            ValidationExpression="[0-9]+(,[0-9]+)?"></asp:RegularExpressionValidator>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Data publicação" Width="150px"></asp:Label>
        <asp:Calendar ID="dataPublicacao" runat="server"></asp:Calendar>
        <br />
        <asp:Button ID="btAdd" runat="server" Text="Adicionar" onclick="btAdd_Click" />
    </asp:Panel>
</asp:Content>
