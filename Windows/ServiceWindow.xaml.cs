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
    public partial class ServiceWindow : Window
    {//окно добавление\редактирования услуги
        public service Service { get; private set; }
        public ServiceWindow()
        {//конструктор по умолчанию
            InitializeComponent();
            bOk.Content = "Добавить";
        }

        public ServiceWindow(service service)
        {//конструктор окна для режима редактирования существующей услуги, параметр исходное описание услуги
            InitializeComponent();
            this.Service = service;
            tbName.Text = service.name;
            tbBaseCost.Text = service.baseCost.ToString();
            bOk.Content = "Сохранить";
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Ок 
            Int32 cost;
            if (String.IsNullOrWhiteSpace(tbName.Text) | 
                String.IsNullOrWhiteSpace(tbBaseCost.Text)|
                !Int32.TryParse(tbBaseCost.Text,out cost)
                ) //если поля заполнены не корректно
                MessageBox.Show("Введены не корректные даные");
            else
            {//если поля заполнены корректно
                if (Service==null)
                Service = new service();
                Service.name = tbName.Text;
                Service.baseCost = cost;
                DialogResult = true;
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки отмена
            DialogResult = false;
            Close();
        }        

        private void tbBaseCost_PreviewKeyDown(object sender, KeyEventArgs e)
        {//обработчик события преднажатие клавиши
            controlInput.DigitNumberValidationTextBox(sender, e);
        }
    }
}
