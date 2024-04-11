using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LlevandoMisCuentas.Controllers
{
    public class SalarioController : Controller
    {
        private SalarioBusiness _SalarioBusiness;
        public SalarioController()
        {
            this._SalarioBusiness = new SalarioBusiness();
        }

        public ActionResult Index(int idSalario)
        {
            Salario salario = _SalarioBusiness.GetById(idSalario);
            return View(salario);
        }

        public JsonResult GetByPeriodo(string periodo)
        {
            Salario salario = _SalarioBusiness.GetByPeriodo(int.Parse(periodo));
            if(salario == null)
            {
            return Json(new { idSalario = 0 }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { idSalario = salario.IdSalario }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Grabar(Salario salario, string periodo)
        {
            bool grabo = false;
            int idSalario = 0;

            Salario nuevoSalario = _SalarioBusiness.NuevoSalario(salario, periodo);
            if (nuevoSalario != null)
            {
                grabo = true;
                idSalario = nuevoSalario.IdSalario;
            }

            return Json(new { estado = grabo, idSalario = idSalario });
        }

        public JsonResult GetSueldoTotal(int idSalario)
        {
            Salario salario = _SalarioBusiness.GetById(idSalario);

            return Json(salario.Sueldo, JsonRequestBehavior.AllowGet);
        }
    }
}