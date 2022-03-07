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
            if (isEditing)
            {
                if (titleBox.Text.Length > 0 &&
                priceBox.Text.Length > 0)
                {
                    if (System.Windows.Forms.MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        service.Title = titleBox.Text;
                        service.Price = Convert.ToDecimal(priceBox.Text);
                        service.Description = descriptionBox.Text;



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
                if (titleBox.Text.Length > 0 &&
               priceBox.Text.Length > 0)
                {
                    if (System.Windows.Forms.MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {

                        var service = new Service()
                        {
                            Title = titleBox.Text,
                            Price = Convert.ToDecimal(priceBox.Text),
                            Description = descriptionBox.Text
                        };

                        Data.Entities.Service.Add(service);
                        Data.Entities.SaveChanges();
                        Close();
                        System.Windows.Forms.MessageBox.Show("New service added succesfully!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
