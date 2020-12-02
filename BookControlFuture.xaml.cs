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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library
{

    public partial class BookControlFuture : UserControl
    {
        public Book currentBook;

        public BookControlFuture(Book b)
        {
            InitializeComponent();
            BookName = b.Name;
            Author = b.Author;
            Cover = b.Cover;
            currentBook = b;
        }

        public string BookName
        {
            set
            {
                name.Text = value;
            }
            get
            {
                return name.Text;
            }
        }

        public string Author
        {
            set { author.Text = value; }
            get { return author.Text; }
        }

        public ImageSource Cover
        {
            set { img.Source = value; }
            get { return img.Source; }
        }


        private void add_Click(object sender, RoutedEventArgs e)
        {
            new MyMessageBox(currentBook, "Подтверждение", "Вы точно хотите начать чтений этой книги?", "     Да     ", "     Нет     ", 0).ShowDialog();
        }

        private void info_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            info.Foreground = (Brush)bc.ConvertFrom("#2F343B");
        }

        private void info_MouseLeave(object sender, MouseEventArgs e)
        {
            info.Foreground = Brushes.Black;
        }


        private void info_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Info w = new Info(currentBook);
            w.ShowDialog();
        }

        private void delete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new MyMessageBox(currentBook, "Удаление книги", "Вы уверены, что больше не собираетесь читать эту книгу?", "     Да     ", "     Нет     ", 3).ShowDialog();
        }
    }
}
