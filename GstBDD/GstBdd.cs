using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ClassesMetier;

namespace GstBDD
{
    public class GstBdd
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;

        // Constructeur
        public GstBdd()
        {
            string chaine = "Server=localhost;Database=gestionvisiteursgsb;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        // Obtenir toutes les régions
        public List<Region> getAllRegions()
        {
            List<Region> mesRegions = new List<Region>();
            // Ecrire votre requête
            cmd = new MySqlCommand("SELECT REG_CODE,REG_NOM,region.SEC_CODE,SEC_LIBELLE FROM region INNER JOIN secteur on region.SEC_CODE = secteur.SEC_CODE GROUP BY REG_CODE", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Region uneNouvelleRegion = new Region(Convert.ToInt16(dr[0].ToString()), (dr[1].ToString()),new Secteur(Convert.ToInt16(dr[2].ToString()),(dr[3].ToString())));
                mesRegions.Add(uneNouvelleRegion);

            }
            dr.Close();
            return mesRegions;
        }

        // Obtenir tous les visiteurs
        public List<Visiteurs> getAllVisiteurs()
        {
            List<Visiteurs> mesVisiteurs = new List<Visiteurs>();
            // Ecrire votre requête
            cmd = new MySqlCommand("SELECT VIS_MATRICULE, VIS_NOM, VIS_PRENOM, VIS_ADRESSE, VIS_CP, VIS_VILLE, VIS_DATEEMBAUCHE, visiteur.SEC_CODE,SEC_LIBELLE, visiteur.LAB_CODE,LAB_NOM,LAB_CHEFVENTE FROM `visiteur` INNER JOIN labo ON visiteur.LAB_CODE = labo.LAB_CODE INNER JOIN secteur on visiteur.SEC_CODE = secteur.SEC_CODE", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Visiteurs unNouveauVisiteur = new Visiteurs(Convert.ToInt16(dr[0].ToString()),(dr[1].ToString()), (dr[2].ToString()), (dr[3].ToString()), (dr[4].ToString()), (dr[5].ToString()), Convert.ToDateTime(dr[6].ToString()),new Secteur(Convert.ToInt16(dr[7].ToString()), (dr[8].ToString())),new Labo(Convert.ToInt16(dr[9].ToString()), (dr[10].ToString()), (dr[11].ToString())));
                mesVisiteurs.Add(unNouveauVisiteur);
            }
            dr.Close();
            return mesVisiteurs;
        }

        // Obtenir tous les secteurs
        public List<Secteur> getAllSecteurs()
        {
            List<Secteur> mesSecteurs = new List<Secteur>();
            // Ecrire votre requête
            cmd = new MySqlCommand("SELECT SEC_CODE,SEC_LIBELLE FROM secteur", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Secteur unNouveauSecteur = new Secteur(Convert.ToInt16(dr[0].ToString()), (dr[1].ToString()));
                mesSecteurs.Add(unNouveauSecteur);

            }
            dr.Close();
            return mesSecteurs;
        }

        // Obtenir tous les laboratoires
        public List<Labo> getAllLabos()
        {
            List<Labo> mesLabos = new List<Labo>();
            // Ecrire votre requête
            cmd = new MySqlCommand("SELECT LAB_CODE,LAB_NOM,LAB_CHEFVENTE FROM labo", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Labo unNouveauLabo = new Labo(Convert.ToInt16(dr[0].ToString()), (dr[1].ToString()), (dr[2].ToString()));
                mesLabos.Add(unNouveauLabo);

            }
            dr.Close();
            return mesLabos;
        }

        // Mise à jour de la région
        public void UpdateRegion(int numRegion, int numSecteur , string nomRegion)
        {
            cmd = new MySqlCommand("UPDATE region SET REG_NOM = '" +nomRegion+ "',region.SEC_CODE ='"+numSecteur+"' WHERE region.REG_CODE =" + numRegion, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            dr.Close();
        }

        // Mise à jour du visiteur
        public void UpdateVisiteurs(string nom,string prenom,string adressse,string cp,int secteur,string ville,int year,int month,int day,TimeSpan time,int codeLabo, string matricule)
        {
            cmd = new MySqlCommand("UPDATE visiteur SET VIS_NOM ='" + nom + "',VIS_PRENOM = '" + prenom + "',VIS_ADRESSE = '" + adressse + "',VIS_CP = '" + cp + "',visiteur.SEC_CODE = '" + secteur + "',VIS_VILLE = '" + ville + "',VIS_DATEEMBAUCHE = '"+year+"-"+month+"-"+day+" "+time+"',visiteur.LAB_CODE = " + codeLabo + "  WHERE VIS_MATRICULE =" + matricule, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();
            dr.Close();
        }

        // Ajout d'une région
        public void AjouterRegion(string nomRegion,int secteur)
        {
            cmd = new MySqlCommand("INSERT INTO region (REG_NOM,SEC_CODE) VALUES ('" +nomRegion + "','" + secteur +"')", cnx);
            cmd.ExecuteNonQuery();
            dr.Close();
        }

        // Ajout d'un visiteur
        public void AjouterVisiteur(string nom, string prenom, string adresse, string cp,int secteur,string ville,int year,int month,int day,TimeSpan time,int codeLabo)
        {
            cmd = new MySqlCommand("INSERT INTO visiteur (VIS_NOM,VIS_PRENOM,VIS_ADRESSE,VIS_CP,visiteur.SEC_CODE,VIS_VILLE,VIS_DATEEMBAUCHE,visiteur.LAB_CODE) VALUES ('"+ nom + "','" + prenom + "', '" + adresse + "', '" + cp + "', '" + secteur + "','" + ville + "','" + year + "-" + month + "-" + day + " " + time + "','" + codeLabo + "')", cnx);
            cmd.ExecuteNonQuery();

            dr.Close();
        }

        // Assigner région de travail à un visiteur
        public void AjouterRegionAVisiteur(int matricule,int year,int month,int day,TimeSpan time, int coderegion, string poste)
        {
            cmd = new MySqlCommand("INSERT INTO travailler (VIS_MATRICULE,JJMMAA,REG_CODE,TRA_ROLE) VALUES ('" + matricule + "','" + year + "-" + month + "-" + day + " " + time + "','" + coderegion + "','" + poste + "')", cnx);
            cmd.ExecuteNonQuery();

            dr.Close();
        }

        // Obtenir la liste des régions dans un secteur
        public List<RegionsDansSecteurs> getAllRegionsDansSecteurs()
        {
            List<RegionsDansSecteurs> unTableau = new List<RegionsDansSecteurs>();
            // Ecrire votre requête
            cmd = new MySqlCommand("SELECT SEC_LIBELLE,COUNT(REG_CODE) FROM secteur INNER JOIN region ON secteur.SEC_CODE = region.SEC_CODE GROUP BY SEC_LIBELLE", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RegionsDansSecteurs uneDonnée = new RegionsDansSecteurs((dr[0].ToString()), Convert.ToInt16(dr[1].ToString()));
                unTableau.Add(uneDonnée);

            }
            dr.Close();
            return unTableau;
        }

        // Obtenir la liste de visiteurs sans secteur
        public List<SansSecteur> getVisiteursSansSecteurs()
        {
            List<SansSecteur> mesVisiteurs = new List<SansSecteur>();
            // Ecrire votre requête
            cmd = new MySqlCommand("SELECT VIS_NOM,VIS_PRENOM FROM visiteur WHERE visiteur.SEC_CODE IS null", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SansSecteur unNouveauVisiteur = new SansSecteur((dr[0].ToString()),(dr[1].ToString()));
                mesVisiteurs.Add(unNouveauVisiteur);
            }
            dr.Close();
            return mesVisiteurs;
        }

        // Nombre de visiteurs par région
        public Dictionary<string, int> GetDatasGraph1()
        {
            Dictionary<string, int> lesDatas = new Dictionary<string, int>();

            cmd = new MySqlCommand("SELECT REG_NOM,COUNT(travailler.REG_CODE)\n" +
                                    "FROM travailler\n" +
                                    "INNER JOIN region ON travailler.REG_CODE = region.REG_CODE\n" +
                                    "GROUP BY travailler.REG_CODE", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lesDatas.Add(dr[0].ToString(), Convert.ToInt16(dr[1].ToString()));
            }
            dr.Close();

            return lesDatas;
        }

        // Taux d'embauche par mois
        public Dictionary<string, double> GetDatasGraph2()
        {
            Dictionary<string, double> lesDatas = new Dictionary<string,double >();

            cmd = new MySqlCommand("SET @@lc_time_names ='fr_FR';\n"+
                                    "SELECT (SELECT MONTHNAME(JJMMAA)),COUNT(travailler.VIS_MATRICULE)\n" +
                                    "FROM travailler\n"+
                                    "INNER JOIN visiteur on travailler.VIS_MATRICULE = visiteur.VIS_MATRICULE\n"+
                                    "GROUP BY(SELECT MONTH(JJMMAA))", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lesDatas.Add(dr[0].ToString(), Convert.ToInt16(dr[1].ToString()));
            }
            dr.Close();

            return lesDatas;
        }
    }
}

