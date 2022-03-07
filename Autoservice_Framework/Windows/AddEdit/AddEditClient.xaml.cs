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
    /// Interaction logic for AddEditClient.xaml
    /// </summary>
    public partial class AddEditClient : Window
    {
        Client client;
        bool isEditing = false;
        public AddEditClient()
        {
            InitializeComponent();
        }


        public AddEditClient(int id) : this()
        {
            isEditing = true;
            Title = "Изменение данных Клиента";

            client = Data.Entities.Client.Where(i => i.Id == id).FirstOrDefault();

            lastNameBox.Text = client.LastName;
            firstNameBox.Text = client.FirstName;
            middleNameBox.Text = client.MiddleName;
            phoneBox.Text = client.phone;
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                if (lastNameBox.Text.Length > 0 &&
                firstNameBox.Text.Length > 0 &&
                phoneBox.Text.Length > 0)
                {
                    if (System.Windows.Forms.MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        client.LastName = lastNameBox.Text;
                        client.FirstName = firstNameBox.Text;
                        client.MiddleName = middleNameBox.Text;
                        client.phone = phoneBox.Text;

                        Data.Entities.SaveChanges();
                        Close();
                        System.Windows.Forms.MessageBox.Show("Changes applied succesfully!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Проверьте правильность введённых данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                if (lastNameBox.Text.Length > 0 &&
                firstNameBox.Text.Length > 0 &&
                phoneBox.Text.Length > 0)
                {
                    if (System.Windows.Forms.MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {

                        var client = new Client()
                        {
                            LastName = lastNameBox.Text,
                            FirstName = firstNameBox.Text,
                            MiddleName = middleNameBox.Text,
                            phone = phoneBox.Text
                        };

                        Data.Entities.Client.Add(client);
                        Data.Entities.SaveChanges();
                        Close();
                        System.Windows.Forms.MessageBox.Show("New client added succesfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Проверьте правильность введённых данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
    }
}
