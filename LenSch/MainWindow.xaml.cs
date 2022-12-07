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

namespace LenSch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       private int code;
        public MainWindow()
        {
            InitializeComponent();
            Base.ELS = new Entities();
            FrameC.frameM = frameM;
            FrameC.frameM.Navigate(new Pages.ShowListService(code));
        }

        private void BTKod_Click(object sender, RoutedEventArgs e)
        {
            code = Convert.ToInt32(tbcode.Text);


        }
    }
}
