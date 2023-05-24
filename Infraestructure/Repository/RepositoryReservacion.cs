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
    public class RepositoryReservacion : IRepositoryReservacion
    {
        public Reservacion GetReservacion(DateTime fecha, int idArea, int idUsuario)
        {
            Reservacion estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Reservacion.Where(e => e.Fecha == fecha 
                    && e.AreaComunID == idArea && e.UsuarioID == idUsuario)
                        .Include("AreaComun")
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

        public Reservacion GetReservacionByDate(DateTime fecha)
        {
            Reservacion estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Reservacion.Where(e => e.Fecha == fecha)
                        .Include("AreaComun")
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

        public IEnumerable<Reservacion> GetReservacionByEstado(string estado)
        {
            IEnumerable<Reservacion> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Reservacion.Where(e => e.Estado == estado).Include("AreaComun")
                    .Include("Usuario").ToList();
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

        public Reservacion GetReservacionByID(int UsuarioID,int AreaComunID)
        {
            Reservacion estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Reservacion.Where(e => e.UsuarioID == UsuarioID 
                    && e.AreaComunID == AreaComunID).Include("AreaComun")
                    .Include("Usuario").FirstOrDefault();
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

        public IEnumerable<Reservacion> GetReservaciones()
        {
            IEnumerable<Reservacion> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Reservacion.Include("AreaComun")
                    .Include("Usuario").ToList();
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

        public IEnumerable<Reservacion> GetReservaciones(int UsuarioID)
        {
            IEnumerable<Reservacion> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Reservacion.Where(e => e.UsuarioID == UsuarioID).Include("AreaComun")
                    .Include("Usuario").ToList();
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

        public Reservacion GetReservacionesByHour(int hora, int id)
        {
            Reservacion estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Reservacion.Where(e => e.Fecha.Hour == hora
                    && e.AreaComunID == id).Include("AreaComun")
                    .Include("Usuario").FirstOrDefault();
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

        public IEnumerable<Reservacion> GetReservacionesByMonth(int mes, int id)
        {
            IEnumerable<Reservacion> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Reservacion.Where(e => e.Fecha.Month == mes
                    && e.AreaComunID == id).Include("AreaComun")
                    .Include("Usuario").ToList();
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

        public void Guardar(Reservacion reservacion)
        {
            int retorno = 0;
            Reservacion reserv = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (GetReservacionByID(reservacion.UsuarioID, reservacion.AreaComunID) == null)
                    {
                        ctx.Reservacion.Add(reservacion);
                    }
                    else {
                        reserv = GetReservacionByID(reservacion.UsuarioID,reservacion.AreaComunID);
                        reserv.Estado = reservacion.Estado;
                        //ctx.Reservacion.Add(reserv);
                        ctx.Entry(reserv).State = EntityState.Modified;
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
