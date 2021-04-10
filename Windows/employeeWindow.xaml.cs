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
    public partial class employeeWindow : Window
    {//окно добавления\просмотра\редактирования сотрудника

        employee currentEmployee;
        bool add; //флаг добавления новой записи

        public employeeWindow()
        {//конструктор по умолчанию 
            InitializeComponent();
            currentEmployee = new employee(); //создаем нового клиента
            cbPosition.ItemsSource = core.serviceCenterDB.positions.ToList();            
            add = true;
        }

        public employeeWindow(employee aCurrentEmployee, bool edit)
        {//перегруженый конструктор для редактирования\просмотра данных сотрудника
         //текущий клиент для просмотра\редактирования aCurrentEmployee  флаг редактирования\просмотра edit
            InitializeComponent();
            cbPosition.ItemsSource = core.serviceCenterDB.positions.ToList();
            add = false;
            currentEmployee = aCurrentEmployee;
            if (!edit)
            {//если не установлен флаг редактирования, скрываем и переименовываем кнопки
                bOk.Visibility = Visibility.Collapsed;
                bCancel.Content = "Закрыть";
            }
            //заполняем поля данными 
            tbFIO.Text = currentEmployee.FIO;
            tbPhoneNumber.Text = currentEmployee.phoneNumber;
            cbPosition.SelectedIndex =currentEmployee.positionId-1;
            tbLogin.Text = currentEmployee.login;
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки ок
            //заполняем поля модели бд
            currentEmployee.FIO = tbFIO.Text;
            currentEmployee.phoneNumber = tbPhoneNumber.Text;
            currentEmployee.positionId = (cbPosition.SelectedItem as position).Id;
            currentEmployee.login = tbLogin.Text;
            if (chbResetPassword.IsChecked.Value)
            {//если отмечена смена пароля, вычисляем хеш и заносим в модель
                currentEmployee.password = passwords.Password.GetHash(10,10,pbPass.Password);
                currentEmployee.resetPassword = true; //устанавливаем флаг на смену пароля
            }
            
            if (add)
            {//если установлен флаг добавления, добавляем нового клиента в модель бд
                core.serviceCenterDB.employees.Add(currentEmployee);
            }
            //сохраняем изменение данных на сервер и закрываем окно
            core.serviceCenterDB.SaveChanges();
            Close();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки отмена
            Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {//перехват вводимых символов и проверка на наличие посторонние знаков
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {//обработчик изменения состояния чекбокса смены пароля
            if ((sender as CheckBox).IsChecked.Value) //если отмечено, показываем поле для ввода временного пароля
                grPasswordField.Height = GridLength.Auto;
            else//если не отмечено, то скрываем
                grPasswordField.Height = new GridLength(0, GridUnitType.Star);
        }
    }
}
