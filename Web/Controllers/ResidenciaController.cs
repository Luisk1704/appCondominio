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
    public class ResidenciaController : Controller
    {
        // GET: Residencia
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IEnumerable<Residencia> lista = new ServiceResidencia().GetResidencias();
                ViewBag.tittle = "Lista de Residencias";
                return View(lista);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: Residencia/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int id)
        {
            Residencia residencia = null;
            try
            {
                residencia = new ServiceResidencia().GetResidenciaByID(id);
                return View(residencia);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Residencia/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.estados = estados();
            ViewBag.usuarios = listaUsuarios();
            return View();
        }

        // POST: Residencia/Create
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

        // GET: Residencia/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Residencia resid = new ServiceResidencia().GetResidenciaByID((int)id);
                if (resid != null)
                {
                    ViewBag.estados = estados();
                    ViewBag.usuarios = listaUsuarios();
                    return View(resid);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Residencia/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Residencia res)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    new ServiceResidencia().Guardar(res);
                }
                else
                {
                    ViewBag.estados = estados();
                    ViewBag.usuarios = listaUsuarios();
                    return View("Create", res);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Residencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        private SelectList estados()
        {
            var v = new { id = 1, nombre = "En construccion" };
            var v2 = new { id = 2, nombre = "Finalizada" };
            List<Object> lista = new List<Object>();
            lista.Add(v);
            lista.Add(v2);
            return new SelectList(lista, "id", "nombre");
        }

        private SelectList listaUsuarios(int UsuarioID = 0)
        {
            IServiceUsuario service = new ServiceUsuario();
            IEnumerable<Usuario> lista = service.GetUsuarios();
            return new SelectList(lista, "UsuarioID", "Nombre", UsuarioID);
        }
        // POST: Residencia/Delete/5
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
