<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Lista_Cuentas.aspx.cs" Inherits="PrograVProyectoll.Pages.Lista_Cuentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
   <link href="../Styles/Tablas.css" rel="stylesheet" />
    <link href="../Styles/Sidevar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container"  style="margin-top:300px;">
    <table class="tabla">
      <tr>
        <th>Num_Cuenta</th>
        <th>Monto</th>
        <th>TipoCuenta</th>
      </tr>
       <tbody id="lstfrmMantenimiento" runat="server">
      <tr>
       <td><a></a> href="frmMantenimiento.aspx" </td>
      </tr>
        </tbody>
        
    </table>
  </div>



</asp:Content>
