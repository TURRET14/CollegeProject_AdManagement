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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (String.IsNullOrEmpty(TextBox_Login.Text))
            {
                errors.AppendLine("Заполните поле (Логин)!");
            }
            if (String.IsNullOrEmpty(PasswordBox_Password.Password))
            {
                errors.AppendLine("Заполните поле (Пароль)!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка");
                return;
            }

            Users user = Emelyanenko_AdManagementEntities.getInstance().Users.FirstOrDefault(entry => entry.User_Login == TextBox_Login.Text && entry.User_Password == PasswordBox_Password.Password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка");
                return;
            }
            // Переход на страницу объявлений для авторизованных пользователей
            NavigationService.Navigate(new MainPageForAuthorized());
        }
    }
}
