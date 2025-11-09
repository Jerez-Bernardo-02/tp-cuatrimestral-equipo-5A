using Datos;
using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MedicoNegocio
    {
        public int agregarMedico(Medico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Medicos (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono, Matricula, UrlImagen, IdUsuario) output inserted.Id VALUES (@Nombre, @Apellido,@FechaNacimiento, @Dni, @Email, @Telefono, @Matricula, @UrlImagen, @IdUsuario)");

                //seteamos parametros  (@Clave, valor) - activo = true por constructor
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@FechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametro("@Dni", nuevo.Dni);
                datos.setearParametro("@Email", nuevo.Email);
                if (string.IsNullOrEmpty(nuevo.Telefono))
                {
                    datos.setearParametro("@Telefono", DBNull.Value); //debido a que la DB acepta NULL. CONSULTAR CON EQUIPO
                }
                else
                {
                    datos.setearParametro("@Telefono", nuevo.Telefono);
                }
                datos.setearParametro("@Matricula", nuevo.Matricula);
                if (string.IsNullOrEmpty(nuevo.UrlImagen))
                {
                    datos.setearParametro("@UrlImagen", DBNull.Value);//debido a que la DB acepta NULL. CONSULTAR CON EQUIPO
                }
                else
                {
                    datos.setearParametro("@UrlImagen", nuevo.UrlImagen);
                }
                datos.setearParametro("@IdUsuario", nuevo.Usuario.Id);
                //para obtener el id autogenerado en la BD 
                int id = datos.ejecutarEscalar();
                nuevo.Id = id;

                return nuevo.Id;
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

        public void modificarMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Medicos SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Telefono = @Telefono, Dni = @Dni, Email = @Email, UrlImagen = @UrlImagen, Matricula = @Matricula WHERE Id = @Id;");
                datos.setearParametro("@Nombre", medico.Nombre);
                datos.setearParametro("@Apellido", medico.Apellido);
                datos.setearParametro("@FechaNacimiento", medico.FechaNacimiento);
                if (string.IsNullOrEmpty(medico.Telefono))
                {
                    datos.setearParametro("@Telefono", DBNull.Value); //debido a que la DB acepta NULL. CONSULTAR CON EQUIPO
                }
                else
                {
                    datos.setearParametro("@Telefono", medico.Telefono); 
                }
           
                datos.setearParametro("@Dni", medico.Dni);
                datos.setearParametro("@Email", medico.Email);
                if (string.IsNullOrEmpty(medico.UrlImagen))
                {
                    datos.setearParametro("@UrlImagen", DBNull.Value);//debido a que la DB acepta NULL. CONSULTAR CON EQUIPO
                }
                else
                {
                    datos.setearParametro("@UrlImagen", medico.UrlImagen);
                }
                datos.setearParametro("@Matricula", medico.Matricula);
                datos.setearParametro("@Id", medico.Id);


                datos.ejecutarAccion();


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

        public Medico buscarPorIdUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen, Matricula  FROM Medicos Where IdUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();
                Medico medico = null;
                if (datos.Lector.Read())
                {
                    medico = new Medico();
                    medico.Id = (int)datos.Lector["Id"];
                    medico.Nombre = (string)datos.Lector["Nombre"];
                    medico.Apellido = (string)datos.Lector["Apellido"];
                    medico.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    medico.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    medico.Dni = (string)datos.Lector["Dni"];
                    medico.Email = (string)datos.Lector["Email"];
                    medico.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                    medico.Matricula = (string)datos.Lector["Matricula"];
                }
                return medico;

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
    }
}
