using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult GetAll()
        {
            //llamo al get all de aseguradora
            ML.Result result = BL.Usuario.UsuarioGetAllEF();

            //creo una instancia de aseguradora
            ML.Usuario usuario = new ML.Usuario();

            //paso de mi lista result.objects a mi lista de aseguradoras
            usuario.Usuarios = result.Objects;
            //aseguradora.Aseguradoras = result.Objects;
            return View(usuario);
            //return View();
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultRol = BL.Rol.RolAllEF();
            ML.Result resultPais = BL.Pais.GetAll();


            usuario.Rol = new ML.Rol();
            usuario.Rol.Roles = resultRol.Objects.ToList();
           usuario.Pais = new ML.Pais();
            usuario.Pais.Paises = resultPais.Objects.ToList();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio=new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

            // return View(usuario);

            if (IdUsuario == null)//ADD
            {

                return View(usuario);
            }
            else  //UPDATE
            {
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);

                if (result.Correct)
                {
                    //usuario = new ML.Usuario();
                    usuario = ((ML.Usuario)result.Object);
                    usuario.Rol = new ML.Rol();
                    usuario.Rol.Roles = resultRol.Objects.ToList();

                    return View(usuario);
                }
            }
            return View(usuario);

        }




        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            if (usuario.IdUsuario != 0)
            {
                result = BL.Usuario.UpdateEF(usuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el usuario";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se actualizo" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Usuario.AddEF(usuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado correctaente el producto";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ha ocurrido un error" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }



        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;

            ML.Result result = BL.Usuario.DeleteEF(usuario);
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return PartialView("Modal");
            }

        }
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.EstadoByIdPais(IdPais);

            return Json(result.Objects);
        }
    }
}
