<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PrograVProyectoll.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Styles/Sidevar.css" rel="stylesheet" />
    <link href="Styles/Login.css" rel="stylesheet" />
<div class="container" style="display: flex; align-items: center; justify-content: center; height: 400px;" aria-atomic="False" aria-hidden="False" aria-orientation="horizontal">
    <img class="userImage2" src="Images/logobanco1.png" />
    <asp:Label ID="lblWelcomeMessage" runat="server" Text="" CssClass="welcome-message"></asp:Label>

          <%--  <div class="logo" aria-multiline="True">
                  <image id="imgIcon2" src="../images/logobanco1.png"></image>
              </div>--%>
</div>




</asp:Content>
