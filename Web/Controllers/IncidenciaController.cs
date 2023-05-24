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
    public class IncidenciaController : Controller
    {
        // GET: Incidencia
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                ViewBag.tittle = "Lista de Incidencias";
                return View(new ServiceIncidencia().GetIncidencias());
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        // GET: Incidencia/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int id)
        {
            try
            {
                ViewBag.tittle = "Detalles de Incidencia";
                return View(new ServiceIncidencia().GetIncidenciaByID(id));
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        // GET: Incidencia/Create
        [CustomAuthorize((int)Roles.Residente)]
        public ActionResult Create()
        {
            ViewBag.estados = estados();
            return View();
        }

        // POST: Incidencia/Create
        [HttpPost]
        [CustomAuthorize((int)Roles.Residente)]
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

        // GET: Incidencia/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            try
            {                
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Incidencia incid = new ServiceIncidencia().GetIncidenciaByID(Convert.ToInt32(id));
                if (id != null) {
                    ViewBag.estados = estados();
                    return View(incid);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        private SelectList listaUsuarios(int UsuarioID = 0)
        {
            IServiceUsuario service = new ServiceUsuario();
            IEnumerable<Usuario> lista = service.GetUsuarios();
            return new SelectList(lista, "UsuarioID", "Nombre", UsuarioID);
        }
        // POST: Incidencia/Edit/5
        [HttpPost]
        public ActionResult Save(Incidencia incidencia)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    new ServiceIncidencia().Guardar(incidencia);
                }
                else if (incidencia.Titulo != null) {
                    incidencia.UsuarioID = ((Usuario)Session["Usuario"]).UsuarioID;
                    new ServiceIncidencia().Guardar(incidencia);
                    TempData["incid"] = Util.SweetAlertHelper.Mensaje("Incidencia reportada", "Incidencia reportada exitosamente", Util.SweetAlertMessageType.success);                    
                }
                else if (incidencia.Estado != null)
                {                    
                    new ServiceIncidencia().Guardar(incidencia);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.estados = estados();
                    return View("Create", incidencia);
                }
                return RedirectToAction("MuestraInformacion", "Informacion");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        private SelectList estados()
        {
            List<string> lista = new List<string>();
            lista.Add("Finalizada");
            lista.Add("En Proceso");
            return new SelectList(lista);
        }

        // GET: Incidencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Incidencia/Delete/5
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
