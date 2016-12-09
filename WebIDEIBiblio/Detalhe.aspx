<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalhe.aspx.cs" MasterPageFile="~/Site.master" Inherits="WebIDEIBiblio.Detalhe" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="title" style="width:490px"><span class="title_icon"><img src="/images/bullet1.gif" alt="" title="" /></span><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></div>
<div class="feat_prod_box_details"> 
    <div class="prod_img" >
        <asp:PlaceHolder ID="plBookPic" runat="server"></asp:PlaceHolder>
        <div id="imgThumb">
            <br /><br />
            <asp:PlaceHolder ID="plBookZoom" runat="server"></asp:PlaceHolder>
        </div>
    </div>
    <div class="prod_det_box">
        <div class="prod_det_box">
            <div class="box_top"></div>
            <div class="box_center">
                <div class="prod_title">Detalhe</div>
                <p class="details"><asp:PlaceHolder ID="plBookDetalhe" runat="server"></asp:PlaceHolder></p>
                <div class="price"><strong>PREÇO:</strong><asp:Literal ID="ltBookPreco" runat="server"></asp:Literal></div>
            <asp:UpdatePanel ID="pnlResponse" runat="server">
                <ContentTemplate>
                        &nbsp;&nbsp;<asp:ImageButton src="/Images/order_now.gif" ID="btnOrder" runat="server" onclick="btnOrder_Click"/>
                        <br />
                        &nbsp;&nbsp;<asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
                <div class="clear"></div>
            </div>                    
            <div class="box_bottom"></div>
        </div>  
        <div class="clear"></div>  
    </div>    
    <div class="clear"></div>  
</div>    
<asp:Literal ID="lightbox" runat="server"></asp:Literal>
</asp:Content>
