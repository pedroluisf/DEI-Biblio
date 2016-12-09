<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="WebIDEIBiblio.Administrador.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="grdClientes" runat="server" AllowPaging="True" 
    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
    CellPadding="4" DataKeyNames="Id" 
    onselectedindexchanged="grdClientes_SelectedIndexChanged" PageSize="5" 
    Width="100%" onrowdeleting="grdClientes_RowDeleting">
        <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Select" 
                ImageUrl="~/Images/info.png" Text="info" />
            <asp:ButtonField ButtonType="Image" CommandName="Delete" 
                ImageUrl="~/Images/delete.png" Text="delete" />
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
    <br />
    <asp:Panel ID="Panel1" runat="server">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ValidationGroup="1" />
        <asp:Label ID="Label1" runat="server" Text="Id" Width="150px"></asp:Label>
        <asp:TextBox ID="txtClienteId" runat="server" BackColor="Silver" 
            Enabled="False"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Username" Width="150px"></asp:Label>
        <asp:TextBox ID="txtClienteUserName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="validatorUsername" runat="server" 
            ControlToValidate="txtClienteUserName" 
            ErrorMessage="Este campo é de preenchimento obrigatório." ValidationGroup="1"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblClienteEmail" runat="server" Text="Email" Width="150px"></asp:Label>
        <asp:TextBox ID="txtClienteEmail" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="validatorEmail" runat="server" 
            ControlToValidate="txtClienteEmail" 
            ErrorMessage="O email introduzido não foi validado." 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
            ValidationGroup="1"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtClienteEmail" 
            ErrorMessage="Este campo é de preenchimento obrigatório." ValidationGroup="1"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Nome real" Width="150px"></asp:Label>
        <asp:TextBox ID="txtClienteNome" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtClienteNome" 
            ErrorMessage="Este campo é de preenchimento obrigatório." ValidationGroup="1"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Função" Width="150px"></asp:Label>
        <asp:DropDownList ID="cboFuncao" runat="server" Width="128px">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Password" Width="150px"></asp:Label>
        <asp:TextBox ID="txtClientePwd" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Confirmar password" Width="150px"></asp:Label>
        <asp:TextBox ID="txtClientePwdConfirm" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Morada" Width="150px"></asp:Label>
        <asp:TextBox ID="txtClienteMorada" runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="validatorMorada" runat="server" 
            ControlToValidate="txtClienteMorada" 
            ErrorMessage="Este campo é de preenchimento obrigatório."></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="btAdd" runat="server" onclick="btAdd_Click" Text="Adicionar" />
    </asp:Panel>
</asp:Content>
