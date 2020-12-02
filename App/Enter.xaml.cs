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
using System.IO;
using System.Security.Cryptography;


namespace Library
{


    public partial class Enter : Window
    {
        public Enter()
        { 
            InitializeComponent();
        }

        private void reg_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            reg.Foreground = (Brush)bc.ConvertFrom("#2F343B");
        }

        private void reg_MouseLeave(object sender, MouseEventArgs e)
        {
            reg.Foreground = Brushes.Black;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text != "" && password.Password != "")
            {
                StreamReader sr = new StreamReader(@"../../Files/Users.txt", Encoding.GetEncoding(1251));
                bool userFlag = false;
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] elements = s.Split(' ');
                    if (login.Text == elements[0])
                    {
                        userFlag = true;
                    }
                    if (login.Text == elements[0] && password.Password.GetHashCode().ToString() == elements[1])
                    {
                        Exchange.Login = login.Text;
                        MainWindow mw = new MainWindow();
                        mw.Show();
                        this.Close();
                        return;
                    }
                }
                sr.Close();
                if (userFlag)
                {
                    MessageBox.Show("Неправильный пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    password.Password = "";
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    password.Password = "";
                    login.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }

        private void reg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Registration r = new Registration();
            r.Show();
            this.Close();

        }

        private void login_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
