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
            crear();

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
            data.Clear();
            

            String direc = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            direc = Path.GetDirectoryName(direc);
            direc = Path.GetDirectoryName(direc);
            string rutaArchivo = Path.Combine(direc, "Ventas", "ventas.txt");
            string[] lineas = File.ReadAllLines(rutaArchivo);
            String ln = "";
            foreach (string linea in lineas)
            {
                if (linea.Split(",")[0].Equals(idventa))
                {
                    ln = linea;
                    MessageBox.Show(ln);

                    break;
                }
                
            }
            if (ln.Equals(""))
            {
                return data;
            }
            string[] elementos = ln.Split(',');
            String nombre = "", cantidad = "";
            for(int i=1;i< elementos.Length; i += 2)//inicia del 1 para saltarse el idventa
            {
                nombre = elementos[i];
                cantidad = elementos[i + 1];
                Product controlp = new Product();
                dr = controlp.producto(nombre);
                dr["stock"] = cantidad;
                data.Rows.Add(dr);


            }
            

                dr.Table.Columns["stock"].ColumnName = "cantidad";

            return data;
            

        }

        public void guardaridventa(String nombre, DataTable data)
        {
            crear();
            String direc = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            direc = Path.GetDirectoryName(direc);
            direc = Path.GetDirectoryName(direc);
            string rutaArchivo = Path.Combine(direc, "Ventas", "ventas.txt");
            using (StreamWriter sw = File.AppendText(rutaArchivo))
            {
                sw.Write(nombre);
                foreach (DataRow dr in data.Rows)
                {
                    sw.Write("," + dr["nombre"] + "," + dr["cantidad"]);
                }
                sw.WriteLine("");
                sw.Close();
            }
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
            cmd.CommandText = "INSERT INTO Reporte (id, Fecha,descuento,IDcliente, cliente, Total, Estado) VALUES ('" + dr["id"] + "','" + dr["Fecha"] + "','" + dr["descuento"] + "','" + dr["IDcliente"] + "','" + dr["cliente"] + "','" + dr["Total"] + "','" + "activa"  + "')";
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void modificar(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            cmd.CommandText = "UPDATE Reporte SET Fecha=?, descuento=?, IDcliente=?, cliente=?, Total=?, Estado=? WHERE Id=?";
            cmd.Parameters.AddWithValue("Fecha", dr["Fecha"]);
            cmd.Parameters.AddWithValue("descuento", dr["descuento"]);
            cmd.Parameters.AddWithValue("IDcliente", dr["IDcliente"]);
            cmd.Parameters.AddWithValue("cliente", dr["cliente"]);
            cmd.Parameters.AddWithValue("Total", dr["Total"]);
            cmd.Parameters.AddWithValue("Estado", "activa"); // Si el campo en la base de datos acepta "verdadero" o "falso"

            cmd.Parameters.AddWithValue("Id", dr["Id"]);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }
        public void anular(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = conexion;
            cmd.CommandText = "UPDATE Reporte SET Estado=? WHERE Id=?";
            cmd.Parameters.AddWithValue("Estado", "anulada"); // Si el campo en la base de datos acepta "verdadero" o "falso"
            cmd.Parameters.AddWithValue("Id", dr["Id"]);

            cmd.ExecuteNonQuery();
            conexion.Close();

        }
    }
}
