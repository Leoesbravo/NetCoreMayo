using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    [Route("api/Materia")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll")]  
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
        [Route("Add")]

        public IActionResult Post([FromBody] ML.Materia materia)
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
