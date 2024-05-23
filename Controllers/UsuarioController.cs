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
    public class UsuarioController : Controller
    {
        private UsuarioBusiness _UsuarioBusiness;

        public UsuarioController()
        {
            this._UsuarioBusiness = new UsuarioBusiness();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Login(Usuario usuario)
        {
            UsuarioDTO usuarioExiste = _UsuarioBusiness.Login(usuario);

            if (usuarioExiste != null)
            {
                HttpCookie cookie = new HttpCookie("UsuarioSesion");
                cookie["Id"] = usuarioExiste.Id.ToString(); // Puedes almacenar más información según tus necesidades
                Response.Cookies.Add(cookie);
            }
            return Json(usuarioExiste, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CerrarSesion()
        {
            if (Request.Cookies["UsuarioSesion"] != null)
            {
                var cookie = new HttpCookie("UsuarioSesion")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                Response.Cookies.Add(cookie);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Registrarme(UsuarioRegistroDTO usuario)
        {
            UsuarioDTO registro = _UsuarioBusiness.Registrarme(usuario);
            return Json(registro);
        }
    }
}