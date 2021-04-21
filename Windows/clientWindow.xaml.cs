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
using System.Text.RegularExpressions;

namespace serviceCenter.Windows
{
    public partial class clientWindow : Window
    {//окно добавления\просмотра\редактирования клиента
        client currentClient;
        bool add; //флаг добавления новой записи
        public clientWindow()

        {//конструктор по умолчанию 
            InitializeComponent();
            currentClient = new client(); //создаем нового клиента
            add = true;
        }

        public clientWindow(client aCurrentClient, bool edit)
        {//перегруженый конструктор для редактирования\просмотра клиента
         //текущий клиент для просмотра\редактирования aCurrentClient  флаг редактирования\просмотра edit

            InitializeComponent();            
            add = false;
            currentClient = aCurrentClient;
            if (!edit)
            {//если не установлен флаг редактирования, скрываем и переименовываем кнопки
                bOk.Visibility = Visibility.Collapsed;
                bCancel.Content = "Закрыть";
            }
            //заполняем поля данными 
            tbFIO.Text = currentClient.FIO;
            tbPhoneNumber.Text = currentClient.phoneNumber;
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки ок
            //заполняем поля модели бд
            if (String.IsNullOrWhiteSpace(tbFIO.Text) | String.IsNullOrWhiteSpace(tbPhoneNumber.Text))//проверка данных полей
                MessageBox.Show("Введены не корректные данные");
            else
            {
                currentClient.FIO = tbFIO.Text;
                currentClient.phoneNumber = tbPhoneNumber.Text;

                if (add)
                {//если установлен флаг добавления, добавляем нового клиента в модель бд
                    core.serviceCenterDB.clients.Add(currentClient);
                }
                //сохраняем изменение данных на сервер и закрываем окно
                core.serviceCenterDB.SaveChanges();
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки отмена
            Close();
        }
        
        private void tbPhoneNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {//Перехват нажатия клавишь и проверка на корректность вводимых символов
            controlInput.DigitNumberValidationTextBox(sender, e);
        }
    }
}
