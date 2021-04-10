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
        }
     
        private void updateDbGridEmployee()
        {//обновление данныех таблицы сотрудники
            dbGridEmployee.ItemsSource = core.serviceCenterDB.employees.ToList(); //устанавливаем источник данных для таблицы сотрудники            
        }

        private void bAddEmp_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки добавить
            Windows.employeeWindow f = new Windows.employeeWindow();
            f.ShowDialog(); //вызываем окно редактирования\добавления сотрудника
            updateDbGridEmployee(); 
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки удалить
            var currentEmployee = dbGridEmployee.SelectedItem as employee;
            core.serviceCenterDB.employees.Remove(currentEmployee);
            core.serviceCenterDB.SaveChanges();
            updateDbGridEmployee();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки редактировать
            var currentEmployee = dbGridEmployee.SelectedItem as employee;
            Windows.employeeWindow f = new Windows.employeeWindow(currentEmployee, true);
            f.ShowDialog(); //вызываем окно редактирования\добавления сотрудника
            updateDbGridEmployee();
        }
        
    }
}
