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
    public class RepositoryEstadoCuenta : IRepositoryEstadoCuenta
    {
        public EstadoCuenta GetEstadoCuentaByID(int ID)
        {
            EstadoCuenta estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.EstadoCuenta.Where(e => e.ID == ID)
                        .Include("Residencia")
                        .Include("Residencia.Usuario")
                        .Include("PlanCobro")                        
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

        public IEnumerable<EstadoCuenta> GetPagados(int ID)
        {
            IEnumerable<EstadoCuenta> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.EstadoCuenta.Where(e => e.Residencia.ID == ID && e.Estado == "1")
                        .Include("Residencia")
                        .Include("Residencia.Usuario")
                        .Include("PlanCobro")
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

        public IEnumerable<EstadoCuenta> GetPendientes(int ID)
        {
            IEnumerable<EstadoCuenta> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.EstadoCuenta.Where(e => e.Residencia.ID == ID && e.Estado == "2")
                        .Include("Residencia")
                        .Include("Residencia.Usuario")
                        .Include("PlanCobro")
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

        public IEnumerable<EstadoCuenta> GetPendientes()
        {
            IEnumerable<EstadoCuenta> estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.EstadoCuenta.Where(e => e.Estado == "2")
                        .Include("Residencia")
                        .Include("Residencia.Usuario")
                        .Include("PlanCobro")
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

        public IEnumerable<EstadoCuenta> GetEstados()
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EstadoCuenta.Include("Residencia")
                    .Include("Residencia.Usuario")
                    .Include("PlanCobro")
                    .ToList();
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

        public EstadoCuenta getDetallePagado(int ID)
        {
            EstadoCuenta estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.EstadoCuenta.Where(e => e.ID == ID && e.Estado == "1")
                        .Include("Residencia")
                        .Include("PlanCobro")
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

        public EstadoCuenta getDetallePendiente(int ID)
        {
            EstadoCuenta estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.EstadoCuenta.Where(e => e.ID == ID && e.Estado == "2")
                        .Include("Residencia")
                        .Include("PlanCobro")
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

        public void Guardar(EstadoCuenta estadoCuenta)
        {
            try
            {
                int retorno = 0;
                using (MyContext ctx = new MyContext())
                {                    
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (estadoCuenta.ResidenciaID != 0)
                    {
                        ctx.EstadoCuenta.Add(estadoCuenta);
                    }
                    else {
                        EstadoCuenta estado = GetEstadoCuentaByID(estadoCuenta.ID);
                        estado.Estado = estadoCuenta.Estado;
                        ctx.Entry(estado).State = EntityState.Modified;
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

        public EstadoCuenta GetEstadoCuentaByFecha(DateTime fecha, int UsuarioID)
        {
            EstadoCuenta estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.EstadoCuenta.Where(e => e.Mes.Month == fecha.Month && e.ResidenciaID == UsuarioID)
                        .Include("PlanCobro")
                        .Include("Residencia")
                        .Include("Residencia.Usuario")
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

        public IEnumerable<EstadoCuenta> GetEstadosByMes(int Mes)
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EstadoCuenta.Where(e => e.Mes.Month == Mes)
                    .Include("Residencia")
                    .Include("Residencia.Usuario")
                    .Include("PlanCobro")
                    .ToList();
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

        public Residencia GetEstadoByUser(int UsuarioID)
        {
            Residencia estado = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    estado = ctx.Residencia.Where(e => e.UsuarioID == UsuarioID)
                        .Include("Usuario")
                        .Include("EstadoCuenta")
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

        public IEnumerable<EstadoCuenta> GetEstadosByResid(int Resid)
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EstadoCuenta.Where(e => e.ResidenciaID == Resid)
                    .Include("Residencia")
                    .Include("Residencia.Usuario")
                    .Include("PlanCobro")
                    .ToList();
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

        public void GetOrdenCountDate(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            String varSuma = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    string[] Meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"};
                    var resultado = ctx.EstadoCuenta.GroupBy(x => x.Mes.Month).
                               Select(o => new {
                                   Count = o.Count(), 
                                   MesIngreso = o.Key,
                                   Suma = o.Sum(x => x.Monto)
                               });
                    foreach (var item in resultado)
                    {
                        for (int i = 0; i < Meses.Length; i++)
                        {
                            if (item.MesIngreso == i+1)
                            {
                                varEtiquetas += Meses[i]+":\t";
                                varEtiquetas += "₡"+item.Suma+",";
                            }
                        }                        
                        varValores += item.Count + ",";
                        varSuma += item.Suma + ",";
                    }
                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public IEnumerable<EstadoCuenta> GetEstadosByMesPendientes(int Mes)
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.EstadoCuenta.Where(e => e.Mes.Month == Mes && e.Estado == "2")
                    .Include("Residencia")
                    .Include("Residencia.Usuario")
                    .Include("PlanCobro")
                    .ToList();
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
