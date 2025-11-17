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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
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
            if (String.IsNullOrEmpty(PasswordBox_ConfirmPassword.Password))
            {
                errors.AppendLine("Заполните поле (Подтвердите пароль)!");
            }
            if (!String.IsNullOrEmpty(PasswordBox_ConfirmPassword.Password) && PasswordBox_Password.Password != PasswordBox_ConfirmPassword.Password)
            {
                errors.AppendLine("Пароли не совпадают!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка");
                return;
            }

            if (Emelyanenko_AdManagementEntities.getInstance().Users.FirstOrDefault(entry => entry.User_Login == TextBox_Login.Text) != null)
            {
                MessageBox.Show("Этот логин уже занят!", "Ошибка");
                return;
            }

            Users user = new Users() { User_Login = TextBox_Login.Text, User_Password = PasswordBox_Password.Password };
            try
            {
                Emelyanenko_AdManagementEntities.getInstance().Users.Add(user);
                Emelyanenko_AdManagementEntities.getInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при работе с базой данных. Попробуйте перезапустить приложение.", "Ошибка");
                return;
            }
            

            NavigationService.Navigate(new MainPageForAuthorized());
        }
    }
}
