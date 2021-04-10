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
    public partial class userChangePasswordWindow : Window
    {//Окно для ввода нового пароля
        public string Password { get; private set; }
        //Введенный пароль

        public userChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки ок
            if (pbNewPass.Password != pbRepeatNewPass.Password)
                //если пароль и его подтверждение не совпадают
                MessageBox.Show("Пароли отличаются");
            else
            {
                this.Password = pbNewPass.Password;
                //выставляем введенный пароль в соответствующие свойство
                DialogResult = true; //если пользователь ввел пароль, возвращаем истину в качестве результата
                Close();
            }
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; //если пользователь нажал отмену возвращаем ложь
            Close();
        }
    }
}
