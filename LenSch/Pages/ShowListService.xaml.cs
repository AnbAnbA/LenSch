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

namespace LenSch.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowListService.xaml
    /// </summary>
    public partial class ShowListService : Page
    {
        private int code;
        public ShowListService(int code)
        {
            InitializeComponent();
            this.code = code;
            LSH.ItemsSource = Base.ELS.Service.ToList();
            
        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnupd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (code == 0000) 
            {
                
            }
        }
    }
}
