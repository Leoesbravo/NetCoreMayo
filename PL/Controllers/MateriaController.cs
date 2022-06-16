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
        public JsonResult GrupoGetByIdPlantel(int IdPais)
        {
            ML.Result result = BL.Grupo.GetByIdPlantel(IdPais);

            return Json(result);
        }
    }
}
