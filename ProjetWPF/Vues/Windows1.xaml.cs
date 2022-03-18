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
using LiveCharts;
using LiveCharts.Wpf;
using GstBDD;
using ProjetWPF.Vues;

namespace ProjetWPF.Vues
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        GstBdd gstbdd = new GstBdd();
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Graphique affichant le nombre d'employés travaillant dans cette région

            PieSeries ps;
            ChartValues<int> line;

            Dictionary<string, int> lesDatas = new Dictionary<string, int>();

            lesDatas = gstbdd.GetDatasGraph1();

            foreach (string cle in lesDatas.Keys)
            {
                ps = new PieSeries();
                line = new ChartValues<int>();
                line.Add(lesDatas[cle]);
                ps.Values = line;
                ps.Title = cle;
                ps.DataLabels = true;
                monGraph2.Series.Add(ps);
            }
            monGraph2.LegendLocation = LegendLocation.Bottom;

        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
