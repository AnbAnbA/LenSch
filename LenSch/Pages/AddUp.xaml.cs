using Microsoft.Win32;
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
    /// Логика взаимодействия для AddUp.xaml
    /// </summary>
    public partial class AddUp : Page
    {
        int n = 0;
        string path;
        bool flagUpdate;
        bool flagUpdatePhoto;
        private string code;
        Service serv;
        ServicePhoto servicephoto;
        public AddUp(string code)
        {
            InitializeComponent();
            this.code = code;
            addPhotos.Visibility = Visibility.Collapsed;
        }

        public AddUp(Service serv, string code)
        {
            InitializeComponent();
            this.serv = serv;
            this.code = code;
            flagUpdate = true;
            usl.Text = serv.Title;
            cost.Text = Convert.ToString(serv.Cost);
            time.Text = Convert.ToString(serv.DurationInSeconds/60);
            sale.Text = Convert.ToString(serv.Discount*100);
            opis.Text = serv.Description;

            if (serv.MainImagePath != null)
            {
                BitmapImage img = new BitmapImage(new Uri(serv.MainImagePath, UriKind.RelativeOrAbsolute));
                Img.Source = img;
            }
            UpdatePhoto.Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new ShowListService(code));
        }

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> u = Base.ELS.ServicePhoto.Where(x => x.ServiceID == serv.ID).ToList();

            n--;
            if (Nextbtn.IsEnabled == false)
            {
                Nextbtn.IsEnabled = true;
            }
            if (u != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {

                BitmapImage img = new BitmapImage(new Uri(u[n].PhotoPath, UriKind.RelativeOrAbsolute));
                Img.Source = img;
            }
            if (n == 0)
            {
                Backbtn.IsEnabled = false;
            }
        }

        private void Nextbtn_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> servicePhoto = Base.ELS.ServicePhoto.Where(x => x.ServiceID == serv.ID).ToList();

            n++;
            if (Backbtn.IsEnabled == false)
            {
                Backbtn.IsEnabled = true;
            }
            if (servicePhoto != null)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {

                BitmapImage img = new BitmapImage(new Uri(servicePhoto[n].PhotoPath, UriKind.RelativeOrAbsolute));
                Img.Source = img;
            }
            if (n == servicePhoto.Count - 1)
            {
                Nextbtn.IsEnabled = false;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (flagUpdate == false)
            {
                if (usl.Text == "" || cost.Text == "" || time.Text == "" || sale.Text == "" || path == null)
                {
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK);
                }
                else
                {
                   
                                    serv = new Service();
                                    serv.Title = usl.Text;
                                    serv.Cost = Convert.ToInt32(cost.Text);
                                    double skidk = Convert.ToDouble(sale.Text) / 100;
                                    serv.Discount = skidk;
                                    int timeSecond = Convert.ToInt32(time.Text) * 60;
                                    serv.DurationInSeconds = timeSecond;
                                    serv.MainImagePath = path;
                                    if (opis.Text == "")
                                    {
                                        serv.Description = null;
                                    }
                                    else
                                    {
                                        serv.Description = opis.Text;
                                    }

                    Base.ELS.Service.Add(serv);
                                    servicephoto = new ServicePhoto();
                                    servicephoto.ServiceID = serv.ID;
                                    servicephoto.PhotoPath = path;
                    Base.ELS.ServicePhoto.Add(servicephoto);

                    Base.ELS.SaveChanges();
                                    MessageBox.Show("Информация добавлена");

                    FrameC.frameM.Navigate(new ShowListService(code));

                }
            }
            else
            {
                if (usl.Text == "" || cost.Text == "" || time.Text == "" || sale.Text == "")
                {
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка", MessageBoxButton.OK);
                }
                else
                {
                   
                                serv.Title = usl.Text;
                                serv.Cost = Convert.ToInt32(cost.Text);
                                double skidk = Convert.ToDouble(sale.Text) / 100;
                                serv.Discount = skidk;
                                int timeSecond = Convert.ToInt32(time.Text) * 60;
                                serv.DurationInSeconds = timeSecond;
                                if (path == null)
                                {
                                    path = serv.MainImagePath;
                                }
                                if ((path != null) && (flagUpdatePhoto == false))
                                {
                                    servicephoto = new ServicePhoto();
                                    servicephoto.ServiceID = serv.ID;
                                    servicephoto.PhotoPath = path;
                        Base.ELS.ServicePhoto.Add(servicephoto);

                                }
                                serv.MainImagePath = path;
                                if (opis.Text == "")
                                {
                                    serv.Description = null;
                                }
                                else
                                {
                                    serv.Description = opis.Text;
                                }
                    Base.ELS.SaveChanges();
                                MessageBox.Show("Информация добавлена");

                    FrameC.frameM.Navigate(new ShowListService(code));
                            
                }
            }

        }

        private void safePhoto_Click(object sender, RoutedEventArgs e)
        {
            flagUpdatePhoto = true;
            List<ServicePhoto> u = Base.ELS.ServicePhoto.Where(x => x.ServiceID == serv.ID).ToList();
            serv.MainImagePath = u[n].PhotoPath;
            Base.ELS.SaveChanges();
            MessageBox.Show("Фотография изменена");
            Nextbtn.Visibility = Visibility.Collapsed;
            Backbtn.Visibility = Visibility.Collapsed;
            safePhoto.Visibility = Visibility.Collapsed;
            addPhoto.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Collapsed;
            UpdatePhoto.Visibility = Visibility.Visible;
            addPhotos.Visibility = Visibility.Visible;
            DeletPhoto.Visibility = Visibility.Collapsed;
        }

        private void addPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
            OFD.InitialDirectory = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.Length - 9) + "images\\";
            OFD.ShowDialog();
            path = OFD.FileName;
            string[] arrayPath = path.Split('\\');


            if (arrayPath.Length != 1)
            {
                path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1];
                Img.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            }

                //BitmapImage image = new BitmapImage(new Uri(path, UriKind.Relative));
                //Img.Source = image;
            }
            catch
            {
                MessageBox.Show("Что то пошло не так", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void addPhotos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog(); 
                OFD.Multiselect = true;  
                if (OFD.ShowDialog() == true) 
                {
                    foreach (string file in OFD.FileNames)  
                    {
                        ServicePhoto u = new ServicePhoto();
                        u.ServiceID = serv.ID;
                        path = file;  
                        string[] arrayPath = path.Split('\\');  
                        path = "\\" + arrayPath[arrayPath.Length - 2] + "\\" + arrayPath[arrayPath.Length - 1]; 
                        u.PhotoPath = path;
                        Base.ELS.ServicePhoto.Add(u);

                    }
                    Base.ELS.SaveChanges();
                    MessageBox.Show("Фото добавлены");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void UpdatePhoto_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> servicePhoto = Base.ELS.ServicePhoto.Where(x => x.ServiceID == serv.ID).ToList();
            if (servicePhoto.Count > 1)
            {

                BitmapImage img = new BitmapImage(new Uri(servicePhoto[n].PhotoPath, UriKind.RelativeOrAbsolute));
                Img.Source = img;

                Nextbtn.Visibility = Visibility.Visible;
                Backbtn.Visibility = Visibility.Visible;
                safePhoto.Visibility = Visibility.Visible;
                addPhoto.Visibility = Visibility.Collapsed;
                UpdatePhoto.Visibility = Visibility.Collapsed;
                back.Visibility = Visibility.Visible;
                addPhotos.Visibility = Visibility.Collapsed;
                DeletPhoto.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Нет дополнительных фотографий", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeletPhoto_Click(object sender, RoutedEventArgs e)
        {
            List<ServicePhoto> photos = Base.ELS.ServicePhoto.Where(x => x.ServiceID == serv.ID).ToList();
            if (photos[n].PhotoPath != serv.MainImagePath)
            {
                ServicePhoto photo = photos.FirstOrDefault(x => x.PhotoPath == photos[n].PhotoPath);
                Base.ELS.ServicePhoto.Remove(photo);
                Base.ELS.SaveChanges();
                FrameC.frameM.Navigate(new AddUp(serv, code));
            }
            else
            {
                MessageBox.Show("Данную фотографию нельзя удалить, так как она является обязательной", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            flagUpdatePhoto = false;
            Nextbtn.Visibility = Visibility.Collapsed;
            Backbtn.Visibility = Visibility.Collapsed;
            safePhoto.Visibility = Visibility.Collapsed;
            addPhoto.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Collapsed;
            UpdatePhoto.Visibility = Visibility.Visible;
            addPhotos.Visibility = Visibility.Visible;
            DeletPhoto.Visibility = Visibility.Collapsed;
            if (serv.MainImagePath != null)
            {
                BitmapImage img = new BitmapImage(new Uri(serv.MainImagePath, UriKind.RelativeOrAbsolute));
                Img.Source = img;
            }
        }
    }
}
