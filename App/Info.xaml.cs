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
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info(Book b)
        {
            InitializeComponent();
            name.Text = b.Name;
            author.Text = b.Author;
            pages.Text = b.Pages.ToString();
            year.Text = b.YearOfPublication.ToString();
            genre.Text = b.Genre;
            description.Text = b.Description;
            image.Source = b.Cover;
        }
    }
}
