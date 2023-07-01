<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PrograVProyectoll.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>
    <! ––«Estilos CSS del Login «––>
    <link href="Estilos%20CSS/Generales/Login.css" rel="stylesheet" />
    <! ––«CDN Framework Bulma para estilos «––>
    <link href="Styles/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       
        
        <div class="divContenido" runat="server">

              <%--<div class="logo">
                  <image id="imgIcon" src="../images/Logo2.png"></image>
              </div>--%>
           
              <div class="inputLogin">
                  <h1 id="textBienvenida">Inicie Sesion</h1>

                   <div class="alert alert-warning alert-dismissible fade show" id="lblMensaje" role="alert" visible="false" runat="server" >
                    
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" runat="server"></button>
                </div>

                 <div style="margin-right:30px">
                      <div class="inputContainerUser">
                          <p class="p">Usuario</p>
                          <asp:TextBox runat="server" class="input" type="text" placeholder="Ingrese tu usuario" id="input_User"></asp:TextBox>
                      </div>

                  

                      <div class="inputContainerPassword">
                          <p class="p">Contraseña</p>
                          <asp:TextBox runat="server"  class="input" type="password" placeholder="Ingrese tu contraseña" id="input_Pass"></asp:TextBox>
                      </div>

                  
                      <div>
                          <asp:Button id="bntIniciarSesion" runat="server" CssClass="btnIniciarSesion" Text="Iniciar Sesión" OnClick="bntIniciarSesion_Click"/>
                      </div>
                  </div>
                  <div id="DivFinalpadding">
                      <p id="p2">Políticas de Privacidad y Términos de uso</p>
                  </div>
     
             
            </div>

        </div>  

    </form>
</body>
</html>
