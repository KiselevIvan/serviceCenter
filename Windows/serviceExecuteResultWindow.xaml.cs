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
    public partial class serviceExecuteResultWindow : Window
    {//Окно для ввода результата выполнения услуги мастером
        public String Result { get; private set; }

        public serviceExecuteResultWindow()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//Обработчик кнопки Сохранить
            if (String.IsNullOrWhiteSpace(tbResult.Text)) //если поле не заполнено
                MessageBox.Show("Не корректно заполнено поле");
            else
            {//иначе заносим текст в свойство и закрываем окно
                Result = tbResult.Text;
                this.DialogResult = true;
                Close();
            }            
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
