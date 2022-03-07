using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Autoservice_Framework
{
    /// <summary>
    /// Логика взаимодействия для AddEditService.xaml
    /// </summary>
    public partial class AddEditService : Window
    {
        Service service;
        bool isEditing = false;

        public AddEditService()
        {
            InitializeComponent();
        }

        public AddEditService(int id):this()
        {
            isEditing = true;
            Title = "Изменение услуги";

            service = Data.Entities.Service.Where(i => i.Id == id).FirstOrDefault();

            titleBox.Text = service.Title;
            descriptionBox.Text = service.Description;
            priceBox.Text = service.Price.ToString();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
