using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class PlanCobroController : Controller
    {
        // GET: PlanCobro
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<PlanCobro> lista = new ServicePlanCobro().GetPlanCobros();
                ViewBag.tittle = "Planes de Cobro";
                return View(lista);
            }
            catch (Exception)
            {

                throw;
            }            
        }

        // GET: PlanCobro/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int ID)
        {
            PlanCobro planCobro = null;
            try
            {
                planCobro = new ServicePlanCobro().GetPlanCobroByID(ID);
                return View(planCobro);
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        // GET: PlanCobro/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.Rubros = listaRubrosCobro();
            ViewBag.estados = estados();
            return View();
        }

        // POST: PlanCobro/Create
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PlanCobro/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                PlanCobro plan = new ServicePlanCobro().GetPlanCobroByID(Convert.ToInt32(id));
                if (plan != null)
                {
                    ViewBag.Rubros = listaRubrosCobro(plan.RubroCobro);
                    ViewBag.estados = estados();
                    return View(plan);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        // POST: PlanCobro/Edit/5
        [HttpPost]
        public ActionResult Save(PlanCobro plan, string[] listaRubros)
        {
            try
            {
                decimal suma = 0;
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    foreach (var item in listaRubros)
                    {
                        suma += new ServiceRubroCobro().GetRubroByID(Convert.ToInt32(item)).Costo;
                    }
                    plan.TotalMes = suma;
                    new ServicePlanCobro().Guardar(plan,listaRubros);
                }
                else {
                    ViewBag.Rubros = listaRubrosCobro(plan.RubroCobro);
                    ViewBag.estados = estados();
                    return View("Create",plan);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        private MultiSelectList listaRubrosCobro(ICollection<RubroCobro> rubros = null) {
            int[] listaSelect = null;
            if (rubros != null)
            {
                listaSelect = rubros.Select(r => r.ID).ToArray();
            }
            return new MultiSelectList(new ServiceRubroCobro().GetRubros(),
                "ID","Nombre",listaSelect);
        }

        private SelectList estados()
        {
            var v = new { id = 1, nombre = "activo" };
            var v2 = new { id = 2, nombre = "inactivo" };
            List<Object> lista = new List<Object>();
            lista.Add(v);
            lista.Add(v2);
            return new SelectList(lista, "id", "nombre");
        }

        // GET: PlanCobro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlanCobro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
