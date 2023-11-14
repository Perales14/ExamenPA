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
    internal class Proveedor
    {
        Provedor modelo = new Provedor();

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

        public List<string> proveedores()
        {
            DataTable dt = modelo.DatosTabla();
            List<string> proveedores = new List<string>();


            foreach (DataRow dr in dt.Rows)
            {
                proveedores.Add(dr["nombre"].ToString());
            }

            return proveedores;
        }
    }
}
