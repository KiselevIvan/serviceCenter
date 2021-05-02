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
    public partial class unitWindow : Window
    {//Окно для добавления Ед. измерения
        public String Name { get; private set; }
        public unitWindow()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Добавить
            if (String.IsNullOrWhiteSpace(tbName.Text)) //если данные не корректны
                MessageBox.Show("Введены не корректные данные");
            else
            {//иначе заносим значение в свойство и закрываем окно
                Name = tbName.Text;
                DialogResult = true;
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Отмена
            DialogResult = false;
            Close();
        }
    }
}
