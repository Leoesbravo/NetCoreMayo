using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Plantel
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoGenMayoContext context = new DL.LEscogidoGenMayoContext())

                {
                    var query = context.Plantels.FromSqlRaw($"PlantelGetAll").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Plantel plantel = new ML.Plantel();
                            plantel.IdPlantel = obj.IdPlantel;
                            plantel.Nombre = obj.Nombre;

                            result.Objects.Add(plantel);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
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
