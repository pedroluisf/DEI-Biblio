<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Funcoes.aspx.cs" Inherits="WebIDEIBiblio.Admin.Funcoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdFuncoes" runat="server" BackColor="White" 
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        onrowdeleting="grdFuncoes_RowDeleting" Width="100%">
        <Columns>
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
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Item" Width="150px"></asp:Label>
    <asp:TextBox ID="txtItem" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtItem" 
        ErrorMessage="Este campo é de preenchimento obrigatório."></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="btAdd" runat="server" onclick="btAdd_Click" Text="Adicionar" />
</asp:Content>
