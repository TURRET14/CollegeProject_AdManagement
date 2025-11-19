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
    /// Логика взаимодействия для EditAdvertPage.xaml
    /// </summary>
    public partial class EditAdvertPage : Page
    {
        private bool editing = false;
        private Emelyanenko_AdManagement.Adverts selected;
        public EditAdvertPage()
        {
            InitializeComponent();
            selected = new Adverts();
        }
        public EditAdvertPage(Emelyanenko_AdManagement.Adverts selected)
        {
            InitializeComponent();
            this.selected = selected;
            editing = true;

            TextBox_User.Text = selected.Users.User_Login;
            TextBox_Title.Text = selected.Ad_Title;
            TextBox_Description.Text = selected.Ad_Description;
            DatePicker_Date.SelectedDate = selected.Ad_Post_Date;
            TextBox_City.Text = selected.Cities.Name;
            TextBox_Category.Text = selected.Categories.Name;
            TextBox_Type.Text = selected.Ad_Types.Name;
            ComboBox_Status.SelectedItem = selected.Ad_Statuses.Name;
            TextBox_Price.Text = selected.Price.ToString();
            TextBox_PhotoPath.Text = selected.PhotoPath;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (String.IsNullOrEmpty(TextBox_User.Text))
            {
                errors.AppendLine("Заполните поле (Пользователь)!");
            }
            if (String.IsNullOrEmpty(TextBox_Title.Text))
            {
                errors.AppendLine("Заполните поле (Название)!");
            }
            if (String.IsNullOrEmpty(TextBox_Description.Text))
            {
                errors.AppendLine("Заполните поле (Описание)!");
            }
            if (DatePicker_Date.SelectedDate == null)
            {
                errors.AppendLine("Заполните поле (Дата)!");
            }
            if (String.IsNullOrEmpty(TextBox_City.Text))
            {
                errors.AppendLine("Заполните поле (Город)!");
            }
            if (String.IsNullOrEmpty(TextBox_Category.Text))
            {
                errors.AppendLine("Заполните поле (Категория)!");
            }
            if (String.IsNullOrEmpty(TextBox_Type.Text))
            {
                errors.AppendLine("Заполните поле (Тип)!");
            }
            if (ComboBox_Status.SelectedItem == null)
            {
                errors.AppendLine("Заполните поле (Статус)!");
            }
            if (String.IsNullOrEmpty(TextBox_Price.Text))
            {
                errors.AppendLine("Заполните поле (Цена)!");
            }
            if (String.IsNullOrEmpty(TextBox_PhotoPath.Text))
            {
                TextBox_PhotoPath.Text = null;
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(TextBox_Price.Text) < 0)
                    {
                        errors.AppendLine("Цена не может быть отрицательной!");
                    }
                }
                catch (Exception ex) 
                {
                    errors.AppendLine("Цена должна быть целым числом!");
                }
            }
                
            

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка");
                return;
            }

            Users user = Emelyanenko_AdManagementEntities.getInstance().Users.FirstOrDefault(entry => entry.User_Login == TextBox_User.Text);
            if (user == null)
            {
                MessageBox.Show("Такого пользователя нет!", "Ошибка!");
                return;
            }
            selected.Users = user;

            Cities city = Emelyanenko_AdManagementEntities.getInstance().Cities.FirstOrDefault(entry => entry.Name == TextBox_City.Text);
            if (city == null)
            {
                city = new Cities() { Name = TextBox_City.Text };
            }
            selected.Cities = city;

            Categories category = Emelyanenko_AdManagementEntities.getInstance().Categories.FirstOrDefault(entry => entry.Name == TextBox_Category.Text);
            if (category == null)
            {
                category = new Categories() { Name = TextBox_Category.Text };
            }
            selected.Categories = category;

            Ad_Types type = Emelyanenko_AdManagementEntities.getInstance().Ad_Types.FirstOrDefault(entry => entry.Name == TextBox_Type.Text);
            if (type == null)
            {
                type = new Ad_Types() { Name = TextBox_Type.Text };
            }
            selected.Ad_Types = type;

            Ad_Statuses status = Emelyanenko_AdManagementEntities.getInstance().Ad_Statuses.FirstOrDefault(entry => entry.Name == ComboBox_Status.SelectedItem.ToString());
            if (status == null)
            {
                status = new Ad_Statuses() { Name = ComboBox_Status.SelectedItem.ToString() };
            }
            selected.Ad_Statuses = status;

            selected.Ad_Title = TextBox_Title.Text;
            selected.Ad_Description = TextBox_Description.Text;
            selected.Ad_Post_Date = DatePicker_Date.SelectedDate;
            selected.Price = Convert.ToInt32(TextBox_Price.Text);
            selected.Price = Convert.ToInt32(TextBox_Price.Text);
            selected.PhotoPath = TextBox_PhotoPath.Text;
            if (editing == false)
            {
                try
                {
                    Emelyanenko_AdManagementEntities.getInstance().Adverts.Add(selected);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при работе с базой данных. Попробуйте перезапустить приложение.", "Ошибка");
                    return;
                }
            }
            Emelyanenko_AdManagementEntities.getInstance().SaveChanges();

            NavigationService.GoBack();
        }
    }
}
