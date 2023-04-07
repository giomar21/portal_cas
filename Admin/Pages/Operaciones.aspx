<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MP.Master" AutoEventWireup="true" CodeBehind="Operaciones.aspx.cs" Inherits="Admin.Pages.Operaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container box">
        <h1 class="title">Operaciones</h1>

        <div class="field">
            <label class="label">Solicitante</label>
            <div class="control">
                <asp:TextBox runat="server" ID="solicitante" CssClass="input" type="text" placeholder="Solicitante"></asp:TextBox>
            </div>
        </div>

        <div class="field">
            <label class="label">Sucursal</label>
            <div class="control">
                <asp:TextBox runat="server" ID="sucursal" CssClass="input" type="text" placeholder="Sucursal"></asp:TextBox>
            </div>
        </div>

        <div class="field">
            <label class="label">Candado</label>
            <div class="control">
              
                 <asp:DropDownList AutoPostBack="true" ID="ddlCandado" CssClass="input" runat="server" OnSelectedIndexChanged="ddlCandado_SelectedIndexChanged">
                 </asp:DropDownList>
            </div>
        </div>
        
        <div id="divMotivo" runat="server" class="field"><%--cambio de precio PK--%>
            <label id="lblMotivo" runat="server"  class="label">Motivo</label>
            <div class="control">
                <asp:TextBox runat="server" ID="motivo" CssClass="input" type="text" placeholder="Motivo"></asp:TextBox>
            </div>
        </div>
        <div id="divTarjeta" runat="server" class="field"><%--Tarjeta Regalo--%>
            <label id="lblTarjeta" runat="server" class="label">Tarjeta</label>
            <div class="control">
                <asp:TextBox runat="server" ID="tarjeta" CssClass="input" type="text" placeholder="No. Tarjeta"></asp:TextBox>
            </div>
        </div>
        <div id="divMonto" runat="server" class="field"><%--Cambio de precio PK--%>
            <label id="lblMonto" runat="server"  class="label">Monto</label>
            <div class="control">
                <asp:TextBox runat="server" ID="monto" CssClass="input" type="text" placeholder="Monto"></asp:TextBox>
            </div>
        </div>
        <div id="divDescuento" runat="server" class="field"><%--Descuento DT--%>
            <label id="lblDescuento" runat="server" class="label">Descuento</label>
            <div class="control">
                <asp:TextBox runat="server" ID="descuento" CssClass="input" type="text" placeholder="Descuento"></asp:TextBox>
            </div>
        </div>
        <div id="divEAN" runat="server" class="field"><%--cambio de precio PK--%> <%--Descuento DT--%>
            <label id="lblEAN" runat="server"  class="label">EAN</label>
            <div class="control">
                <asp:TextBox runat="server" ID="EAN" CssClass="input" type="text" placeholder="EAN"></asp:TextBox>
            </div>
        </div>

        <div class="field is-grouped">
            <div class="control">
                <asp:Button runat="server" ID="Apertura" CssClass="button is-link" OnClick="Registrar_Click" Text="Apertura"/>
            </div>
            <div class="control">
                <a class="button is-link is-light" href="Index.aspx">Cancelar</a>
            </div>
        </div>
    </div>
</asp:Content>
