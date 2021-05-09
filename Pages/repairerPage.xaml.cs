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
{//интерфейс специалиста по ремонту
    public partial class repairerPage : Page
    {
        private employee currentUser;
        public repairerPage(employee employee)
        {
            InitializeComponent();
            currentUser = employee;
            updateRequestedServices();
            updateEmployees();
            updateMyServiceExecution();
            updateModules();
        }

        private void updateRequestedServices()
        {
            dbGridRequestedServices.ItemsSource = 
                core.serviceCenterDB.View_requestedServices.Select(s=> s).ToList();
        }

        private void updateEmployees()
        {
            dbGridEmployee.ItemsSource = core.serviceCenterDB.employees.ToList();            
        }

        private void updateMyServiceExecution()
        {
            List<repairerExecuteServices_Result> dataSE = core.serviceCenterDB.repairerExecuteServices(currentUser.Id).ToList();
            dbGridMyServiceExecution.ItemsSource = dataSE;
        }

        private void updateModules()
        {
            dbGridModules.ItemsSource = core.serviceCenterDB.modules.ToList();
        }

        private void bAddForWork_Click(object sender, RoutedEventArgs e)
        {
            if (dbGridRequestedServices.SelectedItem == null)
                MessageBox.Show("Заявка не выбрана");
            else
            {
                serviceExecution se = new serviceExecution();
                se.dateOfBegin = DateTime.Now;
                se.employee = currentUser;
                Int32 id = (dbGridRequestedServices.SelectedItem as View_requestedServices).Id;
                 requestedService currentRS = core.serviceCenterDB.requestedServices.Where(rs => rs.Id == id).FirstOrDefault();
                se.requestedService = currentRS;
                se.requestedService.stageOfImplementation = core.serviceCenterDB.stagesOfImplementations.Where(st => st.name == "В работе").FirstOrDefault();
                core.serviceCenterDB.servicesExecution.Add(se);
                core.serviceCenterDB.SaveChanges();

                updateMyServiceExecution();
                updateRequestedServices();
                updateModules();
            }
        }

        private void bEndWork_Click(object sender, RoutedEventArgs e)
        {

            repairerExecuteServices_Result currentRES = dbGridMyServiceExecution.SelectedItem as repairerExecuteServices_Result;
            if (currentRES == null)
                MessageBox.Show("Заявка не выбрана");
            else
            {
                Int32 RSid = (currentRES).Id;
                requestedService currentRS = core.serviceCenterDB.requestedServices.Where(rs => rs.Id == RSid).FirstOrDefault();
                Windows.serviceExecuteResultWindow w = new Windows.serviceExecuteResultWindow();
                w.ShowDialog();
                if (w.DialogResult == true)
                {
                    var rse = core.serviceCenterDB.servicesExecution.Where(se => se.requestedService.Id == RSid).FirstOrDefault();
                    rse.dateOfEnd = DateTime.Now;
                    rse.result = w.Result;
                    rse.requestedService.stageOfImplementationId = 3;
                    core.serviceCenterDB.SaveChanges();
                    updateMyServiceExecution();
                }
            }
        }

        private void bUpdateRequestedServicesList_Click(object sender, RoutedEventArgs e)
        {
            updateRequestedServices();
        }

        private void bAddModule_Click(object sender, RoutedEventArgs e)
        {
            Windows.ModuleWindow w = new Windows.ModuleWindow();
            w.ShowDialog();
            if(w.DialogResult==true)
            {
                core.serviceCenterDB.modules.Add(w.Module);
                core.serviceCenterDB.SaveChanges();
                updateModules();
            }
        }

        private void bUpdateModules_Click(object sender, RoutedEventArgs e)
        {
            updateModules();
        }

        private void bDelModule_Click(object sender, RoutedEventArgs e)
        {
            module current = dbGridModules.SelectedItem as module;
            core.serviceCenterDB.modules.Remove(current);
            core.serviceCenterDB.SaveChanges();
            updateModules();
        }

        private void bEditModule_Click(object sender, RoutedEventArgs e)
        {
            module current = dbGridModules.SelectedItem as module;
            Windows.ModuleWindow w = new Windows.ModuleWindow(current);
            w.ShowDialog();
            //(core.serviceCenterDB.modules.Where(m => m.Id == w.Module.Id).First())
            core.serviceCenterDB.SaveChanges();
            updateModules();
        }

        private void bReplaceUpgrade_Click(object sender, RoutedEventArgs e)
        {
            Int32 reqestedServiceID = (dbGridMyServiceExecution.SelectedItem as repairerExecuteServices_Result).Id;
            requestedService currentRS= core.serviceCenterDB.requestedServices.Where(rs => rs.Id == reqestedServiceID).First();
            if (currentRS.service.name != "Замена модуля")
                MessageBox.Show("Данная заявка не требует установки модулей");
            else if (currentRS.stageOfImplementation.name == "Выполнено")
                MessageBox.Show("Заявка уже выполнена");
            else
            {
                List<upgradeReplacement> ur= core.serviceCenterDB.upgradesReplacements.Where(u => u.clientDeviceId == currentRS.clientDeviceId).ToList();
                if (ur.Count == 0)
                {
                    Windows.replaceUpgradeModuleWindow w = new Windows.replaceUpgradeModuleWindow(currentRS.clientDevice, currentUser);
                    w.ShowDialog();
                    if (w.DialogResult == true)
                    {
                        foreach (var i in w.UR)
                            core.serviceCenterDB.upgradesReplacements.Add(i);
                        core.serviceCenterDB.SaveChanges();
                    }
                }

                else
                {
                    Windows.replaceUpgradeModuleWindow w = new Windows.replaceUpgradeModuleWindow(ur,currentRS.clientDevice, currentUser);
                    w.ShowDialog();
                    if (w.DialogResult == true)
                    {
                        List<upgradeReplacement> urOrigin = core.serviceCenterDB.upgradesReplacements.Where(u => u.clientDeviceId == currentRS.clientDeviceId).ToList();
                        foreach (var i in urOrigin)
                        {
                            if (!ur.Contains(i))
                                core.serviceCenterDB.upgradesReplacements.Remove(i);
                        }

                        foreach (var i in w.UR)
                        {                            
                            var entry = core.serviceCenterDB.Entry(i);
                            if (entry.State == System.Data.Entity.EntityState.Detached)
                                core.serviceCenterDB.upgradesReplacements.Add(i);
                        }                        
                        core.serviceCenterDB.SaveChanges();
                    }
                }
            }
        }
    }
}
