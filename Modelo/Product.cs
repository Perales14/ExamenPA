using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            cmd.CommandText = "SELECT * FROM Almacen";
            dr = cmd.ExecuteReader();
            dataTable.Load(dr);
            conexion.Close();

            //eliminar la row de "stock"
            dataTable.Columns.Remove("stock");
            return dataTable;

        }
        public DataTable Datos()
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr;
            DataTable dataTable = new DataTable();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT * FROM Almacen";
            dr = cmd.ExecuteReader();
            dataTable.Load(dr);
            conexion.Close();

            dataTable.Columns["stock"].ColumnName = "cantidad";
            //eliminar la row de "stock"

            return dataTable;

        }

        public void insertar(DataRow dr)
        {
            abrir(); 
            OleDbCommand cmd = new OleDbCommand();            
            cmd.Connection = conexion;
            cmd.CommandText = "INSERT INTO Almacen (id, nombre, precio, categoria, proveedor) VALUES ('" + dr["id"] + "','" + dr["nombre"] + "','" + dr["precio"] + "','" + dr["categoria"] + "','" + dr["proveedor"] + "')";
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void modificar(DataRow dr)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();
            
            cmd.Connection = conexion;
            cmd.CommandText = "UPDATE Almacen SET nombre=?, precio=?, categoria=?, proveedor=? WHERE Id=?";
            cmd.Parameters.AddWithValue("nombre", dr["nombre"]);
            cmd.Parameters.AddWithValue("precio", dr["precio"]);
            cmd.Parameters.AddWithValue("categoria", dr["categoria"]);
            cmd.Parameters.AddWithValue("proveedor", dr["proveedor"]);
            cmd.Parameters.AddWithValue("Id", dr["Id"]);
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        public DataRow producto(String nombre)
        {
            abrir();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataReader dr;
            DataTable dataTable = new DataTable();
            cmd.Connection = conexion;
            cmd.CommandText = "SELECT * FROM Almacen";
            dr = cmd.ExecuteReader();
            dataTable.Load(dr);
            conexion.Close();
            DataRow Dr = dataTable.NewRow();
            //eliminar la row de "stock"
            //dataTable.Columns.Remove("stock");
            //MessageBox.Show(nombre);
            foreach (DataRow row in dataTable.Rows)
            {
                String imp = row["nombre"].ToString();
                //MessageBox.Show("dentro"+nombre);

                if (imp.Equals(nombre))
                {
                    //MessageBox.Show(imp+"=="+nombre);
                    
                    

                    if (row.Table.Columns.Count == Dr.Table.Columns.Count)
                    {
                        Dr.ItemArray = row.ItemArray.Clone() as object[];
                        String D = Dr["nombre"].ToString();
                        //MessageBox.Show(D);
                    }
                    
                    break;
                }
            }

            return Dr;
        }
        


    }
}
