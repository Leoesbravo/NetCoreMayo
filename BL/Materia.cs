using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Materia
    {
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoGenMayoContext context = new DL.LEscogidoGenMayoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MateriaAdd '{materia.Nombre}', {materia.Costo}, {materia.Creditos}, '{materia.Descripcion}' , {materia.Semestre.IdSemestre}, {materia.Estatus} ");

                    if (query >= 1)
                    {
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
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.LEscogidoGenMayoContext context = new DL.LEscogidoGenMayoContext())
                {
                    var obj = context.Materia.FromSqlRaw($"MateriaGetById {IdMateria}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {

                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = obj.IdMateria;
                        materia.Nombre = obj.Nombre;
                        materia.Creditos = obj.Creditos.Value;
                        materia.Costo = obj.Costo.Value;
                        materia.Descripcion = obj.Descripcion;
                        materia.Estatus = obj.Estatus.Value;

                        materia.Semestre = new ML.Semestre();
                        materia.Semestre.IdSemestre = obj.IdSemestre.Value;

                        result.Object = materia;

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
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetAll(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (DL.LEscogidoGenMayoContext context = new DL.LEscogidoGenMayoContext())

                {       
                    var query = context.Materia.FromSqlRaw($"MateriaGetAll {materia.Semestre.IdSemestre},'{materia.Nombre}'").ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Creditos = obj.Creditos.Value;
                            materia.Costo = obj.Costo.Value;
                            materia.Descripcion = obj.Descripcion;
                            materia.Estatus = obj.Estatus.Value;

                            materia.Semestre = new ML.Semestre();
                            materia.Semestre.IdSemestre = obj.IdSemestre.Value;

                            result.Objects.Add(materia);
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

        public static ML.Result UpdateById(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.LEscogidoGenMayoContext context = new DL.LEscogidoGenMayoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MateriaUpdate {materia.IdMateria}, '{materia.Nombre}', {materia.Costo}, {materia.Creditos}, '{materia.Descripcion}' , {materia.Semestre.IdSemestre}, {materia.Estatus} ");

                    if (query >= 1)
                    {
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
                result.Ex = ex;
            }

            return result;
        }
    }
}