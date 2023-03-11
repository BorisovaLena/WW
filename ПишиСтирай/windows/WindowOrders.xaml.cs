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
    /// Логика взаимодействия для WindowOrders.xaml
    /// </summary>
    public partial class WindowOrders : Window
    {
        public WindowOrders()
        {
            InitializeComponent();
            lvOrders.ItemsSource = classes.ClassBase.Base.Order.ToList();
        }
    }
}
