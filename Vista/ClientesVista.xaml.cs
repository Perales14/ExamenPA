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
using ExamenPA.Controlador;

namespace ExamenPA.Vista
{
    /// <summary>
    /// Lógica de interacción para ClientesVista.xaml
    /// </summary>
    public partial class ClientesVista : Window
    {
        private Cliente control = new Cliente();
        //private Categoria categoria = new Categoria();
        //private Proveedor proveedor = new Proveedor();
        Boolean nuevo;
        public ClientesVista()
        {
            InitializeComponent();
            dataGrid.ItemsSource = CargarDatos().DefaultView;
            //categoriaComboBox.ItemsSource = categoria.().DefaultView;
            //proveedorComboBox.ItemsSource = proveedor.().DefaultView;
        }

        private DataTable CargarDatos()
        {
            return control.Obtenerdatos();
        }

        private void GuardarClick(object sender, RoutedEventArgs e)
        {
            if (datosVacios())
            {
                MessageBox.Show("Llene todos los campos");
                return;
            }
            DataRow dr = CargarDatos().NewRow();
            dr["id"] = idTextBox.Text;
            dr["nombre"] = nombreTextBox.Text;

            control.Guardar(dr, nuevo);
            limpiar();
            dataGrid.ItemsSource = CargarDatos().DefaultView;
            tabControl.SelectedIndex = 0;
        }
        private Boolean datosVacios()
        {
            return idTextBox.Text == "" || nombreTextBox.Text == "" ;
        }
        private void limpiar()
        {
            idTextBox.Text = "";
            nombreTextBox.Text = "";

        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            //limpia los campos y regresa a la ventana principal    
            limpiar();
            tabControl.SelectedIndex = 0;

        }

        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
                //carga los datos en los campos
                DataRowView row = (DataRowView)dataGrid.SelectedItem;
                idTextBox.Text = row["id"].ToString();
                nombreTextBox.Text = row["nombre"].ToString();
                tabControl.SelectedIndex = 1;
                nuevo = false;
            }
            else
            {
                MessageBox.Show("Seleccione un registro");
            }
        }
        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            nuevo = true;
            tabControl.SelectedIndex = 1;

        }
    }
}
