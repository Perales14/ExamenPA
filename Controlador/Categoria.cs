using ExamenPA.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenPA.Modelo;
namespace ExamenPA.Controlador
{
    internal class Categoria
    {
        CategoriaM modelo = new CategoriaM();
        public Categoria()
        {
        }

        public void Guardar(DataRow dr, Boolean nuevo)
        {
            //TODO: Guardar
            if (nuevo)
            {
                //si es nuevo, guardar
                modelo.insertar(dr);
            }
            else
            {
                //si no es nuevo, actualizar
                modelo.modificar(dr);
            }

        }
        public DataTable Obtenerdatos()
        {
            return modelo.DatosTabla();
        }
        public List<string> CategoriaM()
        {
            DataTable dt = modelo.DatosTabla();
            List<string> Categoria = new List<string>();


            foreach (DataRow dr in dt.Rows)
            {
                Categoria.Add(dr["nombre"].ToString());
            }

            return Categoria;
        }

    }
}
