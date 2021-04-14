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
    public partial class DeviceWindow : Window
    {//окно добавления\редактирования устройста клиента
        public clientDevice NewDevice { get; private set; } //свойство, которое содержит описаное устройство
        private Int32 contractId; //Ид договора, в рамках которого осуществляется ремонт
        
        public DeviceWindow(ref contract contract)
        {//добавть описание нового устройства, параметр ссылка на договор
            InitializeComponent();
            this.contractId =contract.Id;
            cbTypeOfDevice.ItemsSource = core.serviceCenterDB.typesOfDevices.ToList();
        }

        public DeviceWindow(ref contract contract,ref clientDevice clientDevice)
        {//редактировать существующее описание устройства, 1 параметр ссылка на договор, 2 параметр ссылка на редактируемое описание
            InitializeComponent();
            this.contractId = contract.Id;
            cbTypeOfDevice.ItemsSource = core.serviceCenterDB.typesOfDevices.ToList();
            NewDevice = clientDevice;
            //заполняем поля для ввода существующими данными
            cbTypeOfDevice.SelectedIndex = NewDevice.typeOfDeviceId - 1;
            tbModelName.Text = NewDevice.modelName;
            tbSerialNumber.Text = NewDevice.serialNumber;
            tbDescription.Text = NewDevice.description;
        }

        private void bAddTypeOfDevice_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Добавить новый тип 
            Windows.typeOfDeviceWindow w = new typeOfDeviceWindow(); //окно для добавления нового типа
            w.ShowDialog();
        }

        public bool SetDeviceData()
        {//функция для проверки заполнения полей данных и заполнения объектной модели обновленными данными
            if (//если поля заполнены не корректно
                    (cbTypeOfDevice.SelectedItem ==null) | 
                    (String.IsNullOrWhiteSpace(tbModelName.Text)) | 
                    (String.IsNullOrWhiteSpace(tbSerialNumber.Text)) |
                    (String.IsNullOrWhiteSpace(tbDescription.Text))
               )
            {                
                return false;
            }
            else
            {//если поля заполнены корректно, обновляем объектную модель
                NewDevice = new clientDevice();
                NewDevice.typeOfDevice = (cbTypeOfDevice.SelectedItem as typeOfDevice);
                NewDevice.modelName = tbModelName.Text;
                NewDevice.serialNumber = tbSerialNumber.Text;
                NewDevice.description = tbDescription.Text;
                NewDevice.contractId = contractId;
                return true;
            }
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Ок
                if (SetDeviceData()) //если данные объектной модели корректно обновлены
                {
                    this.DialogResult = true;
                    Close();
                }
                else
                    MessageBox.Show("Поля заполнены не корректно");
            
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        { //обработчик кнопки Отмена
            this.DialogResult = false;
            Close();
        }
    }
}
