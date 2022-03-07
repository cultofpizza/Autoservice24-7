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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();

            LvClient.ItemsSource = Data.Entities.Client.ToList();
        }
        private void Filter()
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

        private void filterLname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void filterFname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void filterMname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void filterPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void addClient_Click(object sender, RoutedEventArgs e)
        {
            var client = (Client)LvClient.SelectedItem;


            var window = new AddEditClient();
            window.ShowDialog();
            Filter();
        }

        private void editClient_Click(object sender, RoutedEventArgs e)
        {
            if (LvClient.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("You haven't select any row", "Service editing", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return;
            }

            var client = (Client)LvClient.SelectedItem;


            var window = new AddEditClient();
            window.ShowDialog();
            Filter();
        }

        private void deleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (LvClient.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("You haven't select any row", "Service deleting", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show($"Are you sure you want to delete {LvClient.SelectedItems.Count} row(s)?",
                "Confirmation",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                return;

            int services = LvClient.SelectedItems.Count;
            int valid = 0;

            foreach (Client client in LvClient.SelectedItems)
            {
                if (Data.Entities.ClientService.Where(i => i.IdService == client.Id).Count() > 0)
                {
                    continue;
                }

                Data.Entities.Client.Remove(client);
                valid++;
            }

            Data.Entities.SaveChanges();

            string dop = "Услуги, на которые имеются актуальные записи, не могут быть удалены.";

            System.Windows.Forms.MessageBox.Show($"Успешно удалено - {valid} из {services}\n{(valid == services ? string.Empty : dop)}",
                "Удаление услуг",
                System.Windows.Forms.MessageBoxButtons.OK,
                !(valid == services) ? System.Windows.Forms.MessageBoxIcon.Hand : System.Windows.Forms.MessageBoxIcon.Information);
            Filter();
        }

        private void clearFilters_Click(object sender, RoutedEventArgs e)
        {
            filterLname.Text = "";
            filterFname.Text = "";
            filterMname.Text = "";
            filterPhone.Text = "";

            Filter();
        }
    }
}
