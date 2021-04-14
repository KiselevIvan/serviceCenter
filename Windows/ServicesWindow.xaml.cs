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
    public partial class ServicesWindow : Window
    {//окно для выбора услуги
        public service Service { get; private set; }
        public Int32 Cost { get; private set; }
        public String Description { get; private set; }

        public ServicesWindow()
        {
            InitializeComponent();
            dgServices.ItemsSource = core.serviceCenterDB.services.ToList(); 
            //указываем источник данных для списка услуг
            bOk.IsEnabled = false;
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Добавить
            if (dgServices.SelectedItem != null | String.IsNullOrWhiteSpace(tbDescription.Text) | String.IsNullOrWhiteSpace(tbCost.Text))
            {//если все поля заполнены
                Cost = Convert.ToInt32(tbCost.Text);
                Description = tbDescription.Text;
                DialogResult = true;
                Service = dgServices.SelectedItem as service;
                Close();
            }
            else
                MessageBox.Show("Поля заполнены не корректно");

        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Отмена
            DialogResult = false;
            Close();
        }

        private void dgServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//обработчик изменения выделения услуги
            tbCost.Text = (dgServices.SelectedItem as service).baseCost.ToString(); //записываем стоимость
            bOk.IsEnabled = dgServices.SelectedItem != null; //если выделена услуга включаем кнопку Добавить
        }

        private void tbCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {//обработчик ввода текста в поле стоимость
            controlInput.DigitNumberValidationTextBox(sender, e);
        }

        private void tbCost_PreviewKeyDown(object sender, KeyEventArgs e)
        {//обработчик события преднажатие клавиши
                e.Handled = e.Key == Key.Space;            
        }
    }
}
