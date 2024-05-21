using Business;
using Data;
using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace LlevandoMisCuentas.Controllers
{
    public class DineroController : Controller
    {
        private DineroBusiness _DineroBusiness;

        public DineroController()
        {
            this._DineroBusiness = new DineroBusiness();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetBilleteras()
        {
            List<TipoBilleteraDTO> lista = _DineroBusiness.GetBilleteras();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTipoBilleteraBySalario(int idSalario)
        {
            List<TipoBilleteraDTO> lista = _DineroBusiness.GetTipoBilleteraBySalario(idSalario);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NuevoSalarioBilletera(string nombrePlataforma, decimal dineroPlataforma, int idSalario)
        {
            bool creo = _DineroBusiness.NuevoSalarioBilletera(nombrePlataforma, dineroPlataforma, idSalario);
            return Json(creo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPlataformas(int idSalario)
        {
            List<TipoBilleteraDTO> lista = _DineroBusiness.GetTipoBilleteraBySalario(idSalario);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Transferir(int idSalario, int idPlataformaEntrante, int idPlataformaSaliente, decimal dineroPlataforma)
        {
            bool grabo = _DineroBusiness.Transferir(idSalario, idPlataformaEntrante, idPlataformaSaliente, dineroPlataforma);
            return Json(grabo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Vaciar(int idSalario, int idSalarioBilletera)
        {
            bool grabo = _DineroBusiness.VaciarBilletera(idSalario, idSalarioBilletera);
            return Json(grabo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDineroDisponible(int idSalario)
        {
            decimal dineroDisponible = _DineroBusiness.GetDineroDisponible(idSalario);
            return Json(dineroDisponible, JsonRequestBehavior.AllowGet);
        }
    }
}