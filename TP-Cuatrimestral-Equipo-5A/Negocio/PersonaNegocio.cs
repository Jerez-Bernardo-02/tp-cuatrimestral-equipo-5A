using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocio
{
    public class PersonaNegocio
    {
        

        public bool ValidarDatosPorPermiso(Usuario usuario, string email, string documento, string matricula) //metodo para validar si cualquiera de los 3 tipos de usuarios se encuentra registrado: medico, paciente, recepcionista
        {
            return false; //codigo comentado porque aun falta desarrollar usuarios, permisos, login etc pero lo comence a armar ya que vamos a reutilizar formulario para los 3 tipos de perfiles: medico, paciente y recepcionista
            /*AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "";// creo una consulta vacìa y segun el tipo de usuario que se va a registrar, chequeamos si a esta registrado en la DB

                // Elegimos la consulta según el tipo de usuario, 3 posibilidades
                switch (usuario.Permiso.Descripcion)
                {
                    case "Medico":
                        consulta = "SELECT Id FROM Medicos WHERE Email = @Email OR Dni = @Dni OR Matricula = @Matricula";
                        break;

                    case "Paciente":
                        consulta = "SELECT Id FROM Pacientes WHERE Email = @Email OR Dni = @Dni";
                        break;

                    case "Recepcionista":
                        consulta = "SELECT Id FROM Recepcionistas WHERE Email = @Email OR Dni = @Dni";
                        break;

                    default:
                        return false; // no valida nada si el permiso no coincide
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@Email", email);
                datos.setearParametro("@Dni", documento);

                if (usuario.Permiso.Descripcion == "Medico")// Solo agregamos la matrícula si corresponde
                    datos.setearParametro("@Matricula", matricula);

                datos.ejecutarLectura();

                return datos.Lector.Read(); // true si ya existe
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar datos por permiso.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }*/
        }

    }
}
