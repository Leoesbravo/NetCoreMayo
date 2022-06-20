using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();

            ML.Result result = BL.Materia.GetAll();
            ML.Result resultSemestre = BL.Semestre.GetAll();

            materia.Semestre.Semestres = resultSemestre.Objects;
            materia.Materias = result.Objects;

            return View(materia);
        }
        [HttpGet]
        public ActionResult Form()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = BL.Semestre.GetAll();
            ML.Result resultPlantel = BL.Plantel.GetAll();

            materia.Semestre = new ML.Semestre();
            materia.Semestre.Semestres = result.Objects;

            materia.Horario = new ML.Horario();
            materia.Horario.Grupo = new ML.Grupo();
            materia.Horario.Grupo.Plantel = new ML.Plantel();

            materia.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;

            return View(materia);
        }
        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            //obtengo la imagen
            IFormFile file = Request.Form.Files["IFImage"];

            //valido si traigo imagen
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                materia.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            ML.Result result = BL.Materia.Add(materia);
            if (result.Correct)
            {
                ViewBag.Message = "Se ha registrado la materia";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se ha podido registrar la materia";
                return PartialView("Modal");
            }
        }
        [HttpGet]
        public ActionResult UpdateStatus(int IdMateria)
        {
            ML.Result result = BL.Materia.GetById(IdMateria);

            if (result.Correct)
            {
                ML.Materia materia = new ML.Materia();
                materia = ((ML.Materia)result.Object);

                materia.Estatus = (materia.Estatus) ? false : true;

                result = BL.Materia.UpdateById(materia);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "El estatus se actualizo correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al actualizar el Estatus" + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un erro al actualizar el Estatus" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }

        public JsonResult GrupoGetByIdPlantel(int IdPlantel)
        {
            ML.Result result = BL.Grupo.GetByIdPlantel(IdPlantel);

            return Json(result.Objects);
        }
        //metodo para convertir a bytes la imagen
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
