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
    public partial class characteristicWindow : Window
    {//Окно добавления и редактирования отдельной характеристики модуля
        public String CharacteristicName { get; private set; }
        public unit Unit { get; private set; }

        private void updateCbUnits()
        {//метод обновления данных выпадающего списка Ед. измерения
            cbUnits.ItemsSource = core.serviceCenterDB.units.ToList();
        }

        public characteristicWindow()
        {
            InitializeComponent();
            updateCbUnits();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Ок
            if (String.IsNullOrWhiteSpace(tbName.Text) | (cbUnits.SelectedItem == null))
                MessageBox.Show("Введены не корректные данные");
            else
            {//иначе заполняем свойства и закрываем окно
                CharacteristicName = tbName.Text;
                Unit = cbUnits.SelectedItem as unit;
                this.DialogResult = true;
                Close();
            }            
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Отмена
            this.DialogResult = false;
            Close(); 
        }

        private void bAddUnit_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки добавить
            Windows.unitWindow uw = new unitWindow();
            uw.ShowDialog();
            if (uw.DialogResult==true) //если пользователь ввел значение
            {//создаем объект, заполняем и добавляем в модель бд
                unit newUnit = new unit();
                newUnit.name = uw.Name;
                core.serviceCenterDB.units.Add(newUnit);
                core.serviceCenterDB.SaveChanges();
                updateCbUnits();
            }
        }
    }
}
