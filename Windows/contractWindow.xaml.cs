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

        public contract Contract 
        {
            get
            {
                return _contract;
            }

            private set
            {
                _contract = value;
            }
        }

        public bool ReadOnly { get; set; }

        contract _contract;
        public contractWindow(client client)
        {
            InitializeComponent();            
            Contract = new contract();
            Contract.client = client;
            controlBokEnable();
            controlButtonsEnable();

        }

        public contractWindow(contract contract)
        {
            InitializeComponent();
            this.Contract = contract;            
            updateDbGridClientDevices();
            dptbApproximateEndDate.SelectedDate = contract.approximateEndDate;
            tbApproximateEndCost.Text = contract.approximateCost.ToString();
            controlBokEnable();
            bOk.IsEnabled = true;
            bOk.Content = "Сохранить";
            bDeleteSevice.Visibility=bDeleteDevice.Visibility = Visibility.Collapsed;

            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Стадия";
            textColumn.Binding = new Binding("stageOfImplementation.name");
            dgServices.Columns.Add(textColumn);
            textColumn.Header = "Результат";
            textColumn.Binding = new Binding("stageOfImplementation.name");

            var button = new Button
            {
                Name = "bShowExecution",
                Content = "Просмотр"               
            };
            button.AddHandler(Button.ClickEvent, new RoutedEventHandler( bShowExecution_Click));
            this.servicesButtonPanel.Children.Add(button);
            button = new Button
            {
                Name = "bReturnToRepairer",
                Content = "Вернуть в работу"
            };
            button.AddHandler(Button.ClickEvent, new RoutedEventHandler(bReturnToRepairer_Click));
            this.servicesButtonPanel.Children.Add(button);
        }

        private bool requestedServiceIsExecution(requestedService currentRS)
        {            
            if (currentRS != null)
            {
                if (currentRS.servicesExecution.FirstOrDefault() != null)
                {
                    return true;
                }
                else
                    MessageBox.Show("Выполнение данной услуги еще не начато");
            }
            else
                MessageBox.Show("Услуга не выбрана");
            return false;
        }

        private void bReturnToRepairer_Click(object sender, RoutedEventArgs e)
        {
            requestedService currentRS = dgServices.SelectedItem as requestedService;
            if (requestedServiceIsExecution(currentRS))
            {
                currentRS.stageOfImplementationId = 2;
                currentRS.servicesExecution.First().dateOfEnd = null;
                core.serviceCenterDB.SaveChanges();
                MessageBox.Show("Возвращено в работу мастеру");
                clientDevice currentDevice = currentRS.clientDevice;
                updateDbGridService(ref currentDevice);
            }
        }

        private void bShowExecution_Click(object sender, RoutedEventArgs e)
        {
            requestedService currentRS = dgServices.SelectedItem as requestedService;
            if (requestedServiceIsExecution(currentRS))
                {
                    Windows.serviceExecutionWindow w = new serviceExecutionWindow(currentRS.servicesExecution.First());
                    w.ShowDialog();
                }                
        }

        private void controlButtonsEnable()
        {
            if (!ReadOnly)
            {
                bDeleteDevice.IsEnabled = bEditDevice.IsEnabled = dgDevices.SelectedItem != null;
                bAddService.IsEnabled = (dgDevices.SelectedItem != null);
                bEditService.IsEnabled = bDeleteSevice.IsEnabled = dgServices.SelectedItem != null;
            }
        }

        private void updateDbGridClientDevices()
        {//обновление данныех таблицы устройства клиента
            dgDevices.ItemsSource = Contract.clientDevices.ToList(); //устанавливаем источник данных для таблицы устройства клинта            
        }

        private void updateDbGridService(ref clientDevice device)
        {//обновление данныех таблицы устройства клиента
            dgServices.ItemsSource = device.requestedServices.ToList(); //устанавливаем источник данных для таблицы услуги            
        }

        private void controlBokEnable()
        {//метод для включения и выключения активности кнопки "Оформить", включаем кнопку когда все данные внесены
            bOk.IsEnabled = ((dgDevices.Items.Count > 0) & (dgServices.Items.Count > 0) & (dptbApproximateEndDate.SelectedDate != null));
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Оформить, формируем заказ          
            Contract.dateOfReceipt = DateTime.Now;
            Contract.approximateEndDate = dptbApproximateEndDate.SelectedDate.Value;
            DialogResult = true;
            Close();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Отмена
            DialogResult = false;
            Close();
        }

        private void bAddDevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Добовить" устройство
            Windows.DeviceWindow f = new DeviceWindow(ref _contract); //окно для заполнения объектной модели устройства
            f.ShowDialog();
            if (f.DialogResult==true) //если модель была создана
            Contract.clientDevices.Add(f.NewDevice); //добавляем новое описание устройства к текущему договору
            updateDbGridClientDevices(); //обновляем список устройств
            controlBokEnable();
            controlButtonsEnable();
        }

        private void bDeleteDevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Удалить" устройство
            Contract.clientDevices.Remove(dgDevices.SelectedItem as clientDevice); //удаляем описание утройства из текущего договора
            updateDbGridClientDevices(); //обновляем список устройств
            controlBokEnable();
            controlButtonsEnable();
        }

        private void bEditDevice_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки "Редактировать" описание устройства
            if (dgDevices.SelectedItem != null)
            {
                clientDevice editableDevice = dgDevices.SelectedItem as clientDevice; // выделяем текущее описание 
                Windows.DeviceWindow w = new DeviceWindow(ref _contract, ref editableDevice); //окно для редактирования описания
                w.ShowDialog();
                if (w.DialogResult == true) //если редактирование описание было выполнено
                { //находим текущее описание объектной модели и обновляем описание свойств
                    Contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().typeOfDevice = w.NewDevice.typeOfDevice;
                    Contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().modelName = w.NewDevice.modelName;
                    Contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().serialNumber = w.NewDevice.serialNumber;
                    Contract.clientDevices.Where(d => d.serialNumber == editableDevice.serialNumber).FirstOrDefault().description = w.NewDevice.description;
                    updateDbGridClientDevices(); //обновляем список устройств
                }
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
                Contract.approximateCost += w.Cost; //добавляем стоимость услуги к общей стоимости
                tbApproximateEndCost.Text = Contract.approximateCost.ToString();
                updateDbGridService(ref currentdevice); //обновляем список услуг для данного устройства
                controlBokEnable();
                controlButtonsEnable();
            }
        }

        private void dgDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//обработчик смены текущего активного устройства
            if (dgDevices.SelectedItem != null)
            {
                clientDevice device = dgDevices.SelectedItem as clientDevice;
                updateDbGridService(ref device); //обновляем список услуг
            }
            controlButtonsEnable();
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
                Contract.approximateCost -= rs.cost;
                currentdevice.requestedServices.Remove(rs);
                updateDbGridService(ref currentdevice);
                tbApproximateEndCost.Text = Contract.approximateCost.ToString();
                controlButtonsEnable();
            }
        }

        private void bEditService_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки "редактировать" услугу
            requestedService rs = dgServices.SelectedItem as requestedService;
            if (rs == null)
                MessageBox.Show("выберите услугу");
            else
            {
                Windows.ServicesWindow w = new ServicesWindow(rs);
                w.ShowDialog();
                if (w.DialogResult == true) //если пользователь завершил ввод
                {
                    Contract.approximateCost -= rs.cost;//вычетаем прежнюю стоимость услуги
                    rs.service = w.Service;
                    rs.cost = w.Cost;
                    rs.description = w.Description;
                    rs.stageOfImplementationId = 1;

                    Contract.approximateCost += w.Cost; //добавляем стоимость услуги к общей стоимости
                    tbApproximateEndCost.Text = Contract.approximateCost.ToString();
                    clientDevice currentDevice = rs.clientDevice;
                    updateDbGridService(ref currentDevice); //обновляем список услуг для данного устройства
                    controlBokEnable();
                    controlButtonsEnable();
                }
            }
        }

        private void dgServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            controlButtonsEnable();
        }
    }
}
