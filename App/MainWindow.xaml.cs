using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;



namespace Library
{

    public class Exchange
    {
        public static string Login;
        public static string path;
    }

    public partial class MainWindow : Window
    {
        public bool flag = true;
        public static List<Book> nowBooks = new List<Book>();
        public static List<Book> futureBooks = new List<Book>();
        public static List<Book> pastBooks = new List<Book>();
        public static List<Book> books = new List<Book>();


        public void addBooks()
        {
            this.sp.Children.Clear();
            StreamReader streamReader = new StreamReader("../../Files/Books.txt", Encoding.GetEncoding(1251));
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                if (str != "")
                {
                    string[] strArray = str.Split('|');
                    MainWindow.books.Add(new Book(strArray[0], strArray[1], (ImageSource)new BitmapImage(new Uri(strArray[2], UriKind.Relative)), int.Parse(strArray[3]), int.Parse(strArray[4]), strArray[5], strArray[6]));
                }
            }
            streamReader.Close();
            int num = 0;
            foreach (Book book in books)
            {
                BookControl bookControl = new BookControl(book);
                if (num % 3 == 0)
                    bookControl.Margin = new Thickness(-490.0, 20.0, 0.0, 0.0);
                else if (num % 3 == 1)
                    bookControl.Margin = new Thickness(7.0, -316.0, 0.0, 0.0);
                else
                    bookControl.Margin = new Thickness(496.0, -316.0, 0.0, 0.0);
                bookControl.MouseEnter += new MouseEventHandler(BookControl_MouseEnter);
                bookControl.MouseLeave += new MouseEventHandler(BookControl_MouseLeave);
                sp.Children.Add(bookControl);
                ++num;
            }
        }


        public void addNowBooks()
        {
            StreamReader streamReader = new StreamReader(Exchange.path + "/ReadingNow.txt", Encoding.GetEncoding(1251));
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                if (str != "")
                {
                    string[] strArray = str.Split('|');
                    nowBooks.Add(new Book(strArray[0], strArray[1], (ImageSource)new BitmapImage(new Uri(strArray[2], UriKind.Absolute)), int.Parse(strArray[3]), int.Parse(strArray[4]), strArray[5], strArray[6], DateTime.Parse(strArray[7])));
                }
            }
            streamReader.Close();
        }



        public void addPastBooks()
        {
            StreamReader streamReader = new StreamReader(Exchange.path + "/HasRead.txt", Encoding.GetEncoding(1251));
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                if (str != "")
                {
                    string[] strArray = str.Split('|');
                    MainWindow.pastBooks.Add(new Book(strArray[0], strArray[1], (ImageSource)new BitmapImage(new Uri(strArray[2], UriKind.Absolute)), int.Parse(strArray[3]), int.Parse(strArray[4]), strArray[5], strArray[6], DateTime.Parse(strArray[7]), DateTime.Parse(strArray[8])));
                }
            }
            streamReader.Close();
        }



        public void addFutureBooks()
        {
            StreamReader streamReader = new StreamReader(Exchange.path + "/WillRead.txt", Encoding.GetEncoding(1251));
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                if (str != "")
                {
                    string[] strArray = str.Split('|');
                    MainWindow.futureBooks.Add(new Book(strArray[0], strArray[1], (ImageSource)new BitmapImage(new Uri(strArray[2], UriKind.Absolute)), int.Parse(strArray[3]), int.Parse(strArray[4]), strArray[5], strArray[6]));
                }
            }
            streamReader.Close();
        }


        public void createDirectory()
        {
            if (Directory.Exists(Exchange.path))
                return;
            Directory.CreateDirectory(Exchange.path);
            File.Create(Exchange.path + "/ReadingNow.txt").Close();
            File.Create(Exchange.path + "/HasRead.txt").Close();
            File.Create(Exchange.path + "/WillRead.txt").Close();
        }


        public string Status
        {
            set { tb.Text = value; }
            get { return tb.Text; }
        }

        public MainWindow()
        {
            InitializeComponent();
            now.IsChecked = true;
            //Status = "Добро пожаловать в приложение \"Моя библиотека \"! ";
            Exchange.path = "../../Files/" + Exchange.Login;
            books.Clear();
            nowBooks.Clear();
            pastBooks.Clear();
            futureBooks.Clear();
            createDirectory();
            addBooks();
            addNowBooks();
            addFutureBooks();
            addPastBooks();
            ShowBooks();
        }

        private void BookControl_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as BookControl).Effect = (Effect)new DropShadowEffect()
            {
                BlurRadius = 20.0,
                ShadowDepth = 1.0
            };
        }

        private void BookControl_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as BookControl).Effect = (Effect)null;
        }

        private void BookControlNow_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as BookControlNow).Effect = (Effect)new DropShadowEffect()
            {
                BlurRadius = 20.0,
                ShadowDepth = 1.0
            };
        }

        private void BookControlNow_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as BookControlNow).Effect = (Effect)null;
        }

        private void BookControlFuture_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as BookControlFuture).Effect = (Effect)new DropShadowEffect()
            {
                BlurRadius = 20.0,
                ShadowDepth = 1.0
            };
        }

        private void BookControlFuture_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as BookControlFuture).Effect = (Effect)null;
        }

        private void BookControlPast_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as BookControlPast).Effect = (Effect)new DropShadowEffect()
            {
                BlurRadius = 20.0,
                ShadowDepth = 1.0
            };
        }

        private void BookControlPast_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as BookControlPast).Effect = (Effect)null;
        }


        public void ShowBooks()
        {

            foreach (Window w in App.Current.Windows)
            {
                if (w is MainWindow)
                {


                  

                    sp1.Children.Clear();
                    if (now.IsChecked)
                    {
                        int num = 0;
                        foreach (Book nowBook in nowBooks)
                        {
                            BookControlNow bookControlNow = new BookControlNow(nowBook);
                            if (num % 3 == 0)
                                bookControlNow.Margin = new Thickness(-490.0, 20.0, 0.0, 0.0);
                            else if (num % 3 == 1)
                                bookControlNow.Margin = new Thickness(7.0, -316.0, 0.0, 0.0);
                            else
                                bookControlNow.Margin = new Thickness(496.0, -316.0, 0.0, 0.0);
                            bookControlNow.MouseEnter += new MouseEventHandler(BookControlNow_MouseEnter);
                            bookControlNow.MouseLeave += new MouseEventHandler(BookControlNow_MouseLeave);
                            sp1.Children.Add((UIElement)bookControlNow);
                            ++num;
                        }
                    }
                    else if (past.IsChecked)
                    {
                        int num = 0;
                        foreach (Book pastBook in pastBooks)
                        {
                            BookControlPast bookControl = new BookControlPast(pastBook);
                            if (num % 3 == 0)
                                bookControl.Margin = new Thickness(-490.0, 20.0, 0.0, 0.0);
                            else if (num % 3 == 1)
                                bookControl.Margin = new Thickness(7.0, -316.0, 0.0, 0.0);
                            else
                                bookControl.Margin = new Thickness(496.0, -316.0, 0.0, 0.0);
                            bookControl.MouseEnter += new MouseEventHandler(BookControlPast_MouseEnter);
                            bookControl.MouseLeave += new MouseEventHandler(BookControlPast_MouseLeave);
                            this.sp1.Children.Add((UIElement)bookControl);
                            ++num;
                        }
                    }
                    else
                    {
                        int num = 0;
                        foreach (Book futureBook in futureBooks)
                        {
                            BookControlFuture bookControl = new BookControlFuture(futureBook);
                            if (num % 3 == 0)
                                bookControl.Margin = new Thickness(-490.0, 20.0, 0.0, 0.0);
                            else if (num % 3 == 1)
                                bookControl.Margin = new Thickness(7.0, -316.0, 0.0, 0.0);
                            else
                                bookControl.Margin = new Thickness(496.0, -316.0, 0.0, 0.0);
                            bookControl.MouseEnter += new MouseEventHandler(BookControlFuture_MouseEnter);
                            bookControl.MouseLeave += new MouseEventHandler(BookControlFuture_MouseLeave);
                            this.sp1.Children.Add((UIElement)bookControl);
                            ++num;
                        }
                    }
                }
            }
        }

        private void now_Checked(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                Status = "Добро пожаловать в приложение \"Моя библиотека \"! Здесь находятся книги, которые вы сейчас читаете"; ;
            }
            else
            {
                Status = "Здесь находятся книги, которые вы сейчас читаете";
            }
            if (nowBooks.Count == 0)
            {
                Status += ". Пока вы ничего не читаете";
            }
            this.now.IsCheckable = false;
            this.past.IsCheckable = true;
            this.future.IsCheckable = true;
            this.past.IsChecked = false;
            this.future.IsChecked = false;
            ShowBooks();
            flag = false;
        }

        private void past_Checked(object sender, RoutedEventArgs e)
        {
            Status = "Здесь находятся книги, которые вы уже прочитали";
            if (pastBooks.Count == 0)
            {
                Status += ". Пока вы ничего не прочитали";
            }
            this.past.IsCheckable = false;
            this.now.IsCheckable = true;
            this.future.IsCheckable = true;
            this.now.IsChecked = false;
            this.future.IsChecked = false;
            ShowBooks();
        }

        private void future_Checked(object sender, RoutedEventArgs e)
        {
            Status = "Здесь находятся книги, которые вы собираетесь прочитать";
            if (futureBooks.Count == 0)
            {
                Status += ". Пока вы ничего не собираетесь прочитать";
            }
            this.future.IsCheckable = false;
            this.now.IsCheckable = true;
            this.past.IsCheckable = true;
            this.now.IsChecked = false;
            this.past.IsChecked = false;
            ShowBooks();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowBooks();
            if (future.IsChecked)
            {
                Status = "Здесь находятся книги, которые вы собираетесь прочитать";

            }
            else if (now.IsChecked)
            {
                Status = "Здесь находятся книги, которые вы сейчас читаете";
            }
            else
            {
                Status = "Здесь находятся книги, которые вы уже прочитали";
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            new MyMessageBox((Window)this.w, "Подтверждение", "Вы уверены, что хотите выйти из аккаунта " + Exchange.Login + "?", "     Да     ", "     Нет     ").ShowDialog();
        }

        private void stats_Click(object sender, RoutedEventArgs e)
        {
            Status = "Здесь вы можете увидеть вашу читательскую статистику";
            sp1.Children.Clear();
            TextBlock total = new TextBlock();
            total.Text = "Всего книг прочитано: " + pastBooks.Count;
            total.Margin = new Thickness(20, 20, 0, 0);
            sp1.Children.Add(total);

            TextBlock sum = new TextBlock();
            int s = SumTime();
            sum.Text = "Суммарное время прочтения: " + s + " дней";
            sum.Margin = new Thickness(20, 10, 0, 0);
            sp1.Children.Add(sum);

            TextBlock average = new TextBlock();
            if (pastBooks.Count > 0)
                average.Text = "Среднее время прочтения книги: " + Math.Round(s/(decimal)pastBooks.Count,1).ToString() + " дней";
            else
                average.Text = "Среднее время прочтения книги: 0 дней";
            average.Margin = new Thickness(20, 10, 0, 0);
            sp1.Children.Add(average);

            TextBlock avVelocity = new TextBlock();
            if (s > 0) {
                avVelocity.Text = "Средняя скорость прочтения: " + Math.Round(SumPages() / (decimal)s, 1).ToString() +  " cтр/день";
            }
            else
            {
                if (s > 0)
                {
                    avVelocity.Text = "Средняя скорость прочтения: 0 cтр/день";
                }
            }
            avVelocity.Margin = new Thickness(20, 10, 0, 0);
            sp1.Children.Add(avVelocity);

            /*TextBlock favGenres = new TextBlock();
            if (s > 0)
            {
                List<string> genres = FavGenres();
                favGenres.Text = "Любимый жанр: " + genres[0];
                genres.RemoveAt(0);
                foreach (string el in genres)
                {
                    favGenres.Text += ", " + el;
                }
                favGenres.Margin = new Thickness(20, 10, 0, 0);
                sp1.Children.Add(favGenres);
            }*/
        }

        private List<string> FavGenres()
        {
            List<string> genres = new List<string>();
            Dictionary<string, int> quantity = new Dictionary<string, int>();
            foreach (Book b in pastBooks)
            {
                if (quantity.ContainsKey(b.Genre))
                {
                    quantity[b.Genre]++;
                }
                else
                {
                    quantity[b.Genre] = 1;
                }
            }
            int max = 0;
            foreach (Book b in pastBooks)
            {
                if (quantity[b.Genre] > max)
                    max = quantity[b.Genre];
            }
            foreach (Book b in pastBooks)
            {
                if (quantity[b.Genre] == max)
                    genres.Add(b.Genre);
            }
            return genres;
        }

        private int SumPages()
        {
            int count = 0;
            foreach (Book b in pastBooks)
            {
                count += b.Pages;
            }
            return count;
        }

        private int SumTime()
        {
            int count = 0;
            foreach (Book b in pastBooks)
            {
                TimeSpan ts = b.Finish.Subtract(b.Start);
                count += ts.Days;
            }
            return count;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowBooks();
        }

        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Status = "В библиотеке вы можеть выбрать книгу";
        }
    }
}
