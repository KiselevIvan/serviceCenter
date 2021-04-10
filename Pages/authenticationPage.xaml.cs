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

namespace serviceCenter.Pages
{

    public partial class authenticationPage : Page
    {//Страница авторизации пользователей
        public authenticationPage()
        {
            InitializeComponent();
        }

        private bool setNewPassword(ref employee user)
        {//функция смены пароля для пользователей, с флагом resetPassword
            Windows.userChangePasswordWindow w = new Windows.userChangePasswordWindow();
            w.ShowDialog(); //окно с запросом нового пароля
            if (w.DialogResult.Value) //если был введен новый пароль
            {
                user.password = passwords.Password.GetHash(10, 10, w.Password); //формируем хэш от нового пароля и заносим в бд
                user.resetPassword = false; //снимаем флаг resetPassword
                core.serviceCenterDB.SaveChanges();//сохраняем результат в бд
                return true;
            }
            return false;
        }

        private void bEnter_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки вход

            var currentUser = core.serviceCenterDB.employees.Where(u => u.login == tbLogin.Text).FirstOrDefault();
            //ищем пользователя в бд по логину
            if (currentUser != null && passwords.Password.VerifyHashedPassword(10, 10, currentUser.password, pbPassword.Password))
            {//если хеш от пароля введенного пользователем совпадет с хешем в бд
                Page currentPage = null;
                if (currentUser.resetPassword)
                {//если стоит флаг сброса пароля, запрашеваем у пользователя новый
                    if (setNewPassword(ref currentUser))
                        pbPassword.Clear(); //если пользователь сменил пароль, очищаем поле
                }
                else
                {//если смена пароля не требуется определяем группу пользователя и открываем его интерфейс
                    switch (currentUser.position.name)
                    {
                        case "Администратор":
                            currentPage = new Pages.administratorPage();
                            break;
                        case "Менеджер":
                            currentPage = new Pages.managerPage();
                            break;
                    }

                    if (currentPage != null)
                        NavigationService.Navigate(currentPage);
                }
            }
            else
            {//если пароль не верный
               // tbLogin.Text = passwords.Password.GetHash(10, 10, pbPassword.Password);
                MessageBox.Show("Данные не верны");
            }
        }

        private void bExit_Click(object sender, RoutedEventArgs e)
        {//кнопка выхода
            Application.Current.Shutdown();
        }
    }
}
