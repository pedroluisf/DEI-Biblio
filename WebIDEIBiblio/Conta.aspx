<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Conta.aspx.cs" Inherits="WebIDEIBiblio.Conta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title" style="width:490px"><span class="title_icon"><img src="images/bullet1.gif" alt="" title="" /></span><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></div>
    <asp:GridView ID="grdEnc" runat="server" AllowPaging="True" AllowSorting="True" 
        AutoGenerateColumns="False" AutoGenerateSelectButton="True" 
        DataSourceID="DataSourceEncomendas" DataKeyNames="ID" PageSize="5">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" 
                SortExpression="ID" InsertVisible="False" ReadOnly="True" visible="false"/>
            <asp:BoundField DataField="dataEncomenda" HeaderText="Data Encomenda" 
                SortExpression="dataEncomenda" DataFormatString="{0:d}">
                <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="nomeCliente" HeaderText="Nome Cliente" 
                SortExpression="nomeCliente" >
                <ItemStyle HorizontalAlign="Left" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="moradaCliente" HeaderText="Morada Cliente" 
                SortExpression="moradaCliente" >
                <ItemStyle HorizontalAlign="Left" Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="nomeExpedidor" HeaderText="Expedidor"
                SortExpression="nomeExpedidor" >
                <ItemStyle HorizontalAlign="Left" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="valorExpedicao" HeaderText="Valor Portes" 
                SortExpression="valorExpedicao" >
                <ItemStyle HorizontalAlign="Right" Width="60px" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle BackColor="#996600" ForeColor="White" />
    </asp:GridView>
    <asp:AccessDataSource ID="DataSourceEncomendas" runat="server" 
        DataFile="~/App_Data/IDEIBiblio.mdb" 
        
        SelectCommand="SELECT [ID], [dataEncomenda], [nomeCliente], [moradaCliente], [nomeExpedidor], [valorExpedicao] FROM [Encomendas] WHERE ([utilizador_id] = ?)">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="utilizador_id" 
                SessionField="cliente_id" Type="Int32" />
        </SelectParameters>
    </asp:AccessDataSource>
    <br/>
    <asp:GridView ID="grdDet" runat="server" AllowPaging="True" AllowSorting="True" 
        DataSourceID="DataSourceDetalhe" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="descricao" HeaderText="descricao" 
                SortExpression="descricao" />
            <asp:BoundField DataField="preco" HeaderText="preco" SortExpression="preco" />
            <asp:BoundField DataField="quantidade" HeaderText="quantidade" 
                SortExpression="quantidade" />
        </Columns>
        <HeaderStyle BackColor="#996600" ForeColor="White" />
    </asp:GridView>
<asp:AccessDataSource ID="DataSourceDetalhe" runat="server" 
    DataFile="~/App_Data/IDEIBiblio.mdb" 
    SelectCommand="SELECT [descricao], [preco], [quantidade] FROM [EncomendasDetalhe] WHERE ([encomenda_id] = ?)">
    <SelectParameters>
        <asp:ControlParameter ControlID="grdEnc" Name="encomenda_id" 
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:AccessDataSource>
</asp:Content>
