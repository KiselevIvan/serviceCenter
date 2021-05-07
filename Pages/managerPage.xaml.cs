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
using System.Data;

namespace serviceCenter.Pages
{
    public partial class managerPage : Page
    {//интерфейс менеджера

        private void updateDbGridClients()
        {//обновление данных таблицы клиенты
            dbGridClients.ItemsSource = core.serviceCenterDB.clients.ToList(); //устанавливаем источник данных для таблицы клиенты            
        }

        private void updateGBGridContracts()
        {
            dbGridContracts.ItemsSource = core.serviceCenterDB.VIew_contractsExecution.ToList();
        }

        public managerPage()
        {
            InitializeComponent();
            dbGridEmployee.ItemsSource = core.serviceCenterDB.employees.ToList(); //устанавливаем источник данных для таблицы сотрудники                        
            updateDbGridClients();
            updateGBGridContracts();
            bAddContract.IsEnabled = false;
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
            if (w.DialogResult == true)
            {
                core.serviceCenterDB.contracts.Add(w.Contract);
                core.serviceCenterDB.SaveChanges();
                updateGBGridContracts();
            }
        }


        private void dbGridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bAddContract.IsEnabled = dbGridClients.SelectedItem != null;
        }

        private void bUpdateClientsList_Click(object sender, RoutedEventArgs e)
        {
            updateDbGridClients();
        }

        private void bEditContract_Click(object sender, RoutedEventArgs e)
        {
            Int32 id = (dbGridContracts.SelectedItem as VIew_contractsExecution).Id;
            contract current = core.serviceCenterDB.contracts.Where(c => c.Id == id).First(); 
            Windows.contractWindow w = new Windows.contractWindow(current);
            w.ShowDialog();
            if (w.DialogResult == true)
            {       
                core.serviceCenterDB.SaveChanges();
            }
            updateGBGridContracts();
        }

        private void bShowContract_Click(object sender, RoutedEventArgs e)
        {
            Int32 id = (dbGridContracts.SelectedItem as VIew_contractsExecution).Id;
            contract current = core.serviceCenterDB.contracts.Where(c => c.Id == id).First();
            Windows.contractWindow w = new Windows.contractWindow(current);
            w.bOk.Visibility = Visibility.Collapsed;
            w.bCancel.Content = "Закрыть";
            w.bAddService.IsEnabled= w.bAddDevice.IsEnabled = false;
            w.bEditDevice.IsEnabled = w.bEditService.IsEnabled = false;
            w.ReadOnly = true;
            w.Width += 400;
            w.Height += 200;
            w.ShowDialog();            
        }

        private void bUpdateContractsList_Click(object sender, RoutedEventArgs e)
        {
            updateGBGridContracts();
        }

        private void bfinishcontract_Click(object sender, RoutedEventArgs e)
        {
            VIew_contractsExecution  ce= dbGridContracts.SelectedItem as VIew_contractsExecution;
            if (ce != null)
            {
                contract currentContract = core.serviceCenterDB.contracts.Where(c => c.Id == ce.Id).First();
                currentContract.endDate = DateTime.Now;
                currentContract.endCost = currentContract.approximateCost;
                foreach (clientDevice cd in currentContract.clientDevices)
                {
                    foreach (requestedService rs in cd.requestedServices)
                    {
                        rs.stageOfImplementationId = 4;
                    }
                }
                MessageBox.Show("Выполнено");
                core.serviceCenterDB.SaveChanges();
            }
        }
    }
}
