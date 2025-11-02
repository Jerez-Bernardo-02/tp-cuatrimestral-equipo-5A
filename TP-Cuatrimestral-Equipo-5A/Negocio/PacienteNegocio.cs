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
                datos.setearConsulta(@"INSERT INTO Personas (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono) output inserted.Id VALUES (@nombre, @apellido,@fechaNacimiento, @dni, @email, @telefono)");
                
                //seteamos parametros  (@Clave, valor) - activo = true por constructor
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@fechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametro("@dni", nuevo.Documento);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", nuevo.Telefono);

                //para obtener el id autogenerado en la BD de Persona
                int idPersona = datos.ejecutarEscalar();
                nuevo.Id = idPersona;

                //Creo el paciente en la BD con el idPersona anterior
                datos.setearConsulta(@"INSERT INTO Pacientes (IdPersona) output inserted.Id VALUES (@idPersona)");

                //seteamos parametros  (@Clave, valor) 
                datos.setearParametro("@idPersona", nuevo.Id);

                //obtengo y devuelvo el idPaciente autogenerado en la BD por si necesito utilizarlo a futuro
                int idPaciente = datos.ejecutarEscalar();
                nuevo.IdPaciente = idPaciente;

                return nuevo.IdPaciente;
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
                    aux.Activo = (bool)datos.Lector["Activo"];

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
    }
}
