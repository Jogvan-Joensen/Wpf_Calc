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

namespace WpfCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //CalcToSQLDataContext db = new CalcToSQLDataContext(Properties.Settings.Default.CalcInfoConnectionString);
        decimal? tal1 = null;
        decimal? tal2 = null;
        string method = "";

        private void buttonAll_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            decimal input = decimal.Parse(b.Content.ToString());

            if (tal1.Equals(null))
            {
                tal1 = input;
            }
            else
            {
                tal2 = input;
            }
            displayTextbox.Text = input + " ";
        }

        private void buttonCalc_Click(object sender, RoutedEventArgs e)
        {
            using (CalcToSQLDataContext db = new CalcToSQLDataContext())
            {
                /* var all = from d in db.CalcInfos
                           select d.tal1;*/
                CalcInfo c = new CalcInfo();
                c.tal1 = tal1;
                c.tal2 = tal2;
                c.metode = method;
                c.resultat = decimal.Parse(controlMethod());
                db.CalcInfos.InsertOnSubmit(c);
                db.SubmitChanges();
            }
            displayTextbox.Text = controlMethod();
            tal1 = null;
            tal2 = null;
            
        }

        private string controlMethod()
        {
            if (method.Equals("+"))
            {
                return addMethod().ToString();
            }
            if (method.Equals("-"))
            {
                return subtractMethod().ToString();
            }
            if (method.Equals("*"))
            {
                return gangeMethod().ToString();
            }
            if (method.Equals("/"))
            {
                return divMethod().ToString();
            }
            return "";
        }

        private void buttonToDo_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            method = b.Content.ToString();
            displayTextbox.Text = method;
        }

        private decimal? addMethod()
        {
            return tal1 + tal2;
        }
        private decimal? subtractMethod()
        {
            return tal1 - tal2;
        }
        private decimal? gangeMethod()
        {
            return tal1 * tal2;
        }
        private decimal? divMethod()
        {
            return tal1/tal2;
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            displayTextbox.Clear();
            tal1 = null;
            tal2 = null;
            method = "";
        }
    }
}
