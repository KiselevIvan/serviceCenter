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
    public partial class managerPage : Page
    {//интерфейс менеджера

        private void updateDbGridClients()
        {//обновление данных таблицы клиенты
            dbGridClients.ItemsSource = core.serviceCenterDB.clients.ToList(); //устанавливаем источник данных для таблицы клиенты            
        }

        public managerPage()
        {
            InitializeComponent();
            dbGridEmployee.ItemsSource = core.serviceCenterDB.employees.ToList(); //устанавливаем источник данных для таблицы сотрудники                        
            updateDbGridClients();
        }

        private void bAddClient_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки добавить клиента
            Windows.clientWindow f = new Windows.clientWindow();
            f.ShowDialog(); //вызываем окно редактирования\добавления клиента
            updateDbGridClients();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки редактировать
            var currentClient = dbGridClients.SelectedItem as client;
            Windows.clientWindow f = new Windows.clientWindow(currentClient, true);
            f.ShowDialog(); //вызываем окно редактирования\добавления сотрудника
            updateDbGridClients();
        }

        private void bAddContract_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Оформить заявку
            Windows.contractWindow w = new Windows.contractWindow(dbGridClients.SelectedItem as client);
            w.ShowDialog();

        }
    }
}
