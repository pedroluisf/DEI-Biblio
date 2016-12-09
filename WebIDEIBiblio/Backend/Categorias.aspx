<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="WebIDEIBiblio.Backend.Categorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdCategorias" runat="server" AllowPaging="True" 
        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" DataKeyNames="Id" 
        onpageindexchanged="grdCategorias_PageIndexChanged" 
        onpageindexchanging="grdCategorias_PageIndexChanging" 
        onselectedindexchanged="grdCategorias_SelectedIndexChanged" PageSize="5" 
        Width="100%">
        <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Select" 
                ImageUrl="~/Images/info.png" Text="info">
            <ItemStyle Width="35px" />
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Image" CommandName="Delete" 
                ImageUrl="~/Images/delete.png" Text="delete">
            <ItemStyle Width="35px" />
            </asp:ButtonField>
        </Columns>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
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
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="Label2" runat="server" Text="Id" Width="150px"></asp:Label>
        <asp:TextBox ID="txtCatId" runat="server" BackColor="Silver" Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Descrição" Width="150px"></asp:Label>
        <asp:TextBox ID="txtCatDesc" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="validatorDesc" runat="server" 
            ControlToValidate="txtCatDesc" ErrorMessage="Este campo é obrigatório."></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="btAdd" runat="server" onclick="btAdd_Click" Text="Adicionar" />
    </asp:Panel>
</asp:Content>
