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
    internal class Cliente
    {
        ClienteM modelo = new ClienteM();
        public Cliente()
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
        public String id(String nombre)
        {
            return modelo.id(nombre);
        }
        public DataTable Obtenerdatos()
        {
            return modelo.DatosTabla();
        }
        public List<string> clientes()
        {
            DataTable dt = modelo.DatosTabla();
            List<string> clientes = new List<string>();


            foreach (DataRow dr in dt.Rows)
            {
                clientes.Add(dr["nombre"].ToString());
            }

            return clientes;
        }
    }
}
