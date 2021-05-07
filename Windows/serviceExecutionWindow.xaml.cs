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
    public partial class serviceExecutionWindow : Window
    {
        public serviceExecutionWindow(serviceExecution currentSE)
        {
            InitializeComponent();
            tbStage.Text = currentSE.requestedService.stageOfImplementation.name.ToString();
            tbDescription.Text = currentSE.requestedService.description;
            tbEmployeeName.Text = currentSE.employee.FIO;
            tbPhone.Text = currentSE.employee.phoneNumber.ToString();
            dpDateOfBegin.SelectedDate=currentSE.dateOfBegin;
            dpDateOfEnd.SelectedDate=currentSE.dateOfEnd;
            tbResult.Text = currentSE.result;            
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
