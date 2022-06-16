using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Semestre
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LEscogidoGenMayoContext context = new DL.LEscogidoGenMayoContext())

                {
                    var query = context.Semestres.FromSqlRaw("SemestreGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Semestre semestre = new ML.Semestre();
                            semestre.IdSemestre = obj.IdSemestre;
                            semestre.Nombre = obj.Nombre;

                            result.Objects.Add(semestre);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
