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


            }

            return result;
        }
    }
}
