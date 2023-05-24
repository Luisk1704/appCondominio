using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class InformacionController : Controller
    {
        // GET: Informacion
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                if (Session["Usuario"] == null)
                {
                    return RedirectToAction("Login","Login");
                }
                IEnumerable<Informacion> lista = new ServiceInformacion().GetInformacion();
                ViewBag.title = "Lista Informacion";
                return View(lista);
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        public ActionResult MuestraInformacion()
        {
            try
            {
                IEnumerable<Informacion> listaInfo = new ServiceInformacion().
                GetInformacion();
                if (TempData.ContainsKey("mensaje"))
                {
                    ViewBag.mensaje = TempData["mensaje"];
                }
                if (TempData.ContainsKey("fail"))
                {
                    ViewBag.fail = TempData["fail"];
                }                
                if (TempData.ContainsKey("succes"))
                {
                    ViewBag.succes = TempData["succes"];
                }
                if (TempData.ContainsKey("incid"))
                {
                    ViewBag.incid = TempData["incid"];
                }
                List<Informacion> lista = new List<Informacion>();
                foreach (var item in listaInfo)
                {
                    if (item.ID <= listaInfo.Count() && item.ID > listaInfo.Count()-4)
                    {
                        lista.Add(item);
                    }
                }
                ViewBag.Noticias = Noticias();
                ViewBag.Avisos = Avisos();
                ViewBag.Documentos = Documentos();
                ViewBag.title = "Lista Informacion";
                TempData["mensaje"] = null;
                return View(lista);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Informacion/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int id)
        {
            try
            {
                Informacion info = new ServiceInformacion().GetInformacionByID(id);
                ViewBag.title = "Detalles de Informacion";
                return View(info);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: Informacion/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            //ids de usuarios
            ViewBag.estados = estados();
            ViewBag.informacion = tipoInfo();
            return View();
        }

        // POST: Informacion/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Informacion/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Informacion info = new ServiceInformacion().GetInformacionByID(Convert.ToInt32(id));
                if (info != null)
                {
                    ViewBag.estados = estados();
                    ViewBag.informacion = tipoInfo();
                    return View(info);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Informacion/Edit/5
        [HttpPost]
        public ActionResult Save(Informacion info)
        {
            try
            {
                IServiceInformacion service = new ServiceInformacion();
                ModelState.Remove("Fecha");
                if (ModelState.IsValid)
                {
                    info.UsuarioID = ((Usuario)Session["Usuario"]).UsuarioID;
                    info.Fecha = DateTime.Now;
                    service.Guardar(info);
                }
                else {                    
                    ViewBag.estados = estados();
                    ViewBag.informacion = tipoInfo();
                    // TODO: Add update logic here
                    return View("Create",info);
                }
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        private SelectList listaUsuarios(int UsuarioID = 0) {
            IServiceUsuario service = new ServiceUsuario();
            IEnumerable<Usuario> lista = service.GetUsuarios();
            return new SelectList(lista,"UsuarioID","Nombre",UsuarioID);
        }

        private SelectList estados()
        {
            var v = new { id = 1, nombre = "activo" };
            var v2 = new { id = 2, nombre = "inactivo" };
            List<Object> lista = new List<Object>();
            lista.Add(v);
            lista.Add(v2);
            return new SelectList(lista,"id","nombre");
        }

        private SelectList tipoInfo() {
            List<string> lista = new List<string>();
            lista.Add("Noticias");
            lista.Add("Avisos");
            lista.Add("Archivos Documentales");
            return new SelectList(lista);
        }

        private IEnumerable<Informacion> Noticias() {
            return new ServiceInformacion().GetNoticias();
        }

        private IEnumerable<Informacion> Avisos()
        {
            return new ServiceInformacion().GetAvisos();
        }

        private IEnumerable<Informacion> Documentos()
        {
            return new ServiceInformacion().GetDocumentos();
        }

        // GET: Informacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Informacion/Delete/5
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
