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

namespace WpfApp8
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

                Sotrud sotr = Entities.GetContext().Sotrud.Where(b => b.Login == log.Text && b.Pass == par.Password).FirstOrDefault();

                if (sotr != null)
                {
                    if ((bool)sotr.Status == false)
                    {
                        MessageBox.Show("False");
                        return;
                    }
                    if (sotr.Rol == 1)
                    {
                        new Adminka(sotr.id).Show();
                        this.Hide();
                        

                    }
                    if (sotr.Rol == 2)
                    {
                        int i = sotr.id;
                        new Menedger(i).Show();
                        this.Hide();
                    }
                    if (sotr.Rol == 3)
                    {
                        int i = sotr.id;
                        Tehnic c = new Tehnic();
                        c.i = i;
                        c.Show();
                        this.Hide();

                    }
                    
                    MessageBox.Show("Hellow!", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
