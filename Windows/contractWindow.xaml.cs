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

namespace serviceCenter.Windows
{
    public partial class contractWindow : Window
    {//Окно составления договора, определяет объем, сроки и время оказания услуг

        contract contract;
        client client;
        public contractWindow(client client)
        {
            InitializeComponent();
            this.client = client;
            contract = new contract();
            controlBokEnable();
            controlBAddServiceEnable();
        }

        private void updateDbGridClientDevices()
        {//обновление данныех таблицы устройства клиента
            dgDevices.ItemsSource = contract.clientDevices.ToList(); //устанавливаем источник данных для таблицы устройства клинта            
        }

        private void updateDbGridService(ref clientDevice device)
        {//обновление данныех таблицы устройства клиента
            dgServices.ItemsSource = device.requestedServices.ToList(); //устанавливаем источник данных для таблицы устройства клинта            
        }

        private void controlBokEnable()
        {//метод для включения и выключения активности кнопки "Оформить", включаем кнопку когда все данные внесены
            bOk.IsEnabled = ((dgDevices.Items.Count > 0) & (dgServices.Items.Count > 0) & (dptbApproximateEndDate.SelectedDate != null));
        }

        private void controlBAddServiceEnable()
        {//метод для включения и выключения кнопки "добавить" услугу, включаем когда выбрано устройство
            bAddService.IsEnabled = (dgDevices.SelectedItem!= null);
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Оформить, формируем заказ и добавляем в бд
            contract.client = client;
            contract.dateOfReceipt = DateTime.Now;
            contract.approximateEndDate = dptbApproximateEndDate.SelectedDate.Value;            
            core.serviceCenterDB.contracts.Add(contract);
            core.serviceCenterDB.SaveChanges();
            Close();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Отмена
            Close();
        }

        private void bAddDevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Добовить" устройство
            Windows.DeviceWindow f = new DeviceWindow(ref contract); //окно для заполнения объектной модели устройства
            f.ShowDialog();
            if (f.DialogResult==true) //если модель была создана
            contract.clientDevices.Add(f.NewDevice); //добавляем новое описание устройства к текущему договору
            updateDbGridClientDevices(); //обновляем список устройств
            controlBokEnable();
        }

        private void bDeleteDevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Удалить" устройство
            contract.clientDevices.Remove(dgDevices.SelectedItem as clientDevice); //удаляем описание утройства из текущего договора
            updateDbGridClientDevices(); //обновляем список устройств
            controlBokEnable();
        }

        private void bEditDevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Редактировать" описание устройства
            clientDevice editableDevice = dgDevices.SelectedItem as clientDevice; // выделяем текущее описание 
            Windows.DeviceWindow w = new DeviceWindow(ref contract,ref editableDevice); //окно для редактирования описания
            w.ShowDialog();
            if (w.DialogResult == true) //если редактирование описание было выполнено
            { //находим текущее описание объектной модели и обновляем описание свойств
                contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().typeOfDevice =w.NewDevice.typeOfDevice;
                contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().modelName =w.NewDevice.modelName;
                contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().serialNumber =w.NewDevice.serialNumber;
                contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().description =w.NewDevice.description;
                updateDbGridClientDevices(); //обновляем список устройств
            }
        }

        private void bAddService_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки "добавить" услугу
            Windows.ServicesWindow w = new ServicesWindow();
            w.ShowDialog();
            if (w.DialogResult == true) //если пользователь выбрал услугу
            {
                clientDevice currentdevice = dgDevices.SelectedItem as clientDevice; //выбираем текущее устройство
                requestedService rs = new requestedService(); //формируем запрос на услугу
                rs.clientDevice = currentdevice;
                currentdevice.requestedServices.Add(rs);
                rs.service = w.Service;
                rs.cost = w.Cost;
                rs.description = w.Description;
                rs.stageOfImplementationId = 1;
                contract.approximateCost += w.Cost; //добавляем стоимость услуги к общей стоимости
                tbApproximateEndCost.Text = contract.approximateCost.ToString();
                updateDbGridService(ref currentdevice); //обновляем список услуг для данного устройства
                controlBokEnable();
            }
        }

        private void dgDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//обработчик смены текущего активного устройства
            if (dgDevices.SelectedItem != null)
            {
                clientDevice device = dgDevices.SelectedItem as clientDevice;
                updateDbGridService(ref device); //обновляем список услуг
            }
            controlBAddServiceEnable();
        }

        private void dptbApproximateEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {//обработчик изменения приммерной даты завершения заказа
            controlBokEnable();
        }

        private void bDeleteSevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки удалить услугу
            if (dgServices.SelectedItem != null)
            {
                clientDevice currentdevice = dgDevices.SelectedItem as clientDevice; //выбираем текущее устройство
                service currentService = (dgServices.SelectedItem as requestedService).service;
                requestedService rs = currentdevice.requestedServices.Where(r => r.service == currentService).FirstOrDefault();
                contract.approximateCost -= rs.cost;
                currentdevice.requestedServices.Remove(rs);
                updateDbGridService(ref currentdevice);
                tbApproximateEndCost.Text = contract.approximateCost.ToString();
            }
        }
    }
}
