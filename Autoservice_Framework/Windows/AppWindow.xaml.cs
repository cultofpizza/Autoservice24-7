using Autoservice_Framework.Pages;
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
using System.Windows.Shapes;

namespace Autoservice_Framework
{
    /// <summary>
    /// Логика взаимодействия для AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        Employee employee;

        VitalInstrumentalDBEntities entities = new VitalInstrumentalDBEntities();
        public AppWindow(int userId)
        {
            InitializeComponent();
            employee = Data.Entities.User.Where(i => i.Id == userId).FirstOrDefault().Employee.FirstOrDefault();
            userNameBlock.Text = $"{employee.LastName} {employee.FirstName}";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void servicesPageButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = null;
            mainFrame.Navigate(new ServicePage());
        }
        private void clientsPageButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = null;
            mainFrame.Navigate(new ClientPage());
        }
        private void orderPageButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = null;
            mainFrame.Navigate(new BookAServicePage(employee));
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Closed -= Window_Closed;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
