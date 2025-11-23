using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocio
{
    public class PersonaNegocio
    {
        public List<Persona> listarPersonaRol()//lisatado para gestion usuarios con roles
        {
            AccesoDatos datos = new AccesoDatos();
            List<Persona> lista = new List<Persona>();
            try
            {
                datos.setearConsulta(@"
            -- Medicos
            SELECT
                M.Id AS IdPersona,
                M.Nombre AS Nombre,
                M.Apellido AS Apellido,
                M.Dni AS Dni,
                M.Email AS Email,
                M.Telefono AS Telefono,
                M.UrlImagen AS UrlImagen,
                U.Id AS IdUsuario,                 
                U.Usuario AS UsuarioNombre,
                U.Activo AS Activo,
                'Médico' AS Rol
            FROM Usuarios U
            INNER JOIN Medicos M ON U.Id = M.IdUsuario

            UNION ALL

            -- Pacientes
            SELECT
                P.Id AS IdPersona,
                P.Nombre AS Nombre,
                P.Apellido AS Apellido,
                P.Dni AS Dni,
                P.Email AS Email,
                P.Telefono AS Telefono,
                P.UrlImagen AS UrlImagen,
                U.Id AS IdUsuario,                 
                U.Usuario AS UsuarioNombre,
                U.Activo AS Activo,
                'Paciente' AS Rol
            FROM Usuarios U
            INNER JOIN Pacientes P ON U.Id = P.IdUsuario

            UNION ALL

            -- Recepcionistas
            SELECT
                R.Id AS IdPersona,
                R.Nombre AS Nombre,
                R.Apellido AS Apellido,
                R.Dni AS Dni,
                R.Email AS Email,
                R.Telefono AS Telefono,
                R.UrlImagen AS UrlImagen,
                U.Id AS IdUsuario,                 
                U.Usuario AS UsuarioNombre,
                U.Activo AS Activo,
                'Recepcionista' AS Rol
            FROM Usuarios U
            INNER JOIN Recepcionistas R ON U.Id = R.IdUsuario;
        ");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Persona aux = new Persona();
                    aux.Id = (int)datos.Lector["IdPersona"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.NombreUsuario = (string)datos.Lector["UsuarioNombre"];
                    aux.Usuario.Activo = (bool)datos.Lector["Activo"];
                    aux.Rol = datos.Lector["Rol"].ToString();
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


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
