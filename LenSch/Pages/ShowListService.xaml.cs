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
        private string code;
        public ShowListService(string code)
        {
            InitializeComponent();
            this.code = code;
            LSH.ItemsSource = Base.ELS.Service.ToList();
            if (code == "0000")
            {
                btnAdd.Visibility = Visibility.Visible;
                btnAddSC.Visibility = Visibility.Visible;
                btnShowSC.Visibility = Visibility.Visible;
            }
        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnupd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btdel_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (code == "0000")
            {
                btn.Visibility= Visibility.Visible;
            }
        }

        private void btnupd_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (code == "0000")
            {
                btn.Visibility = Visibility.Visible;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddSC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowSC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock t = (TextBlock)sender;
            int ind=Convert.ToInt32(t.Text);
            List<Service> s = Base.ELS.Service.Where(x => x.ID == ind).ToList();
            //if(s.Discount)
        }
    }
}
