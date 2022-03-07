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

namespace Autoservice_Framework.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookAServicePage.xaml
    /// </summary>
    public partial class BookAServicePage : Page
    {
        private readonly Employee _employee;

        public BookAServicePage(Employee employee)
        {
            InitializeComponent();

            LvClient.ItemsSource = Data.Entities.Client.ToList();
            LvService.ItemsSource = Data.Entities.Service.ToList();

            _employee = employee;
        }

        private void FilterClient()
        {
            Data.Entities = new VitalInstrumentalDBEntities();

            List<Client> clients = new List<Client>();

            clients = Data.Entities.Client
                .Where(i => i.LastName.Contains(filterLname.Text))
                .Where(i => i.FirstName.Contains(filterFname.Text))
                .Where(i => i.MiddleName.Contains(filterMname.Text))
                .Where(i => i.phone.Contains(filterPhone.Text))
                .ToList();

            LvClient.ItemsSource = clients;
        }

        private void FilterService()
        {
            Data.Entities = new VitalInstrumentalDBEntities();

            decimal min, max;
            if (filterMinPrice.Text != "") min = Convert.ToDecimal(filterMinPrice.Text);
            else min = 0;
            if (filterMaxPrice.Text != "") max = Convert.ToDecimal(filterMaxPrice.Text);
            else max = decimal.MaxValue;

            List<Service> services = new List<Service>();

            services = Data.Entities.Service
                .Where(i => i.Title.Contains(filterTitle.Text))
                .Where(i => i.Description.Contains(filterDesc.Text))
                .Where(i => i.Price > min && i.Price < max)
                .ToList();

            LvService.ItemsSource = services;
        }

        private void filterPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterClient();
        }

        private void filterMname_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterClient();
        }

        private void filterFname_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterClient();
        }

        private void filterLname_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterClient();
        }

        private void filterTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterService();
        }

        private void filterDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterService();
        }

        private void filterMinPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterService();
        }

        private void filterMaxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterService();
        }

        private void makeOrder_Click(object sender, RoutedEventArgs e)
        {
            var client = (Client)LvClient.SelectedItem;
            var service = (Service)LvService.SelectedItem;

            if (client == null)
            {
                MessageBox.Show("Выберите клиента!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (service == null)
            {
                MessageBox.Show("Выберите тип техобслуживания!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClientService clientService = new ClientService
            {
                IdClient = client.Id,
                IdService = service.Id,
                IdEmployee = _employee.Id,
                VisitDate = DateTime.Now
            };

            Data.Entities.ClientService.Add(clientService);

            Data.Entities.SaveChanges();
        }
    }
}
