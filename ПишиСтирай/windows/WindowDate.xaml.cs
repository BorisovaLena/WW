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
    /// Логика взаимодействия для WindowDate.xaml
    /// </summary>
    public partial class WindowDate : Window
    {
        Order order;
        public WindowDate(Order order)
        {
            InitializeComponent();
            this.order = order;
            dpDate.SelectedDate = order.OrderDeliveryDate;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                order.OrderDeliveryDate = (DateTime)dpDate.SelectedDate;
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
