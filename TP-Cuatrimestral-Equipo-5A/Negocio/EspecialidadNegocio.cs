using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Dominio;

namespace Negocio
{
    public class EspecialidadNegocio
    {
        public List<Especialidad> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Especialidad> lista = new List<Especialidad>();
            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM Especialidades;");
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Especialidad especialidad = new Especialidad();
                    especialidad.Id = (int)datos.Lector["Id"];
                    especialidad.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(especialidad);
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


        public List<Especialidad> listarPorIdMedico(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Especialidad> lista = new List<Especialidad>();
            try
            {
                datos.setearConsulta("SELECT E.Id , E.Descripcion as Especialidad FROM EspecialidadesPorMedico EPM INNER JOIN Especialidades E on EPM.IdEspecialidad = E.Id WHERE EPM.IdMedico = @idMedico;");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Especialidad nuevo = new Especialidad();
                    nuevo.Id = (int)datos.Lector["Id"];
                    nuevo.Descripcion = (string)datos.Lector["Especialidad"];
                    lista.Add(nuevo);
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

        public void Agregar(Especialidad nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Especialidades (Descripcion) VALUES (@descripcion)");
                datos.setearParametro("@descripcion", nueva.Descripcion);

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

        public void Modificar(Especialidad esp)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Especialidades SET Descripcion = @descripcion WHERE Id = @id");
                datos.setearParametro("@descripcion", esp.Descripcion);
                datos.setearParametro("@id", esp.Id);

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

        public Especialidad BuscarPorId(int id)
        {
            Especialidad aux = new Especialidad();
            AccesoDatos datos = new AccesoDatos();
           

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM Especialidades WHERE Id = @id");
                datos.setearParametro("@id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                   
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return aux;
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM Especialidades WHERE Id = @id");
                datos.setearParametro("@id", id);

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

        public List<Especialidad> listarEspecialidadesFaltantes(int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Especialidad> lista = new List<Especialidad>();
            try
            {
                datos.setearConsulta("SELECT E.Id, E.Descripcion FROM Especialidades E WHERE E.Id NOT IN (SELECT IdEspecialidad FROM EspecialidadesPorMedico  WHERE IdMedico = @idMedico);");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Especialidad nuevo = new Especialidad();
                    nuevo.Id = (int)datos.Lector["Id"];
                    nuevo.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(nuevo);
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
    }
}
