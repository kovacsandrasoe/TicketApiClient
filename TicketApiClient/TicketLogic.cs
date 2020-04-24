using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketApiClient.Services;

namespace TicketApiClient
{
    class TicketLogic : ObservableObject
    {
        RestService<Ticket, string> rest;
        NotifyService notifyService;

        public List<Ticket> Tickets { get; set; }

        public TicketLogic(string token)
        {
            notifyService = new NotifyService("https://localhost:44346/ticketHub");

            notifyService.AddHandler<Ticket>("NewTicket", (value) => GetTicket(value));
            //notifyService.AddHandler<string>("Disconnected", (value) => MessageBox.Show(value, "Hiba történt", MessageBoxButton.OK, MessageBoxImage.Error));
            //notifyService.AddHandler<string>("Connected", (value) => this.Title += " id: " + value);

            notifyService.Init();

            rest = new RestService<Ticket, string>(
                "https://localhost:44346/", "/api/ticket", token);

            Init();
        }

        private async void Init()
        {
            var items = await rest.Get();
            foreach (var item in items)
            {
                Tickets.Add(item);
            }
        }
        public void AddTicket(Ticket t)
        {
            rest.Post(t);
        }

        private void GetTicket(Ticket t)
        {
            Tickets.Add(t);
            RaisePropertyChanged(nameof(Tickets));
        }
    }
}
