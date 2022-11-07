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
using WPF_Kuznetsov.PR12.ApplicationData;

namespace WPF_Kuznetsov.PR12.PageMain
{
    /// <summary>
    /// Логика взаимодействия для PageCreateAcc.xaml
    /// </summary>
    public partial class PageCreateAcc : Page
    {
        public PageCreateAcc()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void psbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (psbPass.Password != txbPass.Text)
            {
                btnCreate.IsEnabled = false;
                psbPass.Background = Brushes.LightCoral;
                psbPass.BorderBrush = Brushes.Red;
            }
            else
            {
                btnCreate.IsEnabled = true;
                psbPass.Background = Brushes.LightGreen;
                psbPass.BorderBrush = Brushes.Green;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //проверяем есть ли такой же пользователь
            if (AppConnect.mode10db.User.Count(x => x.login == txbLogin.Text) > 0)
            {
                MessageBox.Show("Пользователь с таким логином есть!", "Уведоиление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //если прошли проверку логина, записываем нового пользователя с ролью 2
            try
            {
                User userObj = new User()
                {
                    login = txbLogin.Text, //получаем даные логина
                    name = txbName.Text, //получаем данные имени пользователя
                    password = txbPass.Text, //получаем пароль
                    IdRole = 2 // присваиваем роль 2, тк администратор 1
                };
                AppConnect.mode10db.User.Add(userObj);//добавление объекта
                AppConnect.mode10db.SaveChanges();//сохранение
                MessageBox.Show("Данные успешно добавлены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
