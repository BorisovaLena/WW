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
using System.Windows.Shapes;

namespace ПишиСтирай.windows
{
    /// <summary>
    /// Логика взаимодействия для WindowStatus.xaml
    /// </summary>
    public partial class WindowStatus : Window
    {
        Order order;
        public WindowStatus(Order order)
        {
            InitializeComponent();
            this.order = order;
            List<OrderStatus> orderStatuses = classes.ClassBase.Base.OrderStatus.ToList();
            foreach(OrderStatus status in orderStatuses)
            {
                cmbStatus.Items.Add(status.OrderStatusName);
            }
            cmbStatus.SelectedIndex = order.OrderStatus-1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.OrderStatus = cmbStatus.SelectedIndex + 1;
                classes.ClassBase.Base.SaveChanges();
                MessageBox.Show("Успешное сохранение!!!");
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка!!!");
            }
            
        }
    }
}
