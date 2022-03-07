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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            LvService.ItemsSource = Data.Entities.Service.ToList();
        }

        private void Filtration()
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

            LvService.ItemsSource= services;
        }

        private void filters_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtration();
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditService();
            window.ShowDialog();
            Filtration();
        }

        private void editProduct_Click(object sender, RoutedEventArgs e)
        {
            if (LvService.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("You haven't select any row", "Service editing", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return;
            }

            var service = (Service)LvService.SelectedItem;


            var window = new AddEditService(service.Id);
            window.ShowDialog();
            Filtration();
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (LvService.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("You haven't select any row", "Service deleting", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show($"Are you sure you want to delete {LvService.SelectedItems.Count} row(s)?",
                "Confirmation",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                return;

            int services = LvService.SelectedItems.Count;
            int valid = 0;

            foreach (Service service in LvService.SelectedItems)
            {
                if (Data.Entities.ClientService.Where(i => i.IdService == service.Id).Count() > 0)
                {
                    continue;
                }

                Data.Entities.Service.Remove(service);
                valid++;
            }

            Data.Entities.SaveChanges();

            string dop = "Услуги, на которые имеются актуальные записи, не могут быть удалены.";

            System.Windows.Forms.MessageBox.Show($"Успешно удалено - {valid} из {services}\n{(valid == services ? string.Empty : dop)}",
                "Удаление услуг",
                System.Windows.Forms.MessageBoxButtons.OK,
                !(valid == services) ? System.Windows.Forms.MessageBoxIcon.Hand : System.Windows.Forms.MessageBoxIcon.Information);
            Filtration();
        }

        private void clearFilters_Click(object sender, RoutedEventArgs e)
        {
            filterMaxPrice.Text = "";
            filterMinPrice.Text = "";
            filterDesc.Text = "";
            filterTitle.Text = "";
            Filtration();
        }
    }
}
