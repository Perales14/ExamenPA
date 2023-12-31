﻿using ExamenPA.Controlador;
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
    /// Lógica de interacción para AlmacenVista.xaml
    /// </summary>
    public partial class AlmacenVista : Window
    {
        private Almacenn control = new Almacenn();
        private Categoria categoria = new Categoria();
        private Proveedor proveedor = new Proveedor();
        Boolean nuevo;
        public AlmacenVista()
        {
            InitializeComponent();
            dataGrid.ItemsSource = CargarDatos().DefaultView;
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
            dr["precio"] = precioTextBox.Text;
            dr["stock"] = stockTextBox.Text;
            dr["categoria"] = categoriaComboBox.Text;
            dr["proveedor"] = proveedorComboBox.Text;


            control.Guardar(dr, nuevo);
            limpiar();
            dataGrid.ItemsSource = CargarDatos().DefaultView;
            tabControl.SelectedIndex = 0;
        }
        private Boolean datosVacios()
        {
            return idTextBox.Text == "" || nombreTextBox.Text == "" || precioTextBox.Text == "" || stockTextBox.Text==""|| categoriaComboBox.SelectedValue == "" || proveedorComboBox.SelectedValue == "";
        }
        private void limpiar()
        {
            idTextBox.Text = "";
            nombreTextBox.Text = "";
            precioTextBox.Text = "";
            stockTextBox.Text = "";
            categoriaComboBox.SelectedValue = "";
            proveedorComboBox.SelectedValue = "";

        }

        private void CancelarClick(object sender, RoutedEventArgs e)
        {
            //limpia los campos y regresa a la ventana principal    
            limpiar();
            tabControl.SelectedIndex = 0;

        }

        public void CombosBoxs()
        {
            
            categoriaComboBox.ItemsSource = categoria.ObtenerClientes();
            proveedorComboBox.ItemsSource = proveedor.proveedores();
        }


        private void ModificarButton_Click(object sender, RoutedEventArgs e)
        {

            if (dataGrid.SelectedIndex >= 0)
            {
                //carga los datos en los campos
                DataRowView row = (DataRowView)dataGrid.SelectedItem;
                idTextBox.Text = row["id"].ToString();
                nombreTextBox.Text = row["nombre"].ToString();
                precioTextBox.Text = row["precio"].ToString();
                stockTextBox.Text = row["stock"].ToString();
                categoriaComboBox.SelectedValue = row["categoria"].ToString();
                proveedorComboBox.SelectedValue = row["proveedor"].ToString();
                CombosBoxs();
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
            CombosBoxs();
            nuevo = true;
            tabControl.SelectedIndex = 1;

        }
    }
}
