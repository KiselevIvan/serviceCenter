using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
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
    public partial class ModuleWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<moduleCharacteristic> _tabModuleCharacteristic;

        public ObservableCollection<moduleCharacteristic> TabModuleCharacteristic
        {
            get { return _tabModuleCharacteristic; }
            set
            {
                _tabModuleCharacteristic = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public module Module 
        {
            get 
            { 
                return currentModule; 
            }            
        }

        private module currentModule;
        
        public ModuleWindow()
        {
            currentModule = new module();
            init();
        }

        public ModuleWindow(module module)
        {
            currentModule = module;
            init();
            cbType.SelectedItem = currentModule.typeOfModule;
            tbName.Text = currentModule.name;
            tbInStock.Text = currentModule.numberInStock.ToString();
            tbToOrder.Text = currentModule.needToOrder.ToString();
            tbSafetyStock.Text = currentModule.safetyStock.ToString();
        }

        private void init()
        {
            _tabModuleCharacteristic = (new ObservableCollection<moduleCharacteristic>(currentModule.moduleCharacteristics.ToList()));
            DataContext = this;
            InitializeComponent();
            updateCbType();
            buttonEnableControl();
        }

        private void updateCbType()
        {
            cbType.ItemsSource = core.serviceCenterDB.typesOfModule.ToList();
        }

        private void bAddType_Click(object sender, RoutedEventArgs e)
        {
            Windows.TypeOfModuleWindow w = new TypeOfModuleWindow();
            w.ShowDialog();
            if (w.DialogResult == true)
                updateCbType();
        }

        private void bAddCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            Windows.moduleCharacteristicWindow mchw = new moduleCharacteristicWindow();
            mchw.ShowDialog();
            if (mchw.DialogResult==true)
            {                
                currentModule.moduleCharacteristics.Add(mchw.moduleCharacteristic);
                TabModuleCharacteristic.Add(mchw.moduleCharacteristic);
            }
        }

        private void buttonEnableControl()
        {
            bDelCharacteristic.IsEnabled = dgCharacteristic.SelectedItem != null;
        }

        private void bDelCharacteristic_Click(object sender, RoutedEventArgs e)
        {
            moduleCharacteristic current = dgCharacteristic.SelectedItem as moduleCharacteristic;
            currentModule.moduleCharacteristics.Remove(current);
            TabModuleCharacteristic.Remove(current);            
        }

        private void dgCharacteristic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonEnableControl();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {
            Int16 needToOrder, numberInStock, safetyStock;
            if (Int16.TryParse(tbInStock.Text, out numberInStock) &
                Int16.TryParse(tbToOrder.Text, out needToOrder) &
                Int16.TryParse(tbSafetyStock.Text, out safetyStock) &
                !String.IsNullOrWhiteSpace(tbName.Text) &
                (dgCharacteristic.Items.Count > 0) &
                cbType.SelectedItem!=null)
            {
                currentModule.name = tbName.Text;
                currentModule.needToOrder = needToOrder;
                currentModule.numberInStock = numberInStock;
                currentModule.safetyStock = safetyStock;
                currentModule.typeOfModule = cbType.SelectedItem as typeOfModule;
                this.DialogResult = true;
                Close();
            }
            else
                MessageBox.Show("Введены некорректные данные");

        }

        private void tbInStock_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            controlInput.DigitNumberValidationTextBox(sender, e);
        }

        private void tbToOrder_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            controlInput.DigitNumberValidationTextBox(sender, e);
        }

        private void tbSafetyStock_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            controlInput.DigitNumberValidationTextBox(sender, e);
        }
    }
}
