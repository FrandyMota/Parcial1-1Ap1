using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Parcial1_1AP1.DAL;
using Parcial1_1AP1.Entidades;

namespace Parcial1_1AP1.BLL
{
    class EstudiantesBLL
    {
        public static bool Guardar(Estudiantes evaluacion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Estudiantes.Add(evaluacion) != null)
                {

                    int ItemSeleccionado = EstudiantesBLL.SeleccionarPronostico(evaluacion);
                    evaluacion.Pronostico = ItemSeleccionado;

                    paso = db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Estudiantes evaluacion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(evaluacion).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Estudiantes.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static Estudiantes Buscar(int id)
        {
            Estudiantes evaluacion = new Estudiantes();
            Contexto db = new Contexto();

            try
            {
                evaluacion = db.Estudiantes.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return evaluacion;
        }

        public static List<Estudiantes> GetList(Expression<Func<Estudiantes, bool>> evaluacion)
        {
            List<Estudiantes> Lista = new List<Estudiantes>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Estudiantes.Where(evaluacion).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }

        public static int SeleccionarPronostico(Estudiantes evaluacion)
        {
            int ItemSeleccionado = 0;
            decimal auxiliar;
            decimal resultado;

            auxiliar = (evaluacion.Logrado / evaluacion.Valor) * 100;
            resultado = 100 - auxiliar;

            if (resultado < 25)
                ItemSeleccionado = 0;
            else if (resultado >= 25 && resultado <= 30)
                ItemSeleccionado = 1;
            else if (resultado > 25)
                ItemSeleccionado = 2;

            return ItemSeleccionado;
        }
    }
 }

