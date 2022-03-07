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

namespace Autoservice_Framework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Entities.User.Where(i=>i.login==loginBox.Text&&i.password==passwordBox.Password).Count()==1)
            {
                Hide();
                new AppWindow(Data.Entities.User
                    .Where(i => i.login == loginBox.Text && i.password == passwordBox.Password)
                    .FirstOrDefault().Id)
                    .ShowDialog();
                Show();
            }
            else
            {

            }
        }
    }
}
