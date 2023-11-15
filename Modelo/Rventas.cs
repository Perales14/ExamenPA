using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenPA.Controlador;
using System.IO;
using System.Windows;

namespace ExamenPA.Modelo
{
    internal class Rventas
    {
        String conectar = "@Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBasePA.accdb";
        protected OleDbConnection conexion;
        Almacen alma = new Almacen();
        

        public Rventas()
        {


        }

        public void crear()
        {
            String direc = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            direc = Path.GetDirectoryName(direc);
            direc = Path.GetDirectoryName(direc);
            string rutaArchivo = Path.Combine(direc, "Ventas", "ventas.txt");
            string ruta = Path.Combine(direc, "Ventas");
            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }
            if (!File.Exists(rutaArchivo))
            {
                using (StreamWriter sw = File.CreateText(rutaArchivo)) { }
            }
        }
        
        public DataTable buscaridventa(String idventa)
        {
            DataTable data = alma.DatosTabla();
            DataRow dr = data.NewRow();

            

            String direc = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            direc = Path.GetDirectoryName(direc);
            direc = Path.GetDirectoryName(direc);
            string rutaArchivo = Path.Combine(direc, "Ventas", "ventas.txt");
            string[] lineas = File.ReadAllLines(rutaArchivo);
            MessageBox.Show(rutaArchivo);
            foreach (string linea in lineas)
            {
                string[] elementos = linea.Split(',');

                // Verifica si la línea comienza con el ID buscado
                if (elementos.Length > 0 && elementos[0].Trim() == idventa)
                {
                    // Comenzamos desde 1 para saltar el ID
                    for (int i = 1; i < elementos.Length; i += 2)
                    {
                        // Asegúrate de que haya un par cantidad-ID de producto
                        if (i + 1 < elementos.Length)
                        {
                            abrir();
                            OleDbCommand cmd = new OleDbCommand();

                            cmd.Connection = conexion;
                            cmd.CommandText = "SELECT * From Almacen WHERE id=?";
                            cmd.Parameters.AddWithValue("id", idventa);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    dr["id"] = Convert.ToInt32(reader["id"]);
                                    dr["nombre"] = reader["nombre"].ToString();
                                    dr["precio"] = Convert.ToDecimal(reader["precio"]);
                                    dr["stock"]= Convert.ToInt32(reader["stock"]);
                                    dr["categoria"] = reader["categoria"].ToString();
                                    dr["proveedor"]= reader["proveedor"].ToString();
                                    data.Rows.Add(dr);
                                    // Ahora tienes los valores de cada columna para la fila con el ID específico
                                    // Puedes utilizar estos valores como necesites
                                }
                            }

                            //"SELECT id From WHERE nombre=?";
                            //cantidades.Add(Convert.ToInt32(elementos[i].Trim()));
                            //productos.Add(elementos[i + 1].Trim());
                            //agregar a data el datarow  dr
                            
                        }
                    }
                }
            }

            dr.Table.Columns["stock"].ColumnName = "cantidad";

            return data;
            

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
