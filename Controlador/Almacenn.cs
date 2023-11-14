using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenPA.Modelo;
namespace ExamenPA.Controlador
{
    internal class Almacenn
    {
        Almacen modelo = new Almacen();
        public Almacenn()
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


    }
}
