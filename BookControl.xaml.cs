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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для BookControl.xaml
    /// </summary>
    public partial class BookControl : UserControl
    {
        public Book currentBook;

        public BookControl(Book b)
        {
            InitializeComponent();
            BookName = b.Name;
            Author = b.Author;
            Cover = b.Cover;
            Genre = b.Genre;
            currentBook = b;
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

        public string BookName
        {
            set{ name.Text = value; }
            get{ return name.Text; }
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

        public string Genre
        {
            get { return genre.Text; }
            set { genre.Text = "Жанр: " + value; }
        }

        private void info_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Info w = new Info(currentBook);
            w.ShowDialog();
        }


        private bool CheckInNow()
        {
            foreach (Book b in MainWindow.nowBooks)
            {
                if (b.Name == currentBook.Name)
                    return true;
            }
            return false;
        }

        private bool CheckInFuture()
        {
            foreach (Book b in MainWindow.futureBooks)
            {
                if (b.Name == currentBook.Name)
                    return true;
            }
            return false;
        }

        private bool CheckInPast()
        {
            foreach (Book b in MainWindow.pastBooks)
            {
                if (b.Name == currentBook.Name)
                    return true;
            }
            return false;
        }



        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInNow())
            {
                new MyMessageBox1("Ошибка", "Вы уже читаете эту книгу").ShowDialog();
            }
            else if (CheckInFuture())
            {
                new MyMessageBox(currentBook, "Предупреждение", "Вы уже собиратесь прочитать эту книгу. Начать читать сейчас?", "     Да     ", "     Нет     ", 0).ShowDialog();
            }
            else
            {
                MyMessageBox mb = new MyMessageBox(currentBook, "Добавление книги", "Хотите начать читать сейчас или собираетесь прочитать позже?", " Начать читать ", " Отложить ");
                mb.ShowDialog();
            }
            


        }
    }
}
