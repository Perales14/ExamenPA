using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExamenPA.Modelo
{
    internal class CategoriaM
    {
        protected OleDbConnection conexion;

        public CategoriaM()
        {


        }

        public void abrir()
        {
            String direc = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            direc = Path.GetDirectoryName(direc);
            direc = Path.GetDirectoryName(direc);
            string ruta = Path.Combine(direc, "DataBasePA.accdb");
            conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ruta);
            conexion.Open();
        }

        public DataTable DatosTabla()
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr;
            DataTable dataTable = new DataTable();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT * FROM Categoria";
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
            cmd.CommandText = "INSERT INTO Categoria (id, nombre, descripcion) VALUES ('" + dr["id"] + "','" + dr["nombre"] + "','"  + dr["descripcion"] + "')";
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void modificar(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            cmd.CommandText = "UPDATE Categoria SET nombre=?,descripcion=? WHERE Id=?";
            cmd.Parameters.AddWithValue("nombre", dr["nombre"]);
            cmd.Parameters.AddWithValue("descripcion", dr["descripcion"]);
            cmd.Parameters.AddWithValue("Id", dr["Id"]);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

    }
}
