using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenPA.Modelo
{
    internal class Rventas
    {
        String conectar = "@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBasePA.accdb";
        protected OleDbConnection conexion;

        public Rventas()
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
            cmd.CommandText = "SELECT * FROM Reporte";
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
            string estado = (Boolean)dr["estado"] ? "verdadero" : "falso";
            cmd.CommandText = "INSERT INTO Reporte (id, Fecha,descuento,IDcliente, cliente, Total, Estado) VALUES ('" + dr["id"] + "','" + dr["Fecha"] + "','" + dr["descuento"] + "','" + dr["IDcliente"] + "','" + dr["cliente"] + "','" + dr["Total"] + "','" + estado  + "')";
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void modificar(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            string estado = (Boolean)dr["estado"] ? "verdadero" : "falso";
            cmd.CommandText = "UPDATE Reporte SET Fecha=?, descuento=?, IDcliente=?, cliente=?, Total=?, Estado=? WHERE Id=?";
            cmd.Parameters.AddWithValue("Fecha", dr["Fecha"]);
            cmd.Parameters.AddWithValue("descuento", dr["descuento"]);
            cmd.Parameters.AddWithValue("IDcliente", dr["IDcliente"]);
            cmd.Parameters.AddWithValue("cliente", dr["cliente"]);
            cmd.Parameters.AddWithValue("Total", dr["Total"]);
            cmd.Parameters.AddWithValue("Estado", estado); // Si el campo en la base de datos acepta "verdadero" o "falso"

            cmd.Parameters.AddWithValue("Id", dr["Id"]);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }
        public void anular(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            string estado = (Boolean)dr["estado"] ? "verdadero" : "falso";
            cmd.CommandText = "UPDATE Reporte SET Estado=? WHERE Id=?";
            cmd.Parameters.AddWithValue("Estado", estado); // Si el campo en la base de datos acepta "verdadero" o "falso"
            cmd.Parameters.AddWithValue("Id", dr["Id"]);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }
    }
}
