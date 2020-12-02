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

namespace Library
{

    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text != "" && password.Password != "" && password1.Password != "")
            {
                StreamReader sr = new StreamReader(@"../../Files/Users.txt", Encoding.GetEncoding(1251));
                bool userExists = false;
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] elements = s.Split(' ');
                    if (elements[0] == login.Text)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        userExists = true;
                        login.Text = "";
                        password.Password = "";
                        password1.Password = "";
                        break;
                    }
                }
                sr.Close();
                if (!userExists)
                {
                    if (password.Password == password1.Password)
                    {
                        StreamWriter sw = new StreamWriter(@"../../Files/Users.txt", true, Encoding.GetEncoding(1251));
                        sw.WriteLine(login.Text + ' ' + password.Password.GetHashCode().ToString());
                        sw.Close();
                        MessageBox.Show("Пользователь " + login.Text + " зарегистрирован.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
                        login.Text = "";
                        password.Password = "";
                        password1.Password = "";
                    }
                    else
                    {
                        MessageBox.Show("Введите одинаковые пароли.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        password.Password = "";
                        password1.Password = "";
                    }

                    
                }

            }
            else
            {
                MessageBox.Show("Заполните все поля.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void enter_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            enter.Foreground = (Brush)bc.ConvertFrom("#2F343B");
        }

        private void enter_MouseLeave(object sender, MouseEventArgs e)
        {
            enter.Foreground = Brushes.Black;
        }

        private void enter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Enter ent = new Enter();
            ent.Show();
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
