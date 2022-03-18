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
using ClassesMetier;
using LiveCharts;
using LiveCharts.Wpf;
using ProjetWPF.Vues;

namespace ProjetWPF.Vues
{
    /// <summary>
    /// Logique d'interaction pour Statistiques.xaml
    /// </summary>
    public partial class Statistiques : Window
    {
        GstBdd gstbdd = new GstBdd();
        public Statistiques()
        {
            InitializeComponent();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Rempli les tableaux de statistiques au chargement de la page
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lstRegionsParSecteur.ItemsSource = gstbdd.getAllRegionsDansSecteurs();
            lstVisiteursSansSecteurs.ItemsSource = gstbdd.getVisiteursSansSecteurs();
        }

        private void regionsActives_Click(object sender, RoutedEventArgs e)
        {
            Window1 pactiveregions = new Window1();
            pactiveregions.ShowDialog();
        }

        private void tauxEmbauche_Click(object sender, RoutedEventArgs e)
        {
            TauxEmbauche ptauxembauche = new TauxEmbauche();
            ptauxembauche.ShowDialog();
        }
    }
}
