using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;
namespace serviceCenter
{
    class core
    {
        public static serviceCenterEntities serviceCenterDB = new serviceCenterEntities();
    }

    class controlInput
    {
        public static void PhoneNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {//перехват вводимых символов и проверка на наличие посторонние знаков
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
        
        public static void DigitNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {//перехват вводимых символов и проверка на наличие посторонние знаков
            Regex regex = new Regex("^[^0-9]+$");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
