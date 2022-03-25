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

namespace ProjetWPF.Vues
{   
    /// <summary>
    /// Logique d'interaction pour Régions.xaml
    /// </summary>
    public partial class Régions : Window
    {
        GstBdd gstbdd = new GstBdd();
        public Régions()
        {
            InitializeComponent();
        }

        // On récupère les informations de la région sélectionnée
        private void lstRégions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRégions.SelectedItem != null)
            {   
                TxtNomRégion.Text = (lstRégions.SelectedItem as Region).NomRegion;

                int index = 0;

                foreach(Secteur sec in gstbdd.getAllSecteurs())
                {
                    if(sec.LibSecteur.CompareTo((lstRégions.SelectedItem as Region).LeSecteur.LibSecteur) == 0)
                    {
                        break;
                    }
                    else
                    {
                        index++;
                    }

                }

                cboCodesSecteur.SelectedIndex = index;


                //cboCodesSecteur.Select = (lstRégions.SelectedItem as Region).LeSecteur.CodeSecteur;
            }
        }

        // Vérification avant la modification d'une région
        private void btnValider_Click(object sender, RoutedEventArgs e)
        {   
            if (lstRégions.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir une région à modifier", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (TxtNomRégion.Text == "")
            {
                MessageBox.Show("Veuillez saisir une région", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (cboCodesSecteur.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un secteur", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                gstbdd.UpdateRegion((lstRégions.SelectedItem as Region).CodeRegion,(cboCodesSecteur.SelectedItem as Secteur).CodeSecteur,TxtNomRégion.Text);
                lstRégions.ItemsSource = gstbdd.getAllRegions();
                MessageBox.Show("Modification réussie", "GSB", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Affiche le tableau des régions au chargement de la page
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lstRégions.ItemsSource = gstbdd.getAllRegions();
            cboCodesSecteur.ItemsSource = gstbdd.getAllSecteurs();
        }
    }
}
