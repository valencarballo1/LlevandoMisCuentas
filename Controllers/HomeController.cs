using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LlevandoMisCuentas.Controllers
{
    public class HomeController : Controller
    {
        private UsuarioBusiness _UsuarioBusiness;
        public HomeController()
        {
            this._UsuarioBusiness = new UsuarioBusiness();
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
    }
}