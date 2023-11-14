using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenPA.Modelo;
namespace ExamenPA.Modelo
{
    internal class Provedor
    {
        String conectar = "@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBasePA.accdb";
        protected OleDbConnection conexion;

        public Provedor()
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
            cmd.CommandText = "SELECT * FROM Proveedor";
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
            cmd.CommandText = "INSERT INTO Proveedor (id, nombre, telefono) VALUES ('" + dr["id"] + "','" + dr["nombre"] + "','" +  dr["telefono"] + "')";
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void modificar(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            cmd.CommandText = "UPDATE Proveedor SET nombre=?, telefono=? WHERE Id=?";
            cmd.Parameters.AddWithValue("nombre", dr["nombre"]);
            cmd.Parameters.AddWithValue("telefono", dr["telefono"]);
            cmd.Parameters.AddWithValue("Id", dr["Id"]);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }
    }
}
