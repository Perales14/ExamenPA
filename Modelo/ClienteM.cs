using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.IO;

namespace ExamenPA.Modelo
{
    internal class ClienteM
    {
        //conectar a la base de datos
        //y hacer consultas sobre ellas, las CRUD
        //Create, Read, Update, Delete
        //CRUD
        //usando Access
        //empezar la conecion
        //String conectar = "@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBasePA.accdb";
        protected OleDbConnection conexion;

        public ClienteM()
        {


        }

        public void abrir()
        {
            //String ax = "C:\Users\luis\source\repos\ExamenPA\DataBasePA.accdb";
            String direc = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            direc = Path.GetDirectoryName(direc);
            direc = Path.GetDirectoryName(direc);
            string ruta = Path.Combine(direc, "DataBasePA.accdb");
            conexion = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ruta);
            conexion.Open();
        }

        public DataTable DatosTabla()
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr;
            DataTable dataTable = new DataTable();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT * FROM Clientes";
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
            cmd.CommandText = "INSERT INTO Clientes (id, nombre) VALUES ('" + dr["id"] + "','" + dr["nombre"]  + "')";
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void modificar(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            cmd.CommandText = "UPDATE Clientes SET nombre=? WHERE Id=?";
            cmd.Parameters.AddWithValue("nombre", dr["nombre"]);
            cmd.Parameters.AddWithValue("Id", dr["Id"]);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public String id (String nombre)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            cmd.CommandText = "SELECT id From Clientes WHERE nombre=?";
            cmd.Parameters.AddWithValue("nombre", nombre);
            //cmd.ExecuteNonQuery();
            object resultado = cmd.ExecuteScalar();
            String id="";
            if (resultado != null)
            {
                id = Convert.ToString(resultado);
                conexion.Close();
            } else
            {
                return "";
            }

            return id;
        }

        
    }
}
