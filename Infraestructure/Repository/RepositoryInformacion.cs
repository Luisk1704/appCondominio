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
    public class RepositoryInformacion : IRepositoryInformacion
    {
        public IEnumerable<Informacion> GetAvisos()
        {
            IEnumerable<Informacion> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //estado = ctx.Informacion.Where(e => e.Titulo == "Avisos")
                    //    .Include("Usuario")
                    //    .ToList();
                    estado = ctx.Informacion.Where(e => e.Titulo == "Avisos"
                    && e.Fecha.Day <= DateTime.Now.Day && e.Fecha.Day >= DateTime.Now.Day - 4)
                        .Include("Usuario")
                        .ToList();
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

        public IEnumerable<Informacion> GetDocumentos()
        {
            IEnumerable<Informacion> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //estado = ctx.Informacion.Where(e => e.Titulo == "Archivos Documentales")
                    //    .Include("Usuario")
                    //    .ToList();
                    estado = ctx.Informacion.Where(e => e.Titulo == "Archivos Documentales" 
                    && e.Fecha.Day <= DateTime.Now.Day && e.Fecha.Day >= DateTime.Now.Day-4)
                        .Include("Usuario")
                        .ToList();
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

        public IEnumerable<Informacion> GetInformacion()
        {
            IEnumerable<Informacion> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Informacion.Include("Usuario").ToList();
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

        public Informacion GetInformacionByID(int ID)
        {
            Informacion estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Informacion.Where(e => e.ID == ID)
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

        public IEnumerable<Informacion> GetNoticias()
        {
            IEnumerable<Informacion> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //estado = ctx.Informacion.Where(e => e.Titulo == "Noticias")
                    //    .Include("Usuario")
                    //    .ToList();

                    estado = ctx.Informacion.Where(e => e.Titulo == "Noticias"
                    && e.Fecha.Day <= DateTime.Now.Day && e.Fecha.Day >= DateTime.Now.Day - 4)
                        .Include("Usuario")
                        .ToList();
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

        public void Guardar(Informacion info)
        {            
            try
            {
                int retorno = 0;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (GetInformacionByID(info.ID) == null)
                    {
                        ctx.Informacion.Add(info);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.Informacion.Add(info);
                        ctx.Entry(info).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
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
