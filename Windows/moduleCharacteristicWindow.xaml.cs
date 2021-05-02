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
    public partial class moduleCharacteristicWindow : Window
    {        
        public moduleCharacteristic moduleCharacteristic { get; private set; }
        public moduleCharacteristicWindow()
        
        {//конструктор
            InitializeComponent();
            updateCbCharacteristics();
            moduleCharacteristic = new moduleCharacteristic();
        }

        private void updateCbCharacteristics()
        {
            cbCharacteristics.ItemsSource = core.serviceCenterDB.characteristics.ToList();
        }

        private void bAddCharacteristic_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки добавить
            Windows.characteristicWindow chw = new characteristicWindow();
            chw.ShowDialog();
            if (chw.DialogResult == true) //если пользователь ввел значение
            {//создаем объект, заполняем и добавляем в модель бд
                characteristic newCharacteristic = new characteristic();
                newCharacteristic.name = chw.CharacteristicName;
                newCharacteristic.unit = chw.Unit;                
                core.serviceCenterDB.characteristics.Add(newCharacteristic);
                core.serviceCenterDB.SaveChanges();
                updateCbCharacteristics();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки отмена
            DialogResult = false;
            Close();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Ок
            if (String.IsNullOrWhiteSpace(tbValue.Text) || (cbCharacteristics.SelectedItem == null))
                MessageBox.Show("Введены не корректные данные");
            else
            {
                moduleCharacteristic.characteristic = cbCharacteristics.SelectedItem as characteristic;
                moduleCharacteristic.Value = tbValue.Text;
                DialogResult = true;
                Close();
            }            
        }
    }
}
