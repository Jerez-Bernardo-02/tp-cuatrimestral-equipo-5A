using Datos;
using Dominio;
using System;
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
                datos.setearConsulta(@"INSERT INTO Medicos (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono, Matricula) output inserted.Id VALUES (@nombre, @apellido,@fechaNacimiento, @dni, @email, @telefono, @matricula)");

                //seteamos parametros  (@Clave, valor) - activo = true por constructor
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@fechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametro("@dni", nuevo.Documento);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", nuevo.Telefono);
                datos.setearParametro("@matricula", nuevo.Matricula);

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
    }
}
