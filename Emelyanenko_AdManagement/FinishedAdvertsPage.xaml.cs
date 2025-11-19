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

namespace Emelyanenko_AdManagement
{
    /// <summary>
    /// Логика взаимодействия для FinishedAdvertsPage.xaml
    /// </summary>
    public partial class FinishedAdvertsPage : Page
    {
        public FinishedAdvertsPage()
        {
            InitializeComponent();
            DataGrid_Main.ItemsSource = Emelyanenko_AdManagementEntities.getInstance().Adverts.Where(entry => entry.Ad_Statuses.Name == "Завершено").ToList();
        }
    }
}
