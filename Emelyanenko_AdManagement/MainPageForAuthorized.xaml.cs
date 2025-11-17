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
    /// Логика взаимодействия для MainPageForAuthorized.xaml
    /// </summary>
    public partial class MainPageForAuthorized : Page
    {
        public MainPageForAuthorized()
        {
            InitializeComponent();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditAdvertPage((Adverts)DataGrid_Main.SelectedItem));
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Adverts selected = (Emelyanenko_AdManagement.Adverts)DataGrid_Main.SelectedItem;
                try
                {
                    Emelyanenko_AdManagementEntities.getInstance().Adverts.Remove(selected);
                    Emelyanenko_AdManagementEntities.getInstance().SaveChanges();
                    DataGrid_Main.ItemsSource = Emelyanenko_AdManagementEntities.getInstance().Adverts.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при работе с базой данных. Попробуйте перезапустить приложение.", "Ошибка");
                    return;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_Main.ItemsSource = Emelyanenko_AdManagementEntities.getInstance().Adverts.ToList();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditAdvertPage());
        }
    }
}
