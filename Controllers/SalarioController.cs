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
    public class SalarioController : Controller
    {
        private SalarioBusiness _SalarioBusiness;
        public SalarioController()
        {
            this._SalarioBusiness = new SalarioBusiness();
        }

        public ActionResult Index()
        {
            string idSalario = Request.Cookies["idSalario"]["Id"];
            if (idSalario != null)
            {
                Salario salario = _SalarioBusiness.GetById(int.Parse(idSalario));
                return View(salario);
            }
            else
            {
                return RedirectToAction("Index", "Usuario");
            }
        }

        public JsonResult GetByPeriodo(string periodo)
        {
            string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];
            Salario salario = _SalarioBusiness.GetByPeriodo(int.Parse(periodo), int.Parse(idUsuario));
            if (salario == null)
            {
                return Json(new { idSalario = 0 }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                HttpCookie cookie = new HttpCookie("idSalario");
                cookie["Id"] = salario.IdSalario.ToString(); // Puedes almacenar más información según tus necesidades
                Response.Cookies.Add(cookie);
            }
            return Json(new { idSalario = salario.IdSalario }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Grabar(Salario salario, string periodo)
        {
            string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];

            bool grabo = false;
            int idSalario = 0;

            Salario nuevoSalario = _SalarioBusiness.NuevoSalario(salario, periodo, int.Parse(idUsuario));
            if (nuevoSalario != null)
            {
                grabo = true;
                idSalario = nuevoSalario.IdSalario;
                HttpCookie cookie = new HttpCookie("idSalario");
                cookie["Id"] = idSalario.ToString(); // Puedes almacenar más información según tus necesidades
                Response.Cookies.Add(cookie);
            }

            return Json(new { estado = grabo, idSalario = idSalario });
        }

        public JsonResult GetSueldoTotal(int idSalario)
        {
            Salario salario = _SalarioBusiness.GetById(idSalario);

            return Json(salario.Sueldo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIngresoAnual()
        {
            string idUsuario = Request.Cookies["UsuarioSesion"]["Id"];

            DatosIngresosDTO totalAnual = _SalarioBusiness.GetDatosAnuales(int.Parse(idUsuario));

            return Json(totalAnual, JsonRequestBehavior.AllowGet);
        }
    }
}