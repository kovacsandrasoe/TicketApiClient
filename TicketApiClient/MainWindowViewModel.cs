using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TicketApiClient
{
    class MainWindowViewModel : ViewModelBase
    {
        TicketLogic logic;

        public MainWindowViewModel(string token)
        {
            logic = new TicketLogic(token);
        }
        public Ticket TicketToEdit { get; set; }

        public Ticket SelectedTicket { get; set; }

        public IList<Ticket> Tickets
        {
            get
            {
                return logic.Tickets;
            }
        }

        public ICommand PublishTicketCommand { get; set; }

        public ICommand OpenTicketCommand { get; set; }

        public ICommand DeleteTicketCommand { get; set; }

        public MainWindowViewModel()
        {
            PublishTicketCommand = new RelayCommand(() => Tickets.Add(TicketToEdit), () => TicketToEdit.EventName.Length > 3);
            OpenTicketCommand = new RelayCommand(() => { }, () => SelectedTicket != null); 
        }
    }
}
