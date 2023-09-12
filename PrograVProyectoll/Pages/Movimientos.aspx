<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Movimientos.aspx.cs" Inherits="PrograVProyectoll.Pages.Movimientos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Tablas.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


         <form runat="server"> 
        <asp:DropDownList runat="server" id="ddl_cuentasActivas" style="margin-top: 100px; font-size: 14px; color: #333; background-color: #FFF; border: 1px solid #CCC; padding: 5px;" CssClass="drop-down-list" AutoPostBack="True" OnSelectedIndexChanged="ddl_cuentasActivas_SelectedIndexChanged">
        
        </asp:DropDownList>
           </form>
        <div class="container" style="margin-top:100px;">
    <table class="tabla">
      <tr>
        <th>Fecha del movimiento</th>
        <th>Tipo</th>
        <th>Monto</th>
        <th>Tipo de transacción</th>
        <th>Descripción</th>
        <th>Identificador</th>
      </tr>
    <tbody id="lstfrmMantenimiento" runat="server">
      <tr>
          <td><a></a> href="frmMantenimiento.aspx" </td>
      </tr>
  </tbody>
    </table>
  </div>







</asp:Content>
