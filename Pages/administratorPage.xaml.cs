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
    public partial class administratorPage : Page
    {//интерфейс администратора
      
        public administratorPage()
        {
            InitializeComponent();
            updateDbGridEmployee();
            updateDBGridServices();
        }
     
        private void updateDbGridEmployee()
        {//обновление данныех таблицы сотрудники
            dbGridEmployee.ItemsSource = core.serviceCenterDB.employees.ToList(); //устанавливаем источник данных для таблицы сотрудники
        }

        private void updateDBGridServices()
        {//обновление данных таблицы услуги
            dgServices.ItemsSource = core.serviceCenterDB.services.ToList(); //устанавливаем источник данных для таблицы услуги
        }

        private void bEmployeeADD_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки "добавить" сотраудника
            Windows.employeeWindow f = new Windows.employeeWindow();
            f.ShowDialog(); //вызываем окно редактирования\добавления сотрудника
            updateDbGridEmployee();            
        }

        private void bEmployeeDelete_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки "удалить" сотрудника
            var currentEmployee = dbGridEmployee.SelectedItem as employee;
            core.serviceCenterDB.employees.Remove(currentEmployee);
            core.serviceCenterDB.SaveChanges();
            updateDbGridEmployee();
        }

        private void bEmployeeEdit_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки "редактировать" сотрудника
            var currentEmployee = dbGridEmployee.SelectedItem as employee;
            Windows.employeeWindow f = new Windows.employeeWindow(currentEmployee, true);
            f.ShowDialog(); //вызываем окно редактирования\добавления сотрудника
            updateDbGridEmployee();
        }
                
        private void bServiceAdd_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Добавить" услугу
            Windows.ServiceWindow w = new Windows.ServiceWindow();
            w.ShowDialog();
            if (w.DialogResult==true) //если даные были внесены
            {
                core.serviceCenterDB.services.Add(w.Service); //добавляем в бд
                core.serviceCenterDB.SaveChanges();
                updateDBGridServices();
            }
        }

        private void bServicesDelete_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки "удалить" услугу
            if (dgServices.SelectedItem != null)
            {
                core.serviceCenterDB.services.Remove(dgServices.SelectedItem as service);
                core.serviceCenterDB.SaveChanges();
                updateDBGridServices();
            }
            else
                MessageBox.Show("Услуга для удаления не выбрана");
        }

        private void bServicesEditButton_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки "редактировать" услугу
            if (dgServices.SelectedItem != null) //если выбрана строка таблицы
            {               
                 Windows.ServiceWindow w= new Windows.ServiceWindow(dgServices.SelectedItem as service);
                w.ShowDialog();
                if (w.DialogResult == true) //если пользователь совершил ввод
                    core.serviceCenterDB.SaveChanges();
                updateDBGridServices();
            }
            else
                MessageBox.Show("Услуга для редактирования не выбрана");
        }
    }
}
