﻿using ExamenPA.Modelo;
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
        public void id(String id)
        {
            modelo.id(id);
        }
        public DataTable Obtenerdatos()
        {
            return modelo.DatosTabla();
        }
    }
}
