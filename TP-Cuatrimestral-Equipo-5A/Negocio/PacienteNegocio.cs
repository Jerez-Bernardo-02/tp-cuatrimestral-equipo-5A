using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Dominio;

namespace Negocio
{
    public class PacienteNegocio
    {
        public int agregarPaciente(Paciente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono) output inserted.Id VALUES (@nombre, @apellido,@fechaNacimiento, @dni, @email, @telefono)");
                
                //seteamos parametros  (@Clave, valor)
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@fechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametro("@dni", nuevo.Documento);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", nuevo.Telefono);

                //para obtener el id autogenerado en la BD 
                int id = datos.ejecutarEscalar();
                nuevo.Id = id;

                return nuevo.Id;
            }
            catch (Exception )
            {
                throw;
               
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Paciente buscarPacientePorDni(string dni)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Nombre, Apellido, FechaNacimiento, Dni, Email, Telefono, UrlImagen, IdUsuario, Id FROM Pacientes WHERE Dni = @documento");
                datos.setearParametro("@documento", dni);
                datos.ejecutarLectura();

                //seteamos parametros  (@Clave, valor) - si hay coicidencia entra
                if (datos.Lector.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.Documento = (string)datos.Lector["Dni"];
                    paciente.Nombre = (string)datos.Lector["Nombre"];
                    paciente.Apellido = (string)datos.Lector["Apellido"];
                    paciente.Email = (string)datos.Lector["Email"];
                    paciente.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    paciente.Telefono = (string)datos.Lector["Telefono"];
                    if (datos.Lector["UrlImagen"] != DBNull.Value)
                    {
                        paciente.UrlImagen = (string)datos.Lector["UrlImagen"];
                    }
                    else
                    {
                        paciente.UrlImagen = null;
                    }

                    paciente.Usuario = new Usuario();
                    paciente.Usuario.Id = (int)datos.Lector["IdUsuario"];
                   
                   
                    //se retorna el objeto paciente encontrado y seteado
                    return paciente;
                }
                //si no hay coicidencia retorna null
                return null;
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
        public List<Paciente> listar()
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT  P.Id AS IdPaciente, Pe.Id AS IdPersona, Pe.Nombre, Pe.Apellido, Pe.FechaNacimiento, Pe.Dni, Pe.Email, Pe.Telefono, Pe.UrlImagen, Pe.Activo FROM Pacientes P INNER JOIN Personas Pe ON P.IdPersona = Pe.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["IdPaciente"];

                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Documento = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    aux.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                    

                    lista.Add(aux);
                }

                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public void modificar(Paciente paciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Pacientes SET Nombre = @nombre, Apellido = @apellido, FechaNacimiento = @fechaNacimiento, Dni = @dni, Email = @email, Telefono = @telefono, UrlImagen = @urlImagen WHERE Id = @id");
                datos.setearParametro("@nombre", paciente.Nombre);
                datos.setearParametro("@apellido", paciente.Apellido);
                datos.setearParametro("@fechaNacimiento", paciente.FechaNacimiento);
                //datos.setearParametro("@dni", paciente.Documento); // realmente queremos modificar este campo?
                datos.setearParametro("@email", paciente.Email);
                datos.setearParametro("@telefono", paciente.Telefono);
                if (paciente.UrlImagen != null)
                {
                    datos.setearParametro("@UrlImagen", paciente.UrlImagen);
                }
                else
                {
                    datos.setearParametro("@UrlImagen", DBNull.Value);
                }
                datos.setearParametro("@id", paciente.Id);

                datos.ejecutarAccion();

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
    }
}
