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
    public partial class typeOfDeviceWindow : Window
    {//окно добавления\редактирования типа устройств
        public typeOfDeviceWindow()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Добавить
            if (String.IsNullOrWhiteSpace(tbName.Text)) //если название отсутствует или состоит из пробелов
                MessageBox.Show("Введены не корректные данные");
            else
            {//иначе создаем новый объект и устанавливаем для него свойство имя, далее заносим в бд, сохраняем и закрываем окно
                typeOfDevice newType= new typeOfDevice();
                newType.name = tbName.Text;
                core.serviceCenterDB.typesOfDevices.Add(newType);
                core.serviceCenterDB.SaveChanges();
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Отмена
            Close();
        }
    }
}
