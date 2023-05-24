using ApplicationCore.Services;
using Infraestructure.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModel;

namespace Web.Controllers
{
    public class EstadoCuentaController : Controller
    {
        // GET: EstadoCuenta
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                lista = new ServiceEstadoCuenta().GetEstadoCuentas();
                ViewBag.Tittle = "Estados de Cuenta";
                ViewBag.Meses = fechasFiltros();
                ViewBag.resid = listaResid();
                return View(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult ReporteEstados()
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                lista = new ServiceEstadoCuenta().GetPendientes();
                ViewBag.Tittle = "Estados de Cuenta";
                ViewBag.Meses = fechasFiltros();
                ViewBag.resid = listaResid();
                return View(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public PartialViewResult FiltroFecha(int? id)
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                if (id != null)
                {
                    if ((int)id == 0)
                    {
                        lista = new ServiceEstadoCuenta().GetEstadoCuentas();
                    }
                    else {
                        lista = new ServiceEstadoCuenta().GetEstadosByMes((int)id);                        
                    }
                }
                ViewBag.Tittle = "Estados de Cuenta";
                return PartialView("FiltroFecha",lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public PartialViewResult FiltroPendMes(int? id)
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                if (id != null)
                {
                    if ((int)id == 0)
                    {
                        lista = new ServiceEstadoCuenta().GetPendientes();
                    }
                    else
                    {
                        lista = new ServiceEstadoCuenta().GetEstadosByMesPendientes((int)id);
                    }
                }
                ViewBag.Tittle = "Estados de Cuenta";
                ViewBag.Mes = (int)id;
                return PartialView("FiltroPendMes", lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PartialViewResult FiltroPendResid(int? id)
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                if (id != null)
                {
                    lista = new ServiceEstadoCuenta().GetPendientes((int)id);
                }
                ViewBag.Tittle = "Estados de Cuenta";
                ViewBag.resid = (int)id;
                return PartialView("FiltroPendResid", lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public PartialViewResult FiltroResid(int? id)
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                if (id != null)
                {
                    lista = new ServiceEstadoCuenta().GetEstadosByResid((int)id);
                }
                ViewBag.Tittle = "Estados de Cuenta";
                return PartialView("FiltroFecha", lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public PartialViewResult FiltroPendientes()
        {
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                lista = new ServiceEstadoCuenta().GetPendientes();
                ViewBag.Tittle = "Estados de Cuenta";
                return PartialView("FiltroFecha", lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Reportes)]
        public ActionResult CreatePdfDeudas(int? id)
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<EstadoCuenta> lista = null;
            try
            {
                // Extraer informacion
                //IServiceLibro _ServiceLibro = new ServiceLibro();
                lista = new ServiceEstadoCuenta().GetPendientes();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Lista de Pagos Pendientes")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);


                // Crear tabla con 5 columnas 
                Table table = new Table(4, true);
                // los Encabezados
                table.AddHeaderCell("Usuario");
                table.AddHeaderCell("Mes");
                table.AddHeaderCell("PlanCobro");
                table.AddHeaderCell("Monto");

                decimal suma = 0;

                foreach (var item in lista)
                {
                    suma += item.Monto;
                }

                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.Residencia.Usuario.Nombre+"\t"+ item.Residencia.Usuario.Apellido1));
                    table.AddCell(new Paragraph(item.Mes.ToString()));
                    table.AddCell(new Paragraph(item.PlanCobro.Descripcion));
                    table.AddCell(new Paragraph(item.Monto.ToString()));
                    // Convierte la imagen que viene en Bytes en imagen para PDF                    
                }
                table.AddCell("");
                table.AddCell("");
                table.AddCell(new Paragraph("Total: ").SetFontSize(14));
                table.AddCell(new Paragraph(suma.ToString()).SetFontSize(14));
                doc.Add(table);



                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte.pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult graficoEstados()
        {
            //Documentación chartjs https://www.chartjs.org/docs/latest/
            ViewModelGrafico grafico = new ViewModelGrafico();
            new ServiceEstadoCuenta().GetOrdenCountDate(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Ingresos por mes";
            grafico.tituloEtiquetas = "";
            //Tipos: bar , bubble , doughnut , pie , line , polarArea 
            grafico.tipo = "doughnut";
            ViewBag.grafico = grafico;
            return View();
        }

        private SelectList estados()
        {
            var v = new { id = 1, nombre = "Pagada" };
            var v2 = new { id = 2, nombre = "Pendiente" };
            List<Object> lista = new List<Object>();
            lista.Add(v);
            lista.Add(v2);
            return new SelectList(lista, "id", "nombre");
        }

        // GET: EstadoCuenta/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int id)
        {
            EstadoCuenta detalle = null;
            try
            {
                detalle = new ServiceEstadoCuenta().GetEstadoCuentaByID(id);
                ViewBag.pagados = listaPagados(detalle.ResidenciaID);
                ViewBag.pendientes = listaPendientes(detalle.ResidenciaID);
                ViewBag.Tittle = "Detalle Estado Cuenta";
                return View(detalle);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [CustomAuthorize((int)Roles.Residente)]
        public ActionResult MiEstado(int id)
        {
            Residencia detalle = null;
            try
            {
                detalle = new ServiceEstadoCuenta().GetEstadoByUser(id);
                ViewBag.pagados = listaPagados(detalle.ID);
                ViewBag.pendientes = listaPendientes(detalle.ID);
                ViewBag.Tittle = "Mi Estado de Cuenta";
                return View(detalle);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private SelectList fechasFiltros()
        {
            var v = new { id = 01, nombre = "Enero" };
            var v2 = new { id = 02, nombre = "Febrero" };
            var v3= new { id = 03, nombre = "Marzo" };
            var v4 = new { id = 04, nombre = "Abril" };
            var v5= new { id = 05, nombre = "Mayo" };
            var v6 = new { id = 06, nombre = "Junio" };
            var v7= new { id = 07, nombre = "Julio" };
            var v8 = new { id = 08, nombre = "Agosto" };
            var v9= new { id = 09, nombre = "Septiembre" };
            var v10 = new { id = 10, nombre = "Octubre" };
            var v11 = new { id = 11, nombre = "Noviembre" };
            var v12 = new { id = 12, nombre = "Diciembre" };

            List<Object> lista = new List<Object>();

            lista.Add(v);
            lista.Add(v2);
            lista.Add(v3);
            lista.Add(v4);
            lista.Add(v5);
            lista.Add(v6);
            lista.Add(v7);
            lista.Add(v8);
            lista.Add(v9);
            lista.Add(v10);
            lista.Add(v11);
            lista.Add(v12);

            return new SelectList(lista, "id", "nombre");
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        public ActionResult Pagado(int id)
        {
            EstadoCuenta pagado = null;
            try
            {
                pagado = new ServiceEstadoCuenta().GetdetallePagados(id);
                ViewBag.Tittle = "Pagado";
                return View(pagado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private SelectList listaPlanesCobro()
        {
            IServicePlanCobro service = new ServicePlanCobro();
            IEnumerable<PlanCobro> lista = service.GetPlanCobros();
            return new SelectList(lista, "ID", "Descripcion");
        }

        private SelectList listaUsuarios()
        {
            List<Object> lista = new List<Object>();
            foreach (var item in new ServiceResidencia().GetResidencias())
            {
                var v = new { id = item.ID, nombre = item.Usuario.Nombre +
                    "\t"+ item.Usuario.Apellido1+"\t"+ item.Usuario.Apellido2};
                lista.Add(v);
            }
            return new SelectList(lista, "id", "nombre");
        }

        private SelectList listaResid()
        {
            List<Object> lista = new List<Object>();
            foreach (var item in new ServiceResidencia().GetResidencias())
            {
                var v = new
                {
                    id = item.ID,
                    nombre = item.ID
                };
                lista.Add(v);
            }
            return new SelectList(lista, "id", "nombre");
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        public ActionResult Pendiente(int id)
        {
            EstadoCuenta pendiente = null;
            try
            {
                pendiente = new ServiceEstadoCuenta().GetdetallePendientes(id);
                ViewBag.Tittle = "Pendiente";
                return View(pendiente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<EstadoCuenta> listaPendientes(int ID) {
            return new ServiceEstadoCuenta().GetPendientes(ID);
        }
        private IEnumerable<EstadoCuenta> listaPagados(int ID)
        {
            return new ServiceEstadoCuenta().GetPagados(ID);
        }
        // GET: EstadoCuenta/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            if (TempData.ContainsKey("error"))
            {
                ViewBag.error = TempData["error"];
            }
            ViewBag.estados = estados();
            ViewBag.usuarios = listaUsuarios();
            ViewBag.planes = listaPlanesCobro();
            return View();
        }

        // POST: EstadoCuenta/Create
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

        // GET: EstadoCuenta/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                EstadoCuenta incid = new ServiceEstadoCuenta().GetEstadoCuentaByID(Convert.ToInt32(id));
                if (id != null)
                {
                    TempData["idEstado"] = (int)id;
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

        // POST: EstadoCuenta/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(EstadoCuenta estadoCuenta)
        {
            try
            {
                if (estadoCuenta.ResidenciaID != 0)
                {
                    ModelState.Remove("Monto");
                    if (ModelState.IsValid)
                    {
                        if (new ServiceEstadoCuenta().GetEstadoCuentaByFecha(estadoCuenta.Mes, estadoCuenta.ResidenciaID) == null)
                        {
                            estadoCuenta.Monto = new ServicePlanCobro().GetPlanCobroByID(estadoCuenta.PlanCobroID).TotalMes;
                            new ServiceEstadoCuenta().Guardar(estadoCuenta);
                        }
                        else
                        {
                            TempData["error"] = Util.SweetAlertHelper.Mensaje("Error", "Ya se asigno esta residencia en el mes seleccionado", Util.SweetAlertMessageType.error);
                            return RedirectToAction("Create");
                        }
                    }
                    else
                    {
                        ViewBag.estados = estados();
                        ViewBag.usuarios = listaUsuarios();
                        ViewBag.planes = listaPlanesCobro();
                        return View("Create", estadoCuenta);
                    }
                }
                else {
                    ModelState.Remove("ResidenciaID");
                    ModelState.Remove("PlanCobroID");
                    ModelState.Remove("Monto");
                    ModelState.Remove("Mes");
                    if (ModelState.IsValid)
                    {
                        estadoCuenta.ID = (int)TempData["idEstado"];
                        new ServiceEstadoCuenta().Guardar(estadoCuenta);                        
                    }
                    else
                    {
                        ViewBag.estados = estados();
                        ViewBag.usuarios = listaUsuarios();
                        ViewBag.planes = listaPlanesCobro();
                        return View("Create", estadoCuenta);
                    }
                }                
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: EstadoCuenta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadoCuenta/Delete/5
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
