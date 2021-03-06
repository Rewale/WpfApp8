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
using System.Data.SqlClient;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для Menedger.xaml
    /// </summary>
    public partial class Menedger : System.Windows.Window
    {
        public Menedger(int i)
        {
            InitializeComponent();

            bindcombo();
        }
        public int i { get; set; }
        public List<Status> them { get; set; }
        private void bindcombo()
        {
            Entities dc = new Entities();
            var item = dc.Status.ToList();
            them = item;
            DataContext = them;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string a = @"Data Source=DESKTOP-VITU4VL\SQLEXPRESS;Initial Catalog=Prac;Integrated Security=True";
                SqlConnection con = new SqlConnection(a);
                con.Open();
                string command = ("INSERT INTO Client([Fio],[Pasport],[Bith]) VALUES('" + fio.Text + "','" + pass.Text + "','" + date.Text + "')");
                SqlDataAdapter n = new SqlDataAdapter(command, con);
                DataSet ds = new DataSet();
                n.Fill(ds);
                MessageBox.Show("Клиент был добавлен");
            }
            catch (Exception)
            {
                MessageBox.Show("Клиент уже существует");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            Zada.ItemsSource = Entities.GetContext().Zadachi.ToList().Where(p => p.Status == 3 || p.Status == 2 || p.Status == 1);
            
        }

        private void Stat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = Stat.SelectedIndex + 1;
            Zada.ItemsSource = Entities.GetContext().Zadachi.ToList().Where(p => p.Status == i);
            Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            try
            {
                for (int j = 0; j < Zada.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    myRange.Value2 = Zada.Columns[j].Header;
                }
                for (int i = 0; i < Zada.Columns.Count; i++)
                {
                    for (int j = 0; j < Zada.Items.Count; j++)
                    {
                        TextBlock b = Zada.Columns[i].GetCellContent(Zada.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var ia = Entities.GetContext().Adress.Where(b => b.Numberofber == num.Text).FirstOrDefault();
            if (ia!=null)
            {
                MessageBox.Show("Номер договора верен, владелец:" + ia.Client.Fio);
            }
            else
            {
                MessageBox.Show("Номер договора не верен");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var ia = Entities.GetContext().Adress.Where(b => b.Numberofber == num.Text).FirstOrDefault();
            string a = @"Data Source=DESKTOP-VITU4VL\SQLEXPRESS;Initial Catalog=Prac;Integrated Security=True";
            SqlConnection con = new SqlConnection(a);
            con.Open();
            string command = ("INSERT INTO Appeals([Discribe],[idAdress]) VALUES('" + discribe.Text + "','" +ia.idClient+ "')");
            SqlDataAdapter n = new SqlDataAdapter(command, con);
            DataSet ds = new DataSet();
            n.Fill(ds);
            MessageBox.Show("Обращение добавлено");
            discribe.Clear();
        }
    }
}
