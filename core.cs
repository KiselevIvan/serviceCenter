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

        public static void DigitNumberValidationTextBox(object sender, KeyEventArgs e)
        {//перехват нажимаемых клавишь и проверка на наличие посторонние знаков

            HashSet<Key> allow = new HashSet<Key>{
                Key.D0,Key.D1,Key.D2,Key.D3,Key.D4,Key.D5,Key.D6,Key.D7,Key.D8,Key.D9,
                Key.Back,Key.Delete,
                Key.Left,Key.Right,Key.End,Key.Home,
                Key.Tab};
            e.Handled = !allow.Contains(e.Key);
        }
    }
}
