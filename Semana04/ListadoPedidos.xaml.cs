using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Semana04
{
    /// <summary>
    /// Lógica de interacción para ListadoPedidos.xaml
    /// </summary>
    public partial class ListadoPedidos : Window
    {
        private readonly string connectionString = "Data Source=DESKTOP-0OKTP7C\\MSSQLSERVERRR;Initial Catalog=Neptuno3;User ID=admin;Password=admin";
        public ListadoPedidos()
        {
            InitializeComponent();
        }
        private void BuscarPedidos_Click(object sender, RoutedEventArgs e)
        {
            string FechaInicio = txtFechaInicio.Text;
            string FechaFin = txtFechaFin.Text;

            List<Pedidos> pedidos = BuscarPedidos(FechaInicio, FechaFin);

            dataGridPedidos.ItemsSource = pedidos;
        }
        private List<Pedidos> BuscarPedidos(string nombreContacto, string ciudad)
        {
            List<Pedidos> pedidos = new List<Pedidos>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("ListarDetallesPedidosPorFecha", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FechaInicio", nombreContacto);
                    command.Parameters.AddWithValue("@FechaFin", ciudad);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pedidos pedido = new Pedidos
                            {
                                idpedido = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                                idproducto = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                preciounidad = reader.IsDBNull(1) ? 0 : reader.GetDecimal(2),
                                cantidad = reader.IsDBNull(1) ? 0 : reader.GetInt32(3),
                                descuento = reader.IsDBNull(1) ? 0 : reader.GetDecimal(4),
                            };
                            pedidos.Add(pedido);
                        }
                    }

                }
            }

            return pedidos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
