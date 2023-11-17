using ExamenPA.Controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExamenPA.Vista
{
    /// <summary>
    /// Lógica de interacción para Reporteventa.xaml
    /// </summary>
    public partial class Reporteventa : Window
    {
        ReporteVentas controlador = new ReporteVentas();
        Producto controlp = new Producto();
        Cliente controc = new Cliente();
        DataTable dataproductos = new DataTable();

        public Reporteventa()
        {
            InitializeComponent();
            dataGrid.ItemsSource = CargarDatos().DefaultView;
            dataproductos = controlp.datos();
            dataproductos.Clear();
        }

        public DataTable CargarDatos()
        {
            return controlador.Obtenerdatos();
        }

        //vaciar los campos
        private void vaciar()
        {
            Textboxid.Text = "";
            Textboxdescuento.Text = "";
            Fechat.Text = "";
            Comboboxcliente.Text = "";
            Comboboxproducto.Text = "";
            Textboxcantidad.Text = "";

        }
        //llenar los campos con los datos SELECIONADOS
        private void llenar()
        {
            dataproductos.Clear();
            if (dataGrid.SelectedIndex >= 0)
            {
                //carga los datos en los campos
                DataRowView dataRowView = (DataRowView)dataGrid.SelectedItem;
                Textboxid.Text = dataRowView["id"].ToString();
                Fechat.Text = dataRowView["Fecha"].ToString();
                Textboxdescuento.Text = dataRowView["descuento"].ToString();
                Comboboxcliente.ItemsSource = controc.clientes();
                Comboboxcliente.Text = dataRowView["cliente"].ToString();
                //Comboboxproducto.Text = dataRowView["producto"].ToString();
                Textboxtotal.Text = dataRowView["Total"].ToString();
                //MessageBox.Show(dataRowView["id"].ToString());
                dataproductos = controlador.productos(dataRowView["id"].ToString());
                dataGridproductos.ItemsSource = dataproductos.DefaultView;// controlador.productos(dataRowView["id"].ToString()).DefaultView;
                
            }
        }

        //crera los metodos de los botones, seran vacios
        private void AgregarButton_Click(object sender, RoutedEventArgs e)//actualizar dataproducto
        {
            vaciar();
            dataproductos.Clear();
            Comboboxcliente.ItemsSource = controc.clientes();
            Comboboxproducto.ItemsSource = controlp.productos();
            DataTable dt2 = new DataTable();
            
            foreach (DataColumn col in controlp.datos().Columns)
            {
                dt2.Columns.Add(col.ColumnName, col.DataType);
            }
            
            
            dataGridproductos.ItemsSource = dt2.DefaultView;
            tabControl.SelectedIndex = 1;
        }
        private void Agregarproducto_Click(object sender, RoutedEventArgs e)//actualizar dataproducto
        {
            //toma el nombre de producto, y lo busca en la base de datos
            //y lo agrega al datagridproductos
            if (Comboboxproducto.Text != "" && Textboxcantidad.Text != "")
            {
                //MessageBox.Show(Comboboxproducto.Text);
                DataRow dr = controlp.producto(Comboboxproducto.Text);
                dr.Table.Columns["stock"].ColumnName = "cantidad";
                dr["cantidad"] = Textboxcantidad.Text;
                //MessageBox.Show(dr["cantidad"].ToString());
                DataRow a = dataproductos.NewRow();

                //id nombre precio cantidad categoria provedor
                //cambiar de row a a
                a["id"] = dr["id"];
                a["nombre"] = dr["nombre"];
                a["precio"] = dr["precio"];
                a["cantidad"] = dr["cantidad"];
                a["categoria"] = dr["categoria"];
                a["proveedor"] = dr["proveedor"];

                dataproductos.Rows.Add(a);

                dataGridproductos.ItemsSource = dataproductos.DefaultView;
                double.TryParse(Textboxtotal.Text, out double numero1);
                double.TryParse(dr["precio"].ToString(), out double numero2);
                double.TryParse(Textboxcantidad.Text, out double numero3);
                double total = numero1 + (numero2 * numero3);
                Textboxtotal.Text = total.ToString();

            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto y una cantidad", "Agregar", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void AnularButton_Click(object sender, RoutedEventArgs e)
        {
           //anula venta, pero antes de anular muestra mensaje para confirmar
           if (dataGrid.SelectedIndex >= 0)
            {
                DataRowView dataRowView = (DataRowView)dataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("¿Esta seguro que desea anular la venta?", "Anular", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    controlador.anular(dataRowView.Row);
                    dataGrid.ItemsSource = controlador.Obtenerdatos().DefaultView;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una venta", "Anular", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)//actualizar dataproducto
        {
            //DEL datagridproductos quita el que se este seleccionando
            if (dataGridproductos.SelectedIndex >= 0)
            {
                //eliminar el producto seleccionado en dataproductos
                //orrar la row del datagridproductos
                //actualizar el Total
                //tomar el la row "precio" y "cantidad" del datagridproductos
                //y restarlos al total
                DataRowView dataRowView = (DataRowView)dataGridproductos.SelectedItem;
                double.TryParse(Textboxtotal.Text, out double numero1);
                double.TryParse(dataRowView["precio"].ToString(), out double numero2);
                double.TryParse(dataRowView["cantidad"].ToString(), out double numero3);
                double total = numero1 - (numero2 * numero3);
                Textboxtotal.Text = total.ToString();
                dataproductos.Rows.RemoveAt(dataGridproductos.SelectedIndex);
                dataGridproductos.ItemsSource = dataproductos.DefaultView;

            }
        }

        private void DetallesClick(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
                llenar();
                Guardar.Visibility = Visibility.Hidden;
                Eliminar.Visibility = Visibility.Hidden;
                tabControl.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una venta", "Detalles", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        
        private void GuardarButtonClick(object sender, RoutedEventArgs e)
        {
            
            if (Textboxid.Text == "" ||Fechat.Text==""||Textboxdescuento.Text==""||Comboboxcliente.Text=="")
            {
                MessageBox.Show("Debe llenar todos los campos", "Guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DataRow dr = controlador.Obtenerdatos().NewRow();
            dr["id"] = Textboxid.Text;
            dr["Fecha"] = Fechat.Text;
            dr["descuento"] = Textboxdescuento.Text;
            dr["cliente"] = Comboboxcliente.Text;
            String idcliente = controc.id(dr["cliente"].ToString());
            dr["idcliente"] = idcliente;//hay que buscarl el id del cliente con su nombre

            //dr["producto"] = Comboboxproducto.Text;
            dr["Total"] = Textboxtotal.Text;
            dr["estado"] = "activada";
            //tomar del datagridproductos el id, y guardarlo en un archivo de texto en Ventas/ventas.txt
            //y guardar el id de la venta en el archivo de texto
            //convertir el datagridproductos en un datatable
            
            controlador.guardaridventa(Textboxid.Text, dataproductos);
            controlador.Guardar(dr);
            vaciar();
            dataGrid.ItemsSource = controlador.Obtenerdatos().DefaultView;
            tabControl.SelectedIndex = 0;
        }
        private void CancelarButtonClick(object sender, RoutedEventArgs e)
        {

            vaciar();
            
            tabControl.SelectedIndex = 0;
            Guardar.Visibility = Visibility.Visible;
            Eliminar.Visibility = Visibility.Visible;

        }
    }
}
