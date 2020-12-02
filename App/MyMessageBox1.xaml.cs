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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для MyMessageBox1.xaml
    /// </summary>
    public partial class MyMessageBox1 : Window
    {
        public MyMessageBox1(string head, string question)
        {
            InitializeComponent();
            Head = head;
            Question = question;
            ResizeMode = ResizeMode.NoResize;
        }

        public string Head
        {
            set { Title = value; }
            get { return Title; }
        }

        public string Question
        {
            set { question.Text = value; }
            get { return question.Text; }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
