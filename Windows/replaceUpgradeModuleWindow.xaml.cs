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
    public partial class replaceUpgradeModuleWindow : Window
    {

        public List<upgradeReplacement> UR { get; private set; }
        private clientDevice currentDevice;
        private employee currentEmployee;
        public replaceUpgradeModuleWindow(clientDevice device,employee employee)
        {
            InitializeComponent();
            UR = new List<upgradeReplacement>();
            currentDevice = device;
            currentEmployee = employee;
            updateModulesList();
        }

        public replaceUpgradeModuleWindow(List<upgradeReplacement> ur,clientDevice device,employee employee)
        {
            InitializeComponent();
            UR = ur;
            currentDevice = device;
            currentEmployee = employee;
            updateModulesList();
        }

        private void updateModulesList()
        {
            dgModules.ItemsSource = UR.ToList();
        }

        private void bAddModule_Click(object sender, RoutedEventArgs e)
        {
            Windows.selectModuleWindow w = new selectModuleWindow();
            w.ShowDialog();
            if (w.DialogResult == true)
            {
                w.currentURmodule.clientDevice = currentDevice;
                w.currentURmodule.employee = currentEmployee;
                UR.Add(w.currentURmodule);
                updateModulesList();
            }
        }

        private void bDelModule_Click(object sender, RoutedEventArgs e)
        {
            UR.Remove(dgModules.SelectedItem as upgradeReplacement);
            updateModulesList();
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            Windows.selectModuleWindow w = new selectModuleWindow(dgModules.SelectedItem as upgradeReplacement);
            w.ShowDialog();
            if (w.DialogResult == true)
            {
                UR.Remove(dgModules.SelectedItem as upgradeReplacement);
                w.currentURmodule.clientDevice = currentDevice;
                w.currentURmodule.employee = currentEmployee;
                UR.Add(w.currentURmodule);
                updateModulesList();
            }
        }
    }
}
