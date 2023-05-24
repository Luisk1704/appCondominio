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
    public class RepositoryPlanCobro : IRepositoryPlanCobro
    {
        public IEnumerable<PlanCobro> GetplanCobro()
        {
            IEnumerable<PlanCobro> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.PlanCobro.ToList();
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

        public PlanCobro GetPlanCobroByID(int ID)
        {
            PlanCobro plan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    plan = ctx.PlanCobro.Where(p => p.ID == ID).Include("RubroCobro").FirstOrDefault();
                }
                return plan;
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

        public void Guardar(PlanCobro plan, string[] listaRubros)
        {
            try
            {
                int retorno = 0;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (GetPlanCobroByID(plan.ID) == null)
                    {
                        if (listaRubros != null)
                        {
                            foreach (var item in listaRubros)
                            {
                                var rubroAdd = new RepositoryRubroCobro()
                                .GetRubroByID(Convert.ToInt32(item));
                                ctx.RubroCobro.Attach(rubroAdd);
                                plan.RubroCobro.Add(rubroAdd);
                            }
                        }
                        ctx.PlanCobro.Add(plan);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.PlanCobro.Add(plan);
                        ctx.Entry(plan).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        var selectedCategoriasID = new HashSet<string>(listaRubros);
                        if (listaRubros != null)
                        {
                            ctx.Entry(plan).Collection(p => p.RubroCobro).Load();
                            var newCategoriaForLibro = ctx.RubroCobro
                             .Where(x => selectedCategoriasID.Contains(x.ID.ToString())).ToList();
                            plan.RubroCobro = newCategoriaForLibro;

                            ctx.Entry(plan).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }
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
