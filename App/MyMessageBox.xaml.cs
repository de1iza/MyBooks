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
    
    public partial class MyMessageBox : Window
    {
        Book currentBook;
        Window w;

        public MyMessageBox(Book b, string head, string question, string t1, string t2)
        {
            currentBook = b;
            InitializeComponent();
            Question = question;
            Answer1 = t1;
            Answer2 = t2;
            Head = head;
            ans1.Click += new RoutedEventHandler(button1_Click);
            ans2.Click += new RoutedEventHandler(button2_Click);
            
            ResizeMode = ResizeMode.NoResize;
        }

        public MyMessageBox(Window win, string head, string question, string t1, string t2)
        {
            InitializeComponent();
            w = win;
            Question = question;
            Answer1 = t1;
            Answer2 = t2;
            Head = head;
            ans1.Click += new RoutedEventHandler(exit1_Click);
            ans2.Click += new RoutedEventHandler(exit2_Click);
            
            ResizeMode = ResizeMode.NoResize;
        }

        public MyMessageBox(Book b, string head, string question, string t1, string t2, int flag)
        {
            InitializeComponent();
            //w = win;
            currentBook = b;
            Question = question;
            Answer1 = t1;
            Answer2 = t2;
            Head = head;
            if (flag == 0) // move from future to now
            {
                ans1.Click += new RoutedEventHandler(button3_Click);
                ans2.Click += new RoutedEventHandler(exit2_Click);
            }
            else if (flag == 1) // delete from now 
            {
                
                ans1.Click += new RoutedEventHandler(button4_Click);
                ans2.Click += new RoutedEventHandler(exit2_Click);
            }
            else if (flag == 2) // move from now to past
            {
                ans1.Click += new RoutedEventHandler(button5_Click);
                ans2.Click += new RoutedEventHandler(exit2_Click);
            }
            else if (flag == 3) // delete from future
            {
                ans1.Click += new RoutedEventHandler(button6_Click);
                ans2.Click += new RoutedEventHandler(exit2_Click);
            }
            else if (flag == 4) // delete from past
            {
                ans1.Click += new RoutedEventHandler(button7_Click);
                ans2.Click += new RoutedEventHandler(exit2_Click);
            }
            ResizeMode = ResizeMode.NoResize;
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


        private void button5_Click(object sender, RoutedEventArgs e)
        {

            currentBook.Finish = DateTime.Now.Date;



            StreamWriter sw = new StreamWriter(Exchange.path + "/" + "HasRead.txt", true, Encoding.GetEncoding(1251)); // add to past
            sw.Write(currentBook.Name + '|');
            sw.Write(currentBook.Author + '|');
            sw.Write(currentBook.Cover.ToString() + '|');
            sw.Write(currentBook.Pages.ToString() + '|');
            sw.Write(currentBook.YearOfPublication.ToString() + '|');
            sw.Write(currentBook.Genre + '|');
            sw.Write(currentBook.Description + '|');
            sw.Write(currentBook.Start.ToShortDateString() + '|');
            sw.Write(currentBook.Finish.ToShortDateString());
            sw.WriteLine();
            sw.Close();
            MainWindow.pastBooks.Add(currentBook);


            StreamReader sr = new StreamReader(Exchange.path + "/ReadingNow.txt", Encoding.GetEncoding(1251));
            string res = "";
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                string[] elements = s.Split('|');
                if (elements[0] == currentBook.Name)
                {
                    continue;
                }
                else
                {
                    res += s + '\n';
                }
            }
            sr.Close();

            StreamWriter sw1 = new StreamWriter(Exchange.path + "/" + "ReadingNow.txt", false, Encoding.GetEncoding(1251)); // delete from now
            sw1.Write(res);

            sw1.Close();
            MainWindow.nowBooks.Remove(currentBook);
            //w.ShowBooks();

            foreach (Window W in App.Current.Windows)
            {
                if (w is MainWindow)
                {
                    ((MainWindow)w).ShowBooks();         // НЕ ОБНОВЛЯЕТСЯ АВТОМАТИЧЕСКИ 
                    //((MainWindow)w).Close();
                }
            }


            //new MainWindow().Show();


            //((MainWindow)Application.Current.MainWindow).ShowBooks();

            this.Close();
        }




        private void button4_Click(object sender, RoutedEventArgs e)
        {

            StreamReader sr = new StreamReader(Exchange.path + "/ReadingNow.txt", Encoding.GetEncoding(1251));
            string res = "";
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                string[] elements = s.Split('|');
                if (elements[0] == currentBook.Name)
                {
                    continue;
                }
                else
                {
                    res += s + '\n';
                }
            }
            sr.Close();

            StreamWriter sw1 = new StreamWriter(Exchange.path + "/" + "ReadingNow.txt", false, Encoding.GetEncoding(1251)); // delete from now
            sw1.Write(res);

            sw1.Close();
            MainWindow.nowBooks.Remove(currentBook);
            //w.ShowBooks();

            foreach (Window W in App.Current.Windows)
            {
                if (w is MainWindow)
                {
                    ((MainWindow)w).ShowBooks();         // НЕ ОБНОВЛЯЕТСЯ АВТОМАТИЧЕСКИ 
                    //((MainWindow)w).Close();
                }
            }

            
            //new MainWindow().Show();


            //((MainWindow)Application.Current.MainWindow).ShowBooks();

            this.Close();
        }



        private void button3_Click(object sender, RoutedEventArgs e)
        {
            currentBook.Start = DateTime.Now.Date;


            StreamWriter sw = new StreamWriter(Exchange.path + "/" + "ReadingNow.txt", true, Encoding.GetEncoding(1251));
            sw.Write(currentBook.Name + '|');
            sw.Write(currentBook.Author + '|');
            sw.Write(currentBook.Cover.ToString() + '|');
            sw.Write(currentBook.Pages.ToString() + '|');
            sw.Write(currentBook.YearOfPublication.ToString() + '|');
            sw.Write(currentBook.Genre + '|');
            sw.Write(currentBook.Description + '|');
            sw.Write(currentBook.Start.ToShortDateString());
            sw.WriteLine();
            sw.Close();
            MainWindow.nowBooks.Add(currentBook); // add to now

            StreamReader sr = new StreamReader(Exchange.path + "/WillRead.txt", Encoding.GetEncoding(1251));
            string res = "";
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                string[] elements = s.Split('|');
                if (elements[0] == currentBook.Name)
                {
                    continue;
                }
                else
                {
                    res += s;
                }
            }
            sr.Close();

            StreamWriter sw1 = new StreamWriter(Exchange.path + "/" + "WillRead.txt", false, Encoding.GetEncoding(1251)); // delete from future
            sw1.Write(res);
            
            sw1.Close();
            MainWindow.futureBooks.Remove(currentBook);

            this.Close();
        }


        private void button6_Click(object sender, RoutedEventArgs e)
        {

            StreamReader sr = new StreamReader(Exchange.path + "/WillRead.txt", Encoding.GetEncoding(1251));
            string res = "";
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                string[] elements = s.Split('|');
                if (elements[0] == currentBook.Name)
                {
                    continue;
                }
                else
                {
                    res += s + '\n';
                }
            }
            sr.Close();

            StreamWriter sw1 = new StreamWriter(Exchange.path + "/" + "WillRead.txt", false, Encoding.GetEncoding(1251)); // delete from future
            sw1.Write(res);

            sw1.Close();
            MainWindow.futureBooks.Remove(currentBook);
            //w.ShowBooks();

            foreach (Window W in App.Current.Windows)
            {
                if (w is MainWindow)
                {
                    ((MainWindow)w).ShowBooks();         // НЕ ОБНОВЛЯЕТСЯ АВТОМАТИЧЕСКИ 
                    //((MainWindow)w).Close();
                }
            }


            //new MainWindow().Show();


            //((MainWindow)Application.Current.MainWindow).ShowBooks();

            this.Close();
        }


        private void button7_Click(object sender, RoutedEventArgs e)
        {

            StreamReader sr = new StreamReader(Exchange.path + "/HasRead.txt", Encoding.GetEncoding(1251));
            string res = "";
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                string[] elements = s.Split('|');
                if (elements[0] == currentBook.Name)
                {
                    continue;
                }
                else
                {
                    res += s + '\n';
                }
            }
            sr.Close();

            StreamWriter sw1 = new StreamWriter(Exchange.path + "/" + "HasRead.txt", false, Encoding.GetEncoding(1251)); // delete from future
            sw1.Write(res);

            sw1.Close();
            MainWindow.pastBooks.Remove(currentBook);
            //w.ShowBooks();

            foreach (Window W in App.Current.Windows)
            {
                if (w is MainWindow)
                {
                    ((MainWindow)w).ShowBooks();         // НЕ ОБНОВЛЯЕТСЯ АВТОМАТИЧЕСКИ 
                    //((MainWindow)w).Close();
                }
            }


            //new MainWindow().Show();


            //((MainWindow)Application.Current.MainWindow).ShowBooks();

            this.Close();
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            currentBook.Start = DateTime.Now.Date;


            StreamWriter sw = new StreamWriter(Exchange.path + "/" + "ReadingNow.txt", true, Encoding.GetEncoding(1251));
            sw.Write(currentBook.Name + '|');
            sw.Write(currentBook.Author + '|');
            sw.Write(currentBook.Cover.ToString() + '|');
            sw.Write(currentBook.Pages.ToString() + '|');
            sw.Write(currentBook.YearOfPublication.ToString() + '|');
            sw.Write(currentBook.Genre + '|');
            sw.Write(currentBook.Description + '|');
            sw.Write(currentBook.Start.ToShortDateString());
            sw.WriteLine();
            sw.Close();
            MainWindow.nowBooks.Add(currentBook);
           
            this.Close();
        }




        private void button2_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter(Exchange.path + "/" + "WillRead.txt", true, Encoding.GetEncoding(1251));
            sw.Write(currentBook.Name + '|');
            sw.Write(currentBook.Author + '|');
            sw.Write(currentBook.Cover.ToString() + '|');
            sw.Write(currentBook.Pages.ToString() + '|');
            sw.Write(currentBook.YearOfPublication.ToString() + '|');
            sw.Write(currentBook.Genre + '|');
            sw.Write(currentBook.Description);
            sw.WriteLine();
            sw.Close();
            MainWindow.futureBooks.Add(currentBook);
            
            this.Close();
        }

        private void exit1_Click(object sender, RoutedEventArgs e)
        {
            Enter ent = new Enter();
            ent.Show();
            w.Close();
            this.Close();

        }

        private void exit2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        public string Question
        {
            set { question.Text = value; }
            get { return question.Text; }
        }

        public string Answer1
        {
            get { return ans1.Content.ToString(); }
            set { ans1.Content = new Button { Content = value }; }

        }

        public string Answer2
        {
            get { return ans2.Content.ToString(); }
            set { ans2.Content = new Button { Content = value }; }

        }

        public string Head
        {
            get { return Title; }
            set { Title = value; }
        }

    }
}
