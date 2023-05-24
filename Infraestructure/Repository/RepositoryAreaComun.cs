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
    public class RepositoryAreaComun : IRepositoryAreaComun
    {
        public AreaComun GetAreaComunByID(int ID)
        {
            AreaComun estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.AreaComun.Where(e => e.ID == ID)
                        .Include("Reservacion")
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

        public IEnumerable<AreaComun> GetAreasComunes()
        {
            IEnumerable<AreaComun> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.AreaComun.Include("Reservacion").ToList();
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

        public IEnumerable<Reservacion> GetReservacionesByID(int areaComunID)
        {
            IEnumerable<Reservacion> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Reservacion.Where(e => e.AreaComunID == areaComunID).ToList();
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
    }
}
