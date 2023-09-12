using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class Cn_Recursos
    {
        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
  


        public string ObtenerTipoCuenta(string numeroCuenta)
        {
            string tipoCuenta = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(cn))
                {
                    connection.Open();

                    // Consulta en la tabla Cuenta_Corriente
                    SqlCommand command = new SqlCommand("SELECT Num_Cuenta FROM Cuenta_Corriente WHERE Num_Cuenta = @NumeroCuenta", connection);
                    command.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        tipoCuenta = "Cuenta Corriente";
                    }
                    else
                    {
                        reader.Close();

                        // Consulta en la tabla Cuentas_Ahorro
                        command.CommandText = "SELECT Num_Cuenta FROM Cuentas_Ahorro WHERE Num_Cuenta = @NumeroCuenta";
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            tipoCuenta = "Cuenta de Ahorro";
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Console.WriteLine("Error: " + ex.Message);
            }

            return tipoCuenta;
        }

    }
}
