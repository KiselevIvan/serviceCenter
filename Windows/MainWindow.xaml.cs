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
{//основное окно приложения
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Pages.authenticationPage()); //устанавливаем страницу авторизации
        }

        private void bExit_Click(object sender, RoutedEventArgs e)
        {//обработчик кнопки выход
            mainFrame.Navigate(new Pages.authenticationPage()); 
        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {//обработчик изменения страницы
            if (!(e.Content is Page page)) return; 
            if (page is Pages.authenticationPage) //если текущая страница авторизация, то скрываем кнопку выход
                bExit.Visibility = Visibility.Hidden;
            else
                bExit.Visibility = Visibility.Visible;
        }
    }
}
