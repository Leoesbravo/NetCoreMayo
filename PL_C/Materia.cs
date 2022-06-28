using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Materia
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            string file = @"C:\Users\digis\source\repos\NetCoreMayo\LayOutMateria.txt";

            StreamReader archivo = new StreamReader(file);

            string line;
            bool isFirstLine = true;
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();
            while ((line = archivo.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    line = archivo.ReadLine();
                }

                Console.WriteLine(line);
                string[] datos = line.Split('|');

                ML.Materia materia = new ML.Materia();
                materia.Nombre = datos[0];
                materia.Costo = decimal.Parse(datos[1]);
                materia.Creditos = byte.Parse(datos[2]);
                materia.Descripcion = datos[3];
                materia.Semestre = new ML.Semestre();
                materia.Semestre.IdSemestre = int.Parse(datos[4]);
                materia.Estatus = bool.Parse(datos[5]);

                result = BL.Materia.Add(materia);

                if (!result.Correct) //si el resultado es diferente a correcto
                {
                    resultErrores.Objects.Add(
                        "No se inserto el Nombre " + materia.Nombre +
                        "No se inserto el Costo " + materia.Costo +
                        "No se inserto el Creditos" + materia.Creditos +
                        "No se inserto el Descripcion" + materia.Descripcion);
                } //Se le asigna agrega la lista de errores
            }

            if (resultErrores.Objects != null)
            {

            }

            TextWriter tw = new StreamWriter(@"C:\Users\digis\source\repos\NetCoreMayo\Errores.txt");
            foreach (string error in resultErrores.Objects)
            {
                tw.WriteLine(error); //Se le concatenan todos los errores
            }
            tw.Close();

            return result;
        }
    }
}
