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

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для Tehnic.xaml
    /// </summary>
    public partial class Tehnic : Window
    {
        public Tehnic()
        {
            InitializeComponent();
        }
        public int i;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            Luck.ItemsSource = Entities.GetContext().Zadachi.ToList().Where(p => p.Status == 1);
            Looy.ItemsSource = Entities.GetContext().Zadachi.ToList().Where(p => p.Status == 2 && p.idIsp == i);
            
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
           
            string z = ((sender as Button).DataContext as Zadachi).Discribe;
            int i = ((sender as Button).DataContext as Zadachi).id;
            var smena = Entities.GetContext().Zadachi.FirstOrDefault(h => h.id == i);
            smena.Status = 2;
            smena.idIsp = i;
            Entities.GetContext().SaveChanges();
            MessageBox.Show("Просьба начать выполнять задачу MATASK - " + i);
         
           
            

        }

        private void Star_Click_1(object sender, RoutedEventArgs e)
        {
            string z = ((sender as Button).DataContext as Zadachi).Discribe;
            int i = ((sender as Button).DataContext as Zadachi).id;
            
        }
    }
}
