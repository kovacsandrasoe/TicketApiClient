using Microsoft.AspNetCore.SignalR.Client;
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
using Microsoft.Win32;
using System.Web;
using System.IO;
using TicketApiClient.Services;

namespace TicketApiClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string token;
        RestService<Ticket, string> rest;
        NotifyService notifyService;
        byte[] picturedata;
        string picturecontenttype;
        public MainWindow(string token)
        {
            InitializeComponent();
            this.token = token;

            notifyService = new NotifyService("https://localhost:44346/ticketHub");

            notifyService.AddHandler<Ticket>("NewTicket", (value) => lbox.Items.Add(value));
            notifyService.AddHandler<string>("Disconnected", (value) => MessageBox.Show(value, "Hiba történt", MessageBoxButton.OK, MessageBoxImage.Error));
            notifyService.AddHandler<string>("Connected", (value) => this.Title += " id: " + value);

            try
            {
                notifyService.Init();
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Hiba történt", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

            rest = new RestService<Ticket, string>(
                "https://localhost:44346/", "/api/ticket", token);

            Sync();
        }

        private async void Sync()
        {
            var items = await rest.Get();
            foreach (var item in items)
            {
                lbox.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rest.Post(new Ticket()
            {
                EventName = tb_eventname.Text,
                EventDate = dp_eventdate.SelectedDate ?? DateTime.Now,
                Price = int.Parse(tb_eventprice.Text),
                ContentType = picturecontenttype,
                PictureData = picturedata
            });

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG képek (*.jpg)|*.jpg";
            if (ofd.ShowDialog() == true)
            {
                string filename = ofd.FileName;
                img.Source = new BitmapImage(new Uri(filename));

                string mimeType = MimeMapping.GetMimeMapping(filename);

                (sender as Button).Visibility = Visibility.Collapsed;
                img.Visibility = Visibility.Visible;

                picturedata = File.ReadAllBytes(filename);
                picturecontenttype = mimeType;

            }

            
        }

    }
}
