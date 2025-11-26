using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }

        }

        //Constructor
        public AccesoDatos()
        {                                 //NombreServidor(el punto es genérico) ; NombreBaseDeDatos ; Forma de concetarse(Windows authentication)
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CLINICA_DV; integrated security=true");
            comando = new SqlCommand();                                //TENEMOS QUE PONER EL ***NOMBRE DE LA DB***
        }

        public void setearConsulta(string consulta)
        {
            //comando realiza la acción e inyecta la consulta SQL de tipo texto (recibido por parámetro).
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            //ejecuta el comando (consulta SQL (SELECT)) en la conexion (BD)
            comando.Connection = conexion;
            try
            {
                //se abre la conexion y se realiza la lectura.
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                //ejecuta una acción que no sea de lectura (insert, delete, update)
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ejecutarEscalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                //ejecuta una acción que no sea de lectura (insert, delete, update)
                return (int)comando.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cerrarConexion()
        {
            //En caso de haber un lector activo cuando se llama al método, también se cierra junto con la conexión.
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public void setearParametro(string nombre, object valor)
        {
            //Seteo de parametros recibidos para ejecutar acciones en BD (NombreTabla; Valor a insertar)
            comando.Parameters.AddWithValue(nombre, valor);
        }



    }
}
