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
    public class RepositoryResidencia : IRepositoryResidencia
    {
        public Residencia GetResidenciaByID(int ID)
        {
            Residencia resid = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    resid = ctx.Residencia.Where(p => p.ID == ID).Include("Usuario").FirstOrDefault();
                }
                return resid;
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

        public IEnumerable<Residencia> GetResidencias()
        {
            IEnumerable<Residencia> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Residencia.Include("Usuario").ToList();
                }
                return lista;
            }
            catch (DbUpdateException exDB)
            {
                string mensaje = "";
                Log.Error(exDB,System.Reflection.MethodBase.GetCurrentMethod(),ref mensaje);
                throw new Exception(mensaje);                
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);

            }
        }

        public void Guardar(Residencia res)
        {
            try
            {
                int retorno = 0;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (GetResidenciaByID(res.ID) == null)
                    {                        
                        ctx.Residencia.Add(res);                        
                    }
                    else
                    {
                        ctx.Entry(res).State = EntityState.Modified;                       
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
