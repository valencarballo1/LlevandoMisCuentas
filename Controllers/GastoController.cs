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
            return View();
        }

        public ActionResult MisGastos(int idSalario)
        {
            List<DetalleGastoDTO> gastos = _GastoBusiness.GetDetalleGasto(idSalario);
            return PartialView(gastos);
        }

        public JsonResult GetAllTipoGasto()
        {
            List<TipoGastoDTO> lista = _GastoBusiness.GetAllTipoGasto();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AgregarGasto(Gasto gasto)
        {
            bool grabo = _GastoBusiness.Grabar(gasto);
            return Json(grabo);
        }

        public JsonResult ObtenerDatosGrafico(int idSalario)
        {
            var tiposDeGasto = new List<string>();
            var cantidadDeGastos = new List<decimal>();
            List<TipoGastoDTO> lista = _GastoBusiness.GetGastosByIdSalario(idSalario);

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
            bool grabo = _GastoBusiness.GrabarTipoGasto(nombreTipoGasto);
            return Json(grabo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTotalGastos(int idSalario)
        {
            decimal gastoTotal = _GastoBusiness.GetTotalGastosBySalario(idSalario);
            return Json(gastoTotal, JsonRequestBehavior.AllowGet);
        }
    }
}