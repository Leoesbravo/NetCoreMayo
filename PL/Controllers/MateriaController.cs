using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();

            materia.Nombre = (materia.Nombre == null) ? "" : materia.Nombre;
            materia.Semestre.IdSemestre = (materia.Semestre.IdSemestre == null) ? 0 : materia.Semestre.IdSemestre;

            ML.Result result = BL.Materia.GetAll(materia);
            ML.Result resultSemestre = BL.Semestre.GetAll();

            materia.Semestre.Semestres = resultSemestre.Objects;
            materia.Materias = result.Objects;

            return View(materia);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Materia materia)
        {

            //materia.Semestre = new ML.Semestre();
            materia.Nombre = (materia.Nombre == null) ? "" : materia.Nombre;
            materia.Semestre.IdSemestre = (materia.Semestre.IdSemestre == null) ? 0 : materia.Semestre.IdSemestre;

            ML.Result result = BL.Materia.GetAll(materia);
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
            if (ModelState.IsValid)
            {
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
            else
            {
                ML.Result result = BL.Semestre.GetAll();
                ML.Result resultPlantel = BL.Plantel.GetAll();

                materia.Semestre.Semestres = result.Objects;

                materia.Horario.Grupo.Plantel.Planteles = resultPlantel.Objects;

                return View(materia);
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

        [HttpPost]
        public ActionResult CargaMasiva()
        {
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            try
            {
                IFormFile archivo = Request.Form.Files["Archivo"];
                using (StreamReader sr = new StreamReader(archivo.OpenReadStream()))
                {
                    string line;
                    line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] datos = line.Split('|');

                        ML.Materia materia = new ML.Materia();
                        materia.Nombre = datos[0];
                        materia.Costo = decimal.Parse(datos[1]);
                        materia.Creditos = byte.Parse(datos[2]);
                        materia.Descripcion = datos[3];
                        materia.Semestre = new ML.Semestre();
                        materia.Semestre.IdSemestre = int.Parse(datos[4]);
                        materia.Estatus = bool.Parse(datos[5]);

                        ML.Result result = BL.Materia.Add(materia);

                        if (!result.Correct) //si el resultado es diferente a correcto
                        {
                            resultErrores.Objects.Add(
                                "No se inserto el Nombre " + materia.Nombre +
                                "No se inserto el Costo " + materia.Costo +
                                "No se inserto el Creditos" + materia.Creditos +
                                "No se inserto el Descripcion" + materia.Descripcion);
                        } //Se le asigna agrega la lista de errores



                    }

                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return PartialView("ValidationModal");
        }

        //public ActionResult Download()
        //{
        //    string file = HttpContext.Session.GetString["RutaDescarga"];
        //    string contentType = "text/plain";
        //    return File(file, contentType, Path.GetFileName(file));
        //}
    }
}
