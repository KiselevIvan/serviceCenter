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
{//окно добавления типа модуля
    public partial class TypeOfModuleWindow : Window
    {
        public TypeOfModuleWindow()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки Добавить
            if (String.IsNullOrWhiteSpace(tbName.Text)) //если название отсутствует или состоит из пробелов
                MessageBox.Show("Введены не корректные данные");
            else
            {//иначе создаем новый объект и устанавливаем для него свойство имя, далее заносим в бд, сохраняем и закрываем окно
                typeOfModule newType = new typeOfModule();
                newType.name = tbName.Text;
                core.serviceCenterDB.typesOfModule.Add(newType);
                core.serviceCenterDB.SaveChanges();
                this.DialogResult = true;
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Отмена
            this.DialogResult = false;
            Close();
        }
    }
}
