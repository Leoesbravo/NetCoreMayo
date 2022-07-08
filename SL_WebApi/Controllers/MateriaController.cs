using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    public class MateriaController : ControllerBase
    {
        [HttpGet]
        [Route("api/materia/GetAll")]  
        public IActionResult GetAll()
        {

            ML.Materia materia = new ML.Materia();
            materia.Semestre = new ML.Semestre();

            ML.Result result = BL.Materia.GetAll(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPost]
        [Route("api/materia/add")]

        public IActionResult Add([FromBody] ML.Materia materia)
        {
            var result = BL.Materia.Add(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
