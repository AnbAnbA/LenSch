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
            ch.Text = Base.ELS.Service.ToList().Count + "/" + Base.ELS.Service.ToList().Count;
            if (code == "0000")
            {
                btnAdd.Visibility = Visibility.Visible;
                btnShowSC.Visibility = Visibility.Visible;
            }
        }

        private void btdel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            Service serv = Base.ELS.Service.FirstOrDefault(x => x.ID == id);
            List<ClientService> clientservices = Base.ELS.ClientService.Where(x => x.ServiceID == serv.ID).ToList();
            if (clientservices.Count > 0)
            {
                MessageBox.Show("Удаление невозможно, cуществует запись на данную услугу");
            }
            else
            {
                Base.ELS.Service.Remove(serv);
                Base.ELS.SaveChanges();
                FrameC.frameM.Navigate(new ShowListService(code));
            }
        }

        private void btnupd_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            Service serv = Base.ELS.Service.FirstOrDefault(x => x.ID == id);
            FrameC.frameM.Navigate(new AddUp(serv, code));
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
            FrameC.frameM.Navigate(new AddUp(code));
        }

     

        private void btnShowSC_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new BlZap(code));
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            //TextBlock t = (TextBlock)sender;
            //int ind=Convert.ToInt32(t.Text);
            //List<Service> s = Base.ELS.Service.Where(x => x.ID == ind).ToList();
            //if(s.Discount)
        }

        void filter()
        {
            List<Service> services = new List<Service>();
            services = Base.ELS.Service.ToList();
            //поиск название

            if (!string.IsNullOrWhiteSpace(SerNam.Text))  
            {
                services = services.Where(x => x.Title.ToLower().Contains(SerNam.Text.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(SerOpis.Text))  // если строка не пустая и если она не состоит из пробелов
            {

                List<Service> trl = services.Where(x => x.Description != null).ToList();
                if (trl.Count > 0)
                {

                    services = trl.Where(x => x.Description.ToLower().Contains(SerOpis.Text.ToLower())).ToList();

                }
                else
                {
                    MessageBox.Show("нет описания");
                    SerOpis.Text = "";
                }

            }
            //Фильтрация

            switch (Filt.SelectedIndex)
            {
                case 0:
                    services = services.ToList();
                    break;
                case 1:
                    services = services.Where(x => x.Discount >= 0 && x.Discount < 0.05).ToList();
                    break;
                case 2:
                    services = services.Where(x => x.Discount >= 0.05 && x.Discount < 0.15).ToList();
                    break;
                case 3:
                    services = services.Where(x => x.Discount >= 0.15 && x.Discount < 0.3).ToList();
                    break;
                case 4:
                    services = services.Where(x => x.Discount >= 0.3 && x.Discount < 0.7).ToList();
                    break;
                case 5:
                    services = services.Where(x => x.Discount >= 0.7 && x.Discount < 1).ToList();
                    break;
            }

            //сортировка

            switch (Sort.SelectedIndex)
            {
                case 0:
                    {
                        services.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    }
                    break;
                case 1:
                    {
                        services.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                        services.Reverse();
                    }
                    break;
            }

            LSH.ItemsSource = services;
            if (services.Count == 0)
            {
                MessageBox.Show("нет записей");
                ch.Text = Base.ELS.Service.ToList().Count + "/" + Base.ELS.Service.ToList().Count;
                SerNam.Text = "";
                SerOpis.Text = "";
                Sort.SelectedIndex = 0;
                Filt.SelectedIndex = 0;

            }
            ch.Text = services.Count + "/" + Base.ELS.Service.ToList().Count;

        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }

        private void Filt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }

        private void SerNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }

        private void SerOpis_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }

        private void btnaddusl_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            Service serv = Base.ELS.Service.FirstOrDefault(x => x.ID == id);
            FrameC.frameM.Navigate(new AddZap(serv, code));
        }

        private void btnaddusl_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (code == "0000")
            {
                btn.Visibility = Visibility.Visible;
            }
        }
    }
}
