using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryIncidencia : IRepositoryIncidencia
    {
        public Incidencia GetIncidenciaByID(int ID)
        {
            Incidencia estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Incidencia.Where(e => e.ID == ID)
                        .Include("Usuario")
                        .FirstOrDefault();
                }
                return estado;
            }
            catch (DbUpdateException exDB)
            {
                string mensaje = "";
                Log.Error(exDB, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public IEnumerable<Incidencia> GetIncidencias()
        {
            IEnumerable<Incidencia> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Incidencia.Include("Usuario").ToList();
                }
                return lista;
            }
            catch (DbUpdateException exDB)
            {
                string mensaje = "";
                Log.Error(exDB, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public void Guardar(Incidencia incidencia)
        {
            try
            {
                int retorno = 0;
                Incidencia incid = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (GetIncidenciaByID(incidencia.ID) == null)
                    {
                        incidencia.Estado = "En Proceso";
                        ctx.Incidencia.Add(incidencia);
                    }
                    else
                    {
                        incid = GetIncidenciaByID(incidencia.ID);
                        incid.Estado = incidencia.Estado;
                        //ctx.Incidencia.Add(incid);
                        ctx.Entry(incid).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                    retorno = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException exDB)
            {
                string mensaje = "";
                Log.Error(exDB, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }
    }
}
