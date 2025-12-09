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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            ComboBox_User.ItemsSource = Emelyanenko_AdManagementEntities.GetInstance().Users.ToList();
            ComboBox_City.ItemsSource = Emelyanenko_AdManagementEntities.GetInstance().Cities.ToList();
            ComboBox_Category.ItemsSource = Emelyanenko_AdManagementEntities.GetInstance().Categories.ToList();
            ComboBox_Type.ItemsSource = Emelyanenko_AdManagementEntities.GetInstance().Ad_Types.ToList();
            ComboBox_Status.ItemsSource = Emelyanenko_AdManagementEntities.GetInstance().Ad_Statuses.ToList();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid_Main.ItemsSource = Emelyanenko_AdManagementEntities.GetInstance().Adverts.ToList();
        }

        private void Button_Filter_Click(object sender, RoutedEventArgs e)
        {
            List<Adverts> list = Emelyanenko_AdManagementEntities.GetInstance().Adverts.ToList();
            if (ComboBox_User.SelectedItem != null)
            {
                list = list.Where(entry => entry.Users == ComboBox_User.SelectedItem).ToList();
            }
            if (!String.IsNullOrEmpty(TextBox_Title.Text))
            {
                list = list.Where(entry => entry.Ad_Title.Contains(TextBox_Title.Text)).ToList();
            }
            if (!String.IsNullOrEmpty(TextBox_Description.Text))
            {
                list = list.Where(entry => entry.Ad_Description.Contains(TextBox_Description.Text)).ToList();
            }
            if (ComboBox_City.SelectedItem != null)
            {
                list = list.Where(entry => entry.Cities == ComboBox_City.SelectedItem).ToList();
            }
            if (ComboBox_Category.SelectedItem != null)
            {
                list = list.Where(entry => entry.Categories == ComboBox_Category.SelectedItem).ToList();
            }
            if (ComboBox_Type.SelectedItem != null)
            {
                list = list.Where(entry => entry.Ad_Types == ComboBox_Type.SelectedItem).ToList();
            }
            if (ComboBox_Status.SelectedItem != null)
            {
                list = list.Where(entry => entry.Ad_Statuses == ComboBox_Status.SelectedItem).ToList();
            }
            DataGrid_Main.ItemsSource = list;
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            ComboBox_User.SelectedItem = null;
            ComboBox_City.SelectedItem = null;
            ComboBox_Category.SelectedItem = null;
            ComboBox_Type.SelectedItem = null;
            ComboBox_Status.SelectedItem = null;
            TextBox_Title.Text = null;
            TextBox_Description.Text = null;
        }

        private void Button_Finished_Ads_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FinishedAdvertsPage());
        }
    }
}
