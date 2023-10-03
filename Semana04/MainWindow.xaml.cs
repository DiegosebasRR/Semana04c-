using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Semana04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string connectionString = "Data Source=DESKTOP-0OKTP7C\\MSSQLSERVERRR;Initial Catalog=Neptuno3;User ID=admin;Password=admin";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BuscarProveedores_Click(object sender, RoutedEventArgs e)
        {
            string nombreContacto = txtNombreContacto.Text;
            string ciudad = txtCiudad.Text;

            List<Proveedor> proveedores = BuscarProveedores(nombreContacto, ciudad);

            dataGridProveedores.ItemsSource = proveedores;
        }
        private List<Proveedor> BuscarProveedores(string nombreContacto, string ciudad)
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("BuscarProveedores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NombreContacto", nombreContacto);
                    command.Parameters.AddWithValue("@Ciudad", ciudad);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Proveedor proveedor = new Proveedor
                            {
                                IDProveedor = reader.IsDBNull(0) ? 0 : reader.GetInt32(0), // Verifica si es nulo
                                NombreCompania = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                NombreContacto = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                CargoContacto = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                Direccion = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                Ciudad = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                Region = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                CodPostal = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                Pais = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                                Telefono = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                                Fax = reader.IsDBNull(10) ? string.Empty : reader.GetString(10),
                                PaginaPrincipal = reader.IsDBNull(11) ? string.Empty : reader.GetString(11)
                            };
                            proveedores.Add(proveedor);
                        }
                    }

                }
            }

            return proveedores;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListadoPedidos listadoPedidos = new ListadoPedidos();

            listadoPedidos.Show();

        }
    }
}
