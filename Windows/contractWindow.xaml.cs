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
        }

        private void updateDbGridClientDevices()
        {//обновление данныех таблицы устройства клиента
            dgDevices.ItemsSource = contract.clientDevices.ToList(); //устанавливаем источник данных для таблицы устройства клинта            
        }

        private void updateDbGridService(ref clientDevice device)
        {//обновление данныех таблицы устройства клиента
            dgServices.ItemsSource = device.requestedServices.ToList(); //устанавливаем источник данных для таблицы устройства клинта            
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Ок
            contract.clientId = client.Id;
            core.serviceCenterDB.contracts.Add(contract);
            core.serviceCenterDB.SaveChanges();
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
        }

        private void bDeleteDevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Удалить" устройство
            contract.clientDevices.Remove(dgDevices.SelectedItem as clientDevice); //удаляем описание утройства из текущего договора
            updateDbGridClientDevices(); //обновляем список устройств
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
    }
}
