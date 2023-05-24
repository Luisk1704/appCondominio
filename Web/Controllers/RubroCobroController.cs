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
    public class RubroCobroController : Controller
    {
        // GET: RubroCobro
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                ViewBag.Tittle = "Rubros de Cobro";
                return View(new ServiceRubroCobro().GetRubros());
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        // GET: RubroCobro/Details/5

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int id)
        {
            try
            {
                ViewBag.Tittle = "Detalles de Rubro";
                return View(new ServiceRubroCobro().GetRubroByID(id));
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        // GET: RubroCobro/Create

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.estados = estados();
            return View();
        }

        // POST: RubroCobro/Create
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

        // GET: RubroCobro/Edit/5

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                RubroCobro rubro = new ServiceRubroCobro().GetRubroByID(Convert.ToInt32(id));
                if (rubro != null)
                {
                    ViewBag.estados = estados();
                    return View(rubro);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
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

        // POST: RubroCobro/Edit/5
        [HttpPost]
        public ActionResult Save(RubroCobro rubro)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    new ServiceRubroCobro().Guardar(rubro);
                }
                else {
                    ViewBag.estados = estados();
                    return View("Create",rubro);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public PartialViewResult RubrosXLetra(char? letra)
        {
            IEnumerable<RubroCobro> lista = null;
            IServiceRubroCobro _ServiceRubro = new ServiceRubroCobro();
            if (letra != null)
            {
                lista = _ServiceRubro.GetRubrosByLetra((char)letra);
            }
            else {
                lista = _ServiceRubro.GetRubros();
            }
            return PartialView("PartialLetra", lista);
        }

        // GET: RubroCobro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RubroCobro/Delete/5
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
