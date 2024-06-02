using Business;
using Data;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LlevandoMisCuentas.Controllers
{
    public class GastoController : Controller
    {
        private GastoBusiness _GastoBusiness;
        public GastoController()
        {
            this._GastoBusiness = new GastoBusiness();
        }
        public ActionResult Index()
        {
            if (Request.Cookies["UsuarioSesion"] != null)
            {
                string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }

        public ActionResult MisGastos()
        {
            string idSalario = Request.Cookies["idSalario"]["Id"];
            List<DetalleGastoDTO> gastos = _GastoBusiness.GetDetalleGasto(int.Parse(idSalario));
            return PartialView(gastos);
        }

        public JsonResult GetAllTipoGasto()
        {
            string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];
            List<TipoGastoDTO> lista = _GastoBusiness.GetAllTipoGasto(int.Parse(idUsuario));
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AgregarGasto(Gasto gasto)
        {
            bool grabo = _GastoBusiness.Grabar(gasto);
            return Json(grabo);
        }

        public JsonResult ObtenerDatosGrafico()
        {
            string idSalario = Request.Cookies["idSalario"]["Id"];

            var tiposDeGasto = new List<string>();
            var cantidadDeGastos = new List<decimal>();
            List<TipoGastoDTO> lista = _GastoBusiness.GetGastosByIdSalario(int.Parse(idSalario));

            lista.ForEach(l =>
            {
                tiposDeGasto.Add(l.NombreGasto);
                cantidadDeGastos.Add(l.TotalGasto);
            });
            

            var random = new Random();
            var backgroundColors = Enumerable.Range(0, tiposDeGasto.Count)
                                             .Select(_ => $"rgba({random.Next(0, 255)}, {random.Next(0, 255)}, {random.Next(0, 255)}, 0.6)")
                                             .ToArray();

            var data = new
            {
                labels = tiposDeGasto,
                datasets = new[]
                {
            new
            {
                data = cantidadDeGastos,
                backgroundColor = backgroundColors
            }
                }
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NuevoTipoGasto(string nombreTipoGasto)
        {
            string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];

            bool grabo = _GastoBusiness.GrabarTipoGasto(nombreTipoGasto, int.Parse(idUsuario));
            return Json(grabo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalGastos()
        {
            string idSalario = Request.Cookies["idSalario"]["Id"];
            decimal gastoTotal = _GastoBusiness.GetTotalGastosBySalario(int.Parse(idSalario));
            return Json(gastoTotal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Anual()
        {
            if (Request.Cookies["UsuarioSesion"] != null)
            {
                string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }

        public JsonResult GetGastoAnual()
        {
            string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];

            DatosAnualesDTO totalAnual = _GastoBusiness.GetDatosAnuales(int.Parse(idUsuario));

            return Json(totalAnual, JsonRequestBehavior.AllowGet);
        }
    }
}