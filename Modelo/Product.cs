﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPA.Modelo
{
    internal class Product
    {
        //conectar a la base de datos
        //y hacer consultas sobre ellas, las CRUD
        //Create, Read, Update, Delete
        //CRUD
        //usando Access
        //empezar la conecion
        String conectar = "@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBasePA.accdb";
        protected OleDbConnection conexion;

        public Product()
        {
            
            
        }

        public void abrir()
        {
            conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\luisg\source\repos\ExamenPA\DataBasePA.accdb");
            conexion.Open();
        }

        public DataTable DatosTabla()
        {
            abrir();    
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr;
            DataTable dataTable = new DataTable();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT * FROM Producto";
            dr = cmd.ExecuteReader();
            dataTable.Load(dr);
            conexion.Close();
            return dataTable;

        }

        public void insertar(DataRow dr)
        {
            abrir(); 
            OleDbCommand cmd = new OleDbCommand();            
            cmd.Connection = conexion;
            cmd.CommandText = "INSERT INTO Producto (id, nombre, precio, categoria, proveedor) VALUES ('" + dr["id"] + "','" + dr["nombre"] + "','" + dr["precio"] + "','" + dr["categoria"] + "','" + dr["proveedor"] + "')";
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void modificar(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();
            
            cmd.Connection = conexion;
            cmd.CommandText = "UPDATE Producto SET nombre=?, precio=?, categoria=?, proveedor=? WHERE Id=?";
            cmd.Parameters.AddWithValue("nombre", dr["nombre"]);
            cmd.Parameters.AddWithValue("precio", dr["precio"]);
            cmd.Parameters.AddWithValue("categoria", dr["categoria"]);
            cmd.Parameters.AddWithValue("proveedor", dr["proveedor"]);
            cmd.Parameters.AddWithValue("Id", dr["Id"]);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        


    }
}