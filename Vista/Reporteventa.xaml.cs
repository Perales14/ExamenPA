using ExamenPA.Controlador;
using System;
using System.Collections.Generic;
using System.Data;
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
        public Reporteventa()
        {
            InitializeComponent();
            
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
            if (dataGrid.SelectedIndex >= 0)
            {
                //carga los datos en los campos
                DataRowView dataRowView = (DataRowView)dataGrid.SelectedItem;
                Textboxid.Text = dataRowView["id"].ToString();
                Fechat.Text = dataRowView["Fecha"].ToString();
                Textboxdescuento.Text = dataRowView["descuento"].ToString();
                Comboboxcliente.Text = dataRowView["cliente"].ToString();
                Comboboxproducto.Text = dataRowView["producto"].ToString();
                Textboxtotal.Text = dataRowView["Total"].ToString();
                
            }
        }

        //crera los metodos de los botones, seran vacios
        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            vaciar();
            tabControl.SelectedIndex = 1;   
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

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            //DEL datagridproductos quita el que se este seleccionando
            if (dataGridproductos.SelectedIndex >= 0)
            {
                DataRowView dataRowView = (DataRowView)dataGridproductos.SelectedItem;
                dataRowView.Delete();
            }
        }

        private void DetallesClick(object sender, RoutedEventArgs e)
        {
            if (dataGridproductos.SelectedIndex >= 0)
            {
                llenar();
                tabControl.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Debe seleccionar una venta", "Detalles", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        
        private void GuardarButtonClick(object sender, RoutedEventArgs e)
        {
            DataRow dr = controlador.Obtenerdatos().NewRow();
            dr["id"] = Textboxid.Text;
            dr["Fecha"] = Fechat.Text;
            dr["descuento"] = Textboxdescuento.Text;
            dr["cliente"] = Comboboxcliente.Text;
            dr["id"] = "";//hay que buscarl el id del cliente con su nombre

            //dr["producto"] = Comboboxproducto.Text;
            dr["Total"] = Textboxtotal.Text;
            dr["estado"] = "verdadero";
            controlador.Guardar(dr);
            vaciar();
            tabControl.SelectedIndex = 0;
        }
        private void CancelarButtonClick(object sender, RoutedEventArgs e)
        {
            vaciar();
            tabControl.SelectedIndex = 0;

        }
    }
}
