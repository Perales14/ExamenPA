using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ExamenPA.Modelo;
namespace ExamenPA.Controlador
{
    internal class Producto
    {
        Product modelo = new Product();

        

        public void Guardar(DataRow dr, Boolean nuevo) {
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

        public List<string> productos()
        {
            DataTable dt = modelo.DatosTabla();
            List<string> productos = new List<string>();
            
            
            foreach(DataRow dr in dt.Rows)
            {
                productos.Add(dr["nombre"].ToString());
            }

            return productos;
        }



    }
}
