using ExamenPA.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExamenPA.Controlador
{
    internal class ReporteVentas
    {
        Rventas modelo = new Rventas();

        public void Guardar(DataRow dr)
        {
                modelo.insertar(dr);

        }
        public DataTable Obtenerdatos()
        {
            return modelo.DatosTabla();
        }
        public void anular(DataRow dr)
        {
            modelo.anular(dr);
        }
        public DataTable productos(String id)
        {
            MessageBox.Show("modelo");
            return modelo.buscaridventa(id); 
        }

        public void guardaridventa(String id, DataTable dr)
        {
            modelo.guardaridventa(id, dr);
        }
    }
}
