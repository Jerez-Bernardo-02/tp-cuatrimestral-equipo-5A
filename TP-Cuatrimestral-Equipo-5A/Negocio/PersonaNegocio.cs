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
           
            SELECT --pacientes
                U.Id AS IdUsuario,
                Pa.Dni,
                Pa.Nombre,
                Pa.Apellido,
                U.Usuario AS NombreUsuario,
                U.Activo AS ActivoUsuario,
                'Paciente' AS Rol
            FROM Usuarios U
            INNER JOIN Pacientes Pa ON Pa.IdUsuario = U.Id

            UNION ALL

            SELECT --recepcionistas
                U.Id AS IdUsuario,
                R.Dni,
                R.Nombre,
                R.Apellido,
                U.Usuario AS NombreUsuario,
                U.Activo AS ActivoUsuario,
                'Recepcionista' AS Rol
            FROM Usuarios U
            INNER JOIN Recepcionistas R ON R.IdUsuario = U.Id

            UNION ALL

            SELECT --medicos
                U.Id AS IdUsuario,
                M.Dni,
                M.Nombre,
                M.Apellido,
                U.Usuario AS NombreUsuario,
                U.Activo AS ActivoUsuario,
                'Medico' AS Rol
            FROM Usuarios U
            INNER JOIN Medicos M ON M.IdUsuario = U.Id

            UNION ALL

            SELECT --administradores
                U.Id AS IdUsuario,
                NULL AS Dni,
                NULL AS Nombre,
                NULL AS Apellido,
                U.Usuario AS NombreUsuario,
                U.Activo AS ActivoUsuario,
                'Administrador' AS Rol
            FROM Usuarios U
            WHERE U.IdPermiso = 4;
                    ");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                   
                    Persona aux = new Persona();
                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];

                    aux.Dni = datos.Lector["Dni"] != DBNull.Value ? (string)datos.Lector["Dni"] : "";
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : "";
                    aux.Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : "";

                    aux.Usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    aux.Usuario.Activo = (bool)datos.Lector["ActivoUsuario"];
                    aux.Rol = (string)datos.Lector["Rol"];

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
        public Persona BuscarPorIdUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                SELECT TOP 1
	                p.Nombre,
	                p.Apellido,
	                p.Dni,
	                p.Email,
	                p.Telefono,
	                p.FechaNacimiento
                FROM Pacientes p
                WHERE p.IdUsuario = @idUsuario

                UNION ALL

                SELECT TOP 1
	                m.Nombre,
	                m.Apellido,
	                m.Dni,
	                m.Email,
	                m.Telefono,
	                m.FechaNacimiento
                FROM Medicos m
                WHERE m.IdUsuario = @idUsuario

                UNION ALL

                SELECT TOP 1
	                r.Nombre,
	                r.Apellido,
	                r.Dni,
	                r.Email,
	                r.Telefono,
	                r.FechaNacimiento
                FROM Recepcionistas r
                WHERE r.IdUsuario = @idUsuario
        ");

                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Persona persona = new Persona();
                    persona.Nombre = (string)datos.Lector["Nombre"];
                    persona.Apellido = (string)datos.Lector["Apellido"];
                    persona.Dni = (string)datos.Lector["Dni"];
                    persona.Email = (string)datos.Lector["Email"];
                    persona.Telefono = (string)datos.Lector["Telefono"];
                    persona.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];

                    return persona;
                }

                return null; // No encontrado
            }
            catch (Exception ex)
            {
                throw ex;
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
