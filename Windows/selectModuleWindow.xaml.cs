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
    public partial class selectModuleWindow : Window
    {

        public upgradeReplacement currentURmodule { get; private set; }
        private Int32 _price;

        public selectModuleWindow()
        {
            InitializeComponent();
            currentURmodule = new upgradeReplacement();
            cbType.ItemsSource = core.serviceCenterDB.typesOfModule.ToList();            
        }

        public selectModuleWindow(upgradeReplacement ur)
        {
            InitializeComponent();
            currentURmodule = ur;
            cbType.ItemsSource = core.serviceCenterDB.typesOfModule.ToList();
            cbType.SelectedItem = currentURmodule.module.typeOfModule;
            dgModules.SelectedItem=currentURmodule.module;
            tbPrice.Text = currentURmodule.modulePrice.ToString();
            tbDescription.Text = currentURmodule.description.ToString();
        }

        private void updateModulesList()
        {
            if (cbType.SelectedItem != null)
            {
                Int32 Id = (cbType.SelectedItem as typeOfModule).Id;
                List<module> modules= core.serviceCenterDB.modules.Where(m => m.typeOfModuleId == Id && m.numberInStock>0).ToList();
                dgModules.ItemsSource = modules;
            }
        }

        private bool isValid()
        {
            if (cbType.SelectedItem != null &
                dgModules.SelectedItem != null &
                !String.IsNullOrWhiteSpace(tbPrice.Text) &
                Int32.TryParse(tbPrice.Text, out _price) &
                !String.IsNullOrWhiteSpace(tbDescription.Text)
                )
                return true;
            else
                return false;
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {
            if (!isValid())
                MessageBox.Show("Введены не корректные данные");
            else
            {
                currentURmodule.module = dgModules.SelectedItem as module;
                currentURmodule.modulePrice = _price;
                currentURmodule.description = tbDescription.Text;
                DialogResult = true;
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void bShow_Click(object sender, RoutedEventArgs e)
        {
            Windows.ModuleWindow wM = new ModuleWindow(dgModules.SelectedItem as module);
            wM.bAddCharacteristic.Visibility = wM.bAddType.Visibility = wM.bDelCharacteristic.Visibility = wM.bOk.Visibility = Visibility.Collapsed;
            wM.bCancel.Content = "Закрыть";
            wM.cbType.IsEnabled=wM.tbName.IsEnabled=wM.tbInStock.IsEnabled=wM.tbToOrder.IsEnabled=wM.tbSafetyStock.IsEnabled=false;
            wM.ShowDialog();
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateModulesList();
        }

        private void tbPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           controlInput.DigitNumberValidationTextBox(sender,e);
        }
    }
}
