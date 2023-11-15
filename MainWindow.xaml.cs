using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExamenPA.Vista;
namespace ExamenPA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductoVista producto;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ProductoClick(object sender, RoutedEventArgs e)
        {
            producto = new ProductoVista();
            producto.Show();
        }
        private void AlmacenClick(object sender, RoutedEventArgs e)
        {
            AlmacenVista almacen = new AlmacenVista();
            almacen.Show();
        }
        private void CategoriaClick(object sender, RoutedEventArgs e)
        {
            CategoriaVista categoria = new CategoriaVista();
            categoria.Show();
        }
        private void ProveedorClick(object sender, RoutedEventArgs e)
        {
            ProveedorVista proveedor = new ProveedorVista();
            proveedor.Show();
        }   
        private void ClientesClick(object sender, RoutedEventArgs e)
        {
            ClientesVista clientes = new ClientesVista();
            clientes.Show();
        }
        private void VentaClick(object sender, RoutedEventArgs e)
        {
            Reporteventa venta = new Reporteventa();
            venta.Show();
        }

    }
}
