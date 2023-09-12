

<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Transferencias_Internas.aspx.cs" Inherits="PrograVProyectoll.Pages.Transferencias_Internas" EnableEventValidation="false" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <link href="../Styles/Interna.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>

<html>

<body>
 
    <div class="container">
      <form  id="formTransferencia" runat="server" method="post">
        <div>
            <h2>Transferencia de dinero</h2>
            <hr />
        <div id="ddlCuentaOrigenContainer" data-numero-identificacion="<%= Session["identificacion"] %>">
    
            <asp:DropDownList ID="ddlCuentaOrigen" runat="server" ></asp:DropDownList>
        </div>
         

            <div>
        
             <input type="text" id="txtCuentaDestino" runat="server" placeholder="Cuenta Destino" required="true" />
             <div id="mensaje" class="mensaje"></div>
           </div>

            <div>
             
                <input type="text" id="txtMonto" runat="server" placeholder="Monto" required="true" />
            </div>

            <div>
                
                <input type="email" id="txtCorreoNotificacion" runat="server" placeholder="Correo de notificación" required="true" class="styled-input" />
            </div>

            <div>
            
                <input type="text" id="txtConcepto" runat="server" required="true" placeholder="Concepto"/>
            </div>

             <div>
       
            <asp:DropDownList ID="ddlTipoTransaccion" runat="server">
           
                <asp:ListItem Text="Interna" Value="interna"></asp:ListItem>
            </asp:DropDownList>
        </div>


          <div>
    <label for="lblFechaHora" style="display: inline-block;">Fecha/Hora:</label>
    <asp:Label ID="lblFechaHora" runat="server" Text="<%# DateTime.Now.ToString() %>" style="display: inline-block; vertical-align: middle;"></asp:Label>
</div>


      <div>
    <label for="lblCanal" style="display: inline-block;">Canal:</label>
    <asp:Label ID="lblCanal" runat="server" Text="web" style="display: inline-block; vertical-align: middle;"></asp:Label>
</div>

            <br />
         <asp:Button ID="btnTransferir" runat="server" Text="Transferir" OnClick="btnTransferir_Click" />


            <hr />

            <div>
                <asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</div>

</body>
</html>
 <script>
     $(document).ready(function () {
         // Obtener el número de identificación desde el atributo data
         var ddlCuentaOrigenContainer = document.getElementById('ddlCuentaOrigenContainer');
         var numeroIdentificacion = ddlCuentaOrigenContainer.dataset.numeroIdentificacion;

         // Realizar la solicitud GET a la API para obtener las cuentas activas del cliente
         $.ajax({
             url: "https://localhost:44305/api/clientes/ObtenerCuentasActivas?N_Identificacion=" + numeroIdentificacion,
             type: "GET",
             dataType: "json", // Especificar que esperamos recibir datos en formato JSON
             success: function (response) {
                 // Obtener el elemento select
                 var ddlCuentaOrigen = document.getElementById('<%= ddlCuentaOrigen.ClientID %>');

                // Limpiar las opciones existentes del select
                 ddlCuentaOrigen.innerHTML = "";
                 // Agregar el elemento "Cuenta Origen" al inicio del DropDownList
                 var optionCuentaOrigen = document.createElement("option");
                 optionCuentaOrigen.value = "";
                 optionCuentaOrigen.text = "Cuenta Origen";
                 ddlCuentaOrigen.appendChild(optionCuentaOrigen);

                // Recorrer los datos de las cuentas y agregar opciones al select
                for (var i = 0; i < response.length; i++) {
                    var cuenta = response[i];

                    // Crear una nueva opción
                    var option = document.createElement("option");
                    option.value = cuenta.NumeroCuenta;
                    option.text = cuenta.NumeroCuenta;

                    // Agregar la opción al select
                    ddlCuentaOrigen.appendChild(option);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                // Manejar el error en caso de que la solicitud falle
                console.log("Error al obtener las cuentas activas:", errorThrown);
            }
        });
    });
     $(document).ready(function () {
         // Obtener el elemento de entrada de texto
         var txtCuentaDestino = document.getElementById('<%= txtCuentaDestino.ClientID %>');

          // Asignar el evento blur al campo de texto
          txtCuentaDestino.addEventListener('blur', function () {
              var numCuenta = txtCuentaDestino.value;
              obtenerNombreCliente(numCuenta);
          });
      });
    function obtenerNombreCliente(numCuenta) {
        // Realizar la solicitud GET a la API para obtener el nombre del cliente
        $.ajax({
            url: "https://localhost:44305/api/Cliente/" + numCuenta,
            type: "GET",
            dataType: "json", // Especificar que esperamos recibir datos en formato JSON
            success: function (response) {
                // Mostrar notificación de éxito
                toastr.success("Dueño de la cuenta: " + response);
            },
            error: function (xhr, textStatus, errorThrown) {
                // Mostrar notificación de error
                toastr.error("Cuenta no existe");
            }
        });
    }

    // Variable para almacenar el contenido de la respuesta del API
  

     //// Mostrar contenido de la respuesta solo si hay un mensaje de notificación
     if (responseMessage ) {
         // Mostrar notificación de éxito con el contenido de la respuesta
         toastr.success(responseMessage);
     }

     // Mostrar mensaje de error solo si está presente
     if (responseError) {
         // Mostrar notificación de error con el mensaje
         toastr.error(responseError);
     }

 </script>

</asp:Content>
