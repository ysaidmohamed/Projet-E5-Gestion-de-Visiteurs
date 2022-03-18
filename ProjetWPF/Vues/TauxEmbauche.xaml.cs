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
using GstBDD;
using LiveCharts;
using LiveCharts.Wpf;
using ProjetWPF.Vues;

namespace ProjetWPF.Vues
{
    /// <summary>
    /// Logique d'interaction pour TauxEmbauche.xaml
    /// </summary>
    public partial class TauxEmbauche : Window
    {
        GstBdd gstbdd = new GstBdd();
        public TauxEmbauche()
        {
            InitializeComponent();
        }

        // Génère le diagramme du taux d'embauche par mois
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ColumnSeries cs = new ColumnSeries();
            cs.Fill = Brushes.SteelBlue;
            ChartValues<double> line = new ChartValues<double>();

            Dictionary<string, double> lesDatas = new Dictionary<string, double>();

            lesDatas = gstbdd.GetDatasGraph2();

            foreach (string cle in lesDatas.Keys)
            {
                line.Add(lesDatas[cle]);
            }
            cs.Values = line;

            monGraph1.Series.Clear();
            monGraph1.AxisX.Clear();
            Axis axe = new Axis();
            axe.Labels = lesDatas.Keys.ToList();
            monGraph1.AxisX.Add(axe);
            monGraph1.Series.Add(cs);
            cs.Title = "Nombre d'embauches :";
            cs.DataLabels = true;

            monGraph1.LegendLocation = LegendLocation.Top;
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
