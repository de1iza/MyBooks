﻿using System;
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

    public partial class BookControlPast : UserControl
    {

        public Book currentBook;

        public BookControlPast(Book b)
        {
            InitializeComponent();
            BookName = b.Name;
            Author = b.Author;
            Cover = b.Cover;
            Start = b.Start.ToShortDateString();
            Finish = b.Finish.ToShortDateString();
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

        public string Start
        {
            set { startdate.Text = "Начало чтения: " + value; }
            get { return startdate.Text; }
        }

        public string Finish
        {
            set { finishdate.Text = "Конец чтения: " + value; }
            get { return finishdate.Text; }
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
            new MyMessageBox(currentBook, "Удаление книги", "Вы уверены, что хотите удалить эту книгу из прочитанных?", "     Да     ", "     Нет     ", 4).ShowDialog();
        }
    }
}
