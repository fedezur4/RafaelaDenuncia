using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ServicioPrincipal
{
    /// <summary>
    /// Descripción breve de ServicioPrincipal
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioPrincipal : System.Web.Services.WebService
    {
        SqlConnection connStr = new SqlConnection(ConfigurationManager.ConnectionStrings["RafaelaDenunciaDB"].ConnectionString);
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        [WebMethod]
        public DataSet GetData()
        {

//            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (connStr.State == ConnectionState.Closed)
                {
                    connStr.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("select * from Usuarios", connStr);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        [WebMethod]
        public void registrarUsuario(string nombre, string apellido, string mail, string contraseña, string dirección, string fecha, string latitud, string longitud)
        {
            try
            {
               // using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (connStr.State == ConnectionState.Closed)
                    {
                        connStr.Open();
                    }
                    SqlCommand command = new SqlCommand("nuevoUsuario", connStr);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("nombre", nombre);
                    command.Parameters.AddWithValue("apellido", apellido);
                    command.Parameters.AddWithValue("mail", mail);
                    command.Parameters.AddWithValue("contraseña", contraseña);
                    command.Parameters.AddWithValue("direccion", dirección);
                    command.Parameters.AddWithValue("fecha", fecha);
                    command.Parameters.AddWithValue("latitud", latitud);
                    command.Parameters.AddWithValue("longitud", longitud);
                    command.ExecuteNonQuery();
                    connStr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [WebMethod]
        public string buscarUsuario(string mail, string contraseña)
        {
            try
            {
                //using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (connStr.State == ConnectionState.Closed)
                    {
                        connStr.Open();
                    }
                    SqlCommand command = new SqlCommand("buscarUsuario", connStr);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("mail", mail);
                    command.Parameters.AddWithValue("contraseña", contraseña);
                    SqlParameter resultado = new SqlParameter("result", DbType.Int32);
                    resultado.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(resultado);
                    command.ExecuteNonQuery();
                    int result = Int32.Parse(command.Parameters["result"].Value.ToString());
                    connStr.Close();
                    if (result == 1)
                    {
                        return ("El usuario existe");
                    }
                    else
                    {
                        return ("el usuario no existe");
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
