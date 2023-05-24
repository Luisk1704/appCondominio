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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (TempData.ContainsKey("fail"))
            {
                ViewBag.fail = TempData["fail"];
            }
            if (new ServiceUsuario().GetUsuario("admin@amores.com", "123") == null)
            {
                Usuario user = new Usuario();
                user.UsuarioID = 1;                
                user.TipoUsuarioID = 1;
                user.Nombre = "Luis";
                user.Apellido1 = "Amores";
                user.Apellido2 = "Villalobos";
                user.Correo = "admin@amores.com";
                user.Contrasenna = "123";
                user.Estado = 1;
                new ServiceUsuario().Save(user);
            }
            Session["Usuario"] = null;
            return View();
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult ListaUsuarios()
        {
            try
            {
                ViewBag.Title = "Lista de Usuarios";
                return View(new ServiceUsuario().GetUsuarios());
            }
            catch (Exception)
            {

                throw;
            }            
        }

        [HttpPost]
        public ActionResult Login(Usuario user) {
            Usuario usuario = null;
            try
            {        
                ModelState.Remove("TipoUsuarioID");
                ModelState.Remove("Nombre");
                ModelState.Remove("Apellido1");
                ModelState.Remove("Apellido2");
                ModelState.Remove("Estado");
                ModelState.Remove("Habilitado");                
                if (ModelState.IsValid)
                {
                    usuario = new ServiceUsuario().GetUsuario(user.Correo,user.Contrasenna);
                    if (usuario != null)
                    {
                        if (usuario.Estado == 2)
                        {
                            TempData["fail"] = Util.SweetAlertHelper.Mensaje("Error", "El usuario no existe o ha sido inabilitado", Util.SweetAlertMessageType.error);
                            return RedirectToAction("Index", "Login");
                        }
                        else {
                            Session["Usuario"] = usuario;
                            Log.Info($"Inicio sesion: {usuario.Correo}");
                            TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Inicio Sesion", "Usuario Autenticado", Util.SweetAlertMessageType.success);
                            return RedirectToAction("MuestraInformacion", "Informacion");
                        }                        
                    }
                    else {
                        //Log.Warn($"Intento de inicio: {usuario.Correo}");
                        TempData["fail"] = Util.SweetAlertHelper.Mensaje("Error", "Usuario o contraseña incorrectos", Util.SweetAlertMessageType.error);                        
                        return RedirectToAction("Index", "Login");
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("Index");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.Roles = listaRoles();
            ViewBag.estados = estados();
            return View();
        }

        // POST: Login/Create
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

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Usuario user = new ServiceUsuario().GetUsuarioByID(Convert.ToInt32(id));
                if (user != null)
                {
                    ViewBag.Roles = listaRoles();
                    ViewBag.estados = estados();
                    return View(user);
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

        private SelectList listaRoles(int tipo = 0)
        {
            IServiceTipoUsuario service = new ServiceTipoUsuario();
            IEnumerable<TipoUsuario> lista = service.GetRoles();
            return new SelectList(lista, "ID", "Descripcion", tipo);
        }
        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Save(Usuario usuario)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    new ServiceUsuario().Save(usuario);
                }
                else
                {
                    ViewBag.Roles = listaRoles(usuario.TipoUsuarioID);
                    ViewBag.estados = estados();
                    return View("Create", usuario);
                }
                return RedirectToAction("ListaUsuarios");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult UnAuthorized()
        {
            ViewBag.tittle = "Acceso no Autorizado";
            return View();
        }

        // POST: Login/Delete/5
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
