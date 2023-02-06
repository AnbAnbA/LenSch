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
    /// Логика взаимодействия для AddZap.xaml
    /// </summary>
    public partial class AddZap : Page
    {
        private string code;
        Service serv;
        ClientService cl;
        public AddZap(Service serv,string code)
        {
            InitializeComponent();
            this.code = code;
            this.serv = serv;
            Title.Text = "Название услуги: " + serv.Title + " | " + "Длительность услуги: " + serv.DurationInSeconds/60 + " минут";
            List<Client> clients = Base.ELS.Client.ToList();
            for (int i = 0; i < clients.Count; i++) 
            {
                fio.Items.Add(clients[i].FirstName+ clients[i].LastName+ clients[i].Patronymic);
            }

            hour.Text = DateTime.Now.ToString("HH");
            min.Text = DateTime.Now.ToString("mm");
            int HH = Convert.ToInt32(DateTime.Now.ToString("HH"));
            int MM = Convert.ToInt32(DateTime.Now.ToString("mm"));
            DateTime date = new DateTime(2000, 2, 2, HH, MM, 0);
            DateTime data = date.AddMinutes(Convert.ToInt32(serv.DurationInSeconds/60));
            end.Text = data.ToShortTimeString();

        }

        void timer()
        {
            try
            {
                int h = Convert.ToInt32(hour.Text);
                int m = Convert.ToInt32(min.Text);
                if ((h < 24) && (m < 60))
                {

                    int HH = Convert.ToInt32(h);
                    int MM = Convert.ToInt32(m);
                    DateTime date = new DateTime(2000, 2, 2, HH, MM, 0);
                    DateTime data = date.AddMinutes(Convert.ToInt32(serv.DurationInSeconds/60));
                    end.Text = data.ToShortTimeString();
                }
                else
                {
                    MessageBox.Show("Введите время правильно", "Ошибка", MessageBoxButton.OK);

                }
            }
            catch
            {

                //MessageBox.Show("Что-то пошло не так!", "Ошибка", MessageBoxButton.OK);
            }
        }
        private void min_TextChanged(object sender, TextChangedEventArgs e)
        {
            timer();
        }

        private void hour_TextChanged(object sender, TextChangedEventArgs e)
        {
            timer();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (fio.Text == "" || hour.Text == "" || min.Text == "" || dat.Text == "")
            {
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK);
            }
            else
            {
                cl = new ClientService();
                cl.ServiceID = serv.ID;
                cl.ClientID = fio.SelectedIndex + 1;
                string date = dat.Text;
                string[] Dat = date.Split('.');
                int h = Convert.ToInt32(hour.Text);
                int m = Convert.ToInt32(min.Text);
                DateTime dateStar = new DateTime(Convert.ToInt32(Dat[2]), Convert.ToInt32(Dat[1]), Convert.ToInt32(Dat[0]), h, m, 0);
                cl.StartTime = dateStar;
                Base.ELS.ClientService.Add(cl);

                Base.ELS.SaveChanges();
                MessageBox.Show("Клиент записан");

                FrameC.frameM.Navigate(new ShowListService(code));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new ShowListService(code));
        }
    }
}
