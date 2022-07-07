using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;

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
                    var query = context.Database.ExecuteSqlRaw($"MateriaAdd '{materia.Nombre}', {materia.Creditos}, {materia.Costo}, {materia.Semestre.IdSemestre} , '{materia.Descripcion}', '{materia.Horario.Turno}', {materia.Horario.Grupo.IdGrupo}, '{materia.Imagen}' ");

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
                    //materia.Semestre.IdSemestre = (materia.Semestre.IdSemestre == null) ? 0 : materia.Semestre.IdSemestre;
                    materia.Nombre = (materia.Nombre == null) ? "" : materia.Nombre;
                    materia.Descripcion = (materia.Descripcion == null) ? "" : materia.Descripcion;
                    var query = context.Materia.FromSqlRaw($"MateriaGetAll '{materia.Nombre}','{materia.Descripcion}'").ToList();

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
        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableMateria = new DataTable();

                        da.Fill(tableMateria);

                        if (tableMateria.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableMateria.Rows)
                            {
                                ML.Materia materia = new ML.Materia();
                                materia.Nombre = row[0].ToString(); 
                                materia.Creditos = byte.Parse(row[1].ToString());
                                materia.Costo = decimal.Parse(row[2].ToString());

                                materia.Semestre = new ML.Semestre();
                                materia.Semestre.IdSemestre = int.Parse(row[3].ToString());

                                //materia.Grupo = new ML.Grupo();
                                //materia.Grupo.Horario = row[4].ToString();

                                materia.Grupo.Plantel = new ML.Plantel();
                                //
                                materia.Grupo.Plantel.IdPlantel = int.Parse(row[5].ToString());

                                materia.Estatus = bool.Parse(row[4].ToString());

                                result.Objects.Add(materia);
                            }

                            result.Correct = true;

                        }
                       
                        result.Object = tableMateria;

                        if (tableMateria.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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
        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Materia materia in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (materia.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }
                    if (materia.Creditos.ToString() == "")
                    {
                        error.Mensaje += "Ingresar los creditos ";
                    }
                    if (materia.Costo.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el Costo ";
                    }
                    if (materia.Semestre.IdSemestre.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    //if (materia.Grupo.Horario == "")
                    //{
                    //    error.Mensaje += "Ingresar el horario ";
                    //}
                    //if (materia.Grupo.Plantel.IdPlantel.ToString() == "")
                    //{
                    //    error.Mensaje += "Ingresar el plantel ";
                    //}
                    if (materia.Estatus.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el status ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
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