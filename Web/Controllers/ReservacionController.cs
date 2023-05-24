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
    public class ReservacionController : Controller
    {
        //private int AreaComun { get; set; }
        // GET: Reservacion
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                ViewBag.Title = "Lista de Reservaciones";
                return View(new ServiceReservacion().GetReservaciones());
            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        [CustomAuthorize((int)Roles.Residente)]
        public ActionResult MuestraHistorial()
        {
            try
            {
                ViewBag.Title = "Mis Reservaciones";
                return View(new ServiceReservacion().GetReservaciones(((Usuario)Session["Usuario"]).UsuarioID));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public PartialViewResult GetByEstado(string parametro)
        {
            IEnumerable<Reservacion> lista = null;
            IServiceReservacion _ServiceRubro = new ServiceReservacion();
            if (parametro.Equals("Todos"))
            {
                lista = _ServiceRubro.GetReservaciones();
            }
            else {
                lista = _ServiceRubro.GetReservacionByEstado(parametro);
            }            
            return PartialView("PartialReservacion", lista);
        }
        public ActionResult SeleccionaArea()
        {
            ViewBag.areas = listaAreasComunes();
            if (TempData.ContainsKey("noSelected"))
            {
                string mensaje = (string)TempData["noSelected"];
                ViewBag.noSelected = mensaje;
            }
            return View("");
        }

        [CustomAuthorize((int)Roles.Residente)]
        [HttpPost]
        public ActionResult SeleccionaAreaComun(int? area)
        {
            try
            {                
                if (area == null)
                {
                    TempData["noSelected"] = "Debe escoger un area";
                    return RedirectToAction("SeleccionaArea", "Reservacion");
                }
                if (ModelState.IsValid)
                {
                    int AreaComun = (int)area;
                    TempData["AreaID"] = AreaComun;
                    return RedirectToAction("Create","Reservacion");
                }
                else {

                    ViewBag.areas = listaAreasComunes();
                    return View();
                }
            }
            catch (Exception ex)
            {

                throw;
            }            
        }

        // GET: Reservacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reservacion/Create
        [CustomAuthorize((int)Roles.Residente)]
        public ActionResult Create()
        {
            if (TempData.ContainsKey("no_reservado"))
            {
                ViewBag.reserv = TempData["no_reservado"];
            }
            if (TempData.ContainsKey("errorDia"))
            {
                ViewBag.errorDia = (string)TempData["errorDia"];
            }
            if (TempData.ContainsKey("errorHora"))
            {
                ViewBag.errorHora = (string)TempData["errorHora"];
            }
            ViewBag.estados = estados();
            ViewBag.areas = listaAreasComunes();
            ViewBag.fechas = listaDisponibles();
            ViewBag.horas = listaHorasDisponibles();
            return View();
        }



        // POST: Reservacion/Create
        [HttpPost]
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

        // GET: Reservacion/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? idUsuario,int? idArea)
        {
            try
            {
                if (idUsuario == null || idArea == null)
                {
                    return RedirectToAction("Index");
                }
                TempData["Area"] = idArea;
                TempData["Usuario"] = idUsuario;
                Reservacion reserv = new ServiceReservacion().GetReservacionByID(Convert.ToInt32(idUsuario), Convert.ToInt32(idArea));
                if (reserv != null)
                {
                    ViewBag.estados = estados();
                    return View(reserv);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private SelectList listaAreasComunes(int UsuarioID = 0)
        {
            IServiceAreaComun service = new ServiceAreaComun();
            IEnumerable<AreaComun> lista = service.GetAreasComunes();
            return new SelectList(lista, "ID", "Descripcion", UsuarioID);
        }
        private SelectList estados()
        {
            List<string> lista = new List<string>();
            lista.Add("Aprobada");
            lista.Add("Reprobada");
            return new SelectList(lista);
        }

        private SelectList listaDisponibles()
        {
            List<int> lista = new List<int>();                   
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)+1; i++)
            {
                lista.Add(i);
            } 
            return new SelectList(lista);
        }

        private SelectList listaHorasDisponibles()
        {
            List<int> lista = new List<int>();
            for (int i = 7; i <= 21; i++)
            {
                lista.Add(i);
            }            
            return new SelectList(lista);
        }

        // POST: Reservacion/Edit/5
        [HttpPost]
        public ActionResult Save(Reservacion reservacion,int? dia, int? hora)
        {
            try
            {
                if (dia == null || hora == null)
                {
                    if (dia == null)
                    {
                        TempData["errorDia"] = "Debe seleccionar un dia para reservar";                        
                    }
                    if (hora == null)
                    {
                        TempData["errorHora"] = "Debe seleccionar un dia para reservar";
                    }
                    return RedirectToAction("Create", "Reservacion");
                }                
                TempData["dia"] = dia;
                string fecha = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" +((int)TempData["dia"])+" "+hora+":00:00.0000";
                int idArea = (int)TempData["AreaID"];
                int idUsuario = ((Usuario)Session["Usuario"]).UsuarioID;
                if (new ServiceReservacion().GetReservacion(DateTime.Parse(fecha), idArea, idUsuario) == null)
                {
                    ModelState.Remove("Fecha");
                    ModelState.Remove("Estado");
                    ModelState.Remove("AreaComunID");
                    ModelState.Remove("UsuarioID");
                    // TODO: Add update logic here
                    if (ModelState.IsValid)
                    {

                        reservacion.Fecha = DateTime.Parse(fecha);
                        reservacion.Estado = "En Proceso";
                        reservacion.UsuarioID = ((Usuario)Session["Usuario"]).UsuarioID;
                        reservacion.AreaComunID = (int)TempData["AreaID"];
                        new ServiceReservacion().Guardar(reservacion);
                        TempData["succes"] = Util.SweetAlertHelper.Mensaje("Reservacion Exitosa", "Reservacion realizada exitosamente", Util.SweetAlertMessageType.success);
                        return RedirectToAction("MuestraInformacion", "Informacion");
                    }
                    else
                    {
                        ViewBag.estados = estados();
                        ViewBag.fechas = listaDisponibles();
                        ViewBag.horas = listaHorasDisponibles();
                        return View("Create", reservacion);
                    }                
                }
                else {
                    TempData["no_reservado"] = Util.SweetAlertHelper.Mensaje("Reservacion Denegada", "La fecha o la hora escogida ya han sido reservadas", Util.SweetAlertMessageType.error);
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                return RedirectToAction("MuestraInformacion", "Informacion");
            }
        }

        [HttpPost]
        public ActionResult Save2(Reservacion reservacion)
        {
            try
            {                
                int idArea = (int)TempData["Area"];
                int idUsuario = (int)TempData["Usuario"];
                
                ModelState.Remove("Fecha");
                ModelState.Remove("AreaComunID");
                ModelState.Remove("UsuarioID");
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    reservacion.UsuarioID = (int)TempData["Usuario"];
                    reservacion.AreaComunID = (int)TempData["Area"];
                    new ServiceReservacion().Guardar(reservacion);
                    return RedirectToAction("Index", "Reservacion");
                }
                else
                {
                    ViewBag.estados = estados();
                    ViewBag.fechas = listaDisponibles();
                    ViewBag.horas = listaHorasDisponibles();
                    return View("Create", reservacion);
                }
            }
            catch
            {
                return RedirectToAction("MuestraInformacion", "Informacion");
            }
        }

        // GET: Reservacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservacion/Delete/5
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
