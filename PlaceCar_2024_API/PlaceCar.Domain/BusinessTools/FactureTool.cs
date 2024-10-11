using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessTools
{
    public static class FactureTool
    {

        public static bool FacturesNonPayé(List<Facture> factureList)
        {
            if (factureList.Count != 0) return true;
            else { return false; }
        }
        public static decimal CalculeTotalJours(DateTime dateDeb, DateTime dateFin)
        {
            TimeSpan t = dateFin - dateDeb;
            decimal tot = (decimal)t.TotalDays;
            return tot;
        }
        public static decimal CalculeTotalHeurs(DateTime dateDeb, DateTime dateFin)
        {
            TimeSpan t = dateFin - dateDeb;
            decimal tot = (decimal)t.TotalHours;
            return tot;
        }
        public static decimal CalculeSommeHeursExacte(DateTime dateDeb, DateTime dateFin,DateTime dateRes, FormuleDePrix formule,Personne personne)
        {
            //Protéger de l'utilisation par un co-développeur
            if(formule == null) throw new ArgumentNullException("formule");
            if (personne == null) throw new ArgumentException("personne");
            if (personne.Client == null) throw new ArgumentException("personne.client");
            if (personne.Employee == null) throw new ArgumentException("personne.employee");

            decimal heurTotal = CalculeTotalHeurs(dateDeb, dateFin);
            decimal Prixtot = heurTotal * formule.FORM_Prix;
            Prixtot = CalculeReduction(Prixtot, dateDeb, dateRes, formule);

            if(personne.Employee.Emp_Pers_Id != 0 && personne.Client.Cli_Id != 0)
            {
                Prixtot = Prixtot - (Prixtot * (decimal)0.20);
            }
            return Prixtot;
        }
       
        
        public static decimal CalculPrix(Reservation res, decimal difference, FormuleDePrix formprix, List<FormuleDePrix> formuleParking,Personne personne)
        {
            decimal PrixFaacture;
            //1- récupération de la découpe en annee/mois/jours...
            List<ReadFormuleOptionBO> fo = CalculePrixAdaptee(formuleParking, res.RES_DateDebut, res.RES_DateFin, formprix);

            ReadFormuleOptionBO optionChoisie = fo.FirstOrDefault();

            PrixFaacture = PrixInitial(optionChoisie); 
             //PrixFaacture = (decimal)( (optionChoisie.totalHeurs * optionChoisie.PrixHeur) 
             //                            + (optionChoisie.totalJours * optionChoisie.PrixlJour) 
             //                               + (optionChoisie.totalSemaines * optionChoisie.PrixSemaine) 
             //                                   + (optionChoisie.totalMois * optionChoisie.PrixMois) 
             //                                       + (optionChoisie.totalTrimestre * optionChoisie.PrixTrimestre) 
             //                                           + (optionChoisie.totalDemiAnnee * optionChoisie.PrixDemiAnnee) 
             //                                               + (optionChoisie.totalAnnees * optionChoisie.PrixAnnee) ); //Prix de base initial

             //Regarder ce qui reste !!!
             decimal prixSupp = 0;
  
            if (difference > 0)
            {
                //je dois savoir combien il reste d'heure ou de mois ou de jours à facturer

               var detailsSupplement = RecuperationRepartionFormule((double)difference/24, res.RES_DateFin.Value, DateTime.Now);

               List<ReadFormuleOptionBO> f = CalculePrixAdaptee(formuleParking, res.RES_DateFin.Value, DateTime.Now, null);

                ReadFormuleOptionBO optionSupplementaire = f.OrderBy(m=>m.Total).FirstOrDefault();

                prixSupp = (int)( (detailsSupplement.totalHeurs * optionChoisie.PrixHeur)
                                        + (detailsSupplement.totalJours * optionSupplementaire.PrixlJour)
                                            + (detailsSupplement.totalSemaines * optionSupplementaire.PrixSemaine)
                                               + (detailsSupplement.totalMois * optionSupplementaire.PrixMois)
                                                   + (detailsSupplement.totalTrimestre * optionSupplementaire.PrixTrimestre)
                                                       + (detailsSupplement.totalDemiAnnee * optionSupplementaire.PrixDemiAnnee)
                                                           + (detailsSupplement.totalAnnees * optionSupplementaire.PrixAnnee));//Prix supplementaire

                decimal PrixAvecDepassementEventuel = PrixFaacture + prixSupp;
                PrixFaacture = Math.Round(PrixAvecDepassementEventuel);
                
            }
            if (personne.Employee != null && personne.Employee.Emp_Pers_Id != 0 && personne.Client.Cli_Id != 0)
            {
                PrixFaacture = PrixFaacture - (PrixFaacture * (decimal)0.20);
            }

            decimal p = CalculeReduction(PrixFaacture, res.RES_DateDebut, res.RES_DateReservation, formprix);
            PrixFaacture = p;
            return PrixFaacture;
        }

       public static decimal PrixInitial(ReadFormuleOptionBO optionChoisie)
        {
            decimal PrixFaacture = 0;
            
            // Calculer prix en fonction de la période choisi
            if (optionChoisie.Description == "Formule horaire")
            {
                PrixFaacture = optionChoisie.Total;
            }
            else if (optionChoisie.Description == "Formule journalière")
            {
                PrixFaacture = optionChoisie.Total;
            }
            else if (optionChoisie.Description == "Formule hebdomadaire")
            {
                PrixFaacture = optionChoisie.Total;
            }
            else if (optionChoisie.Description == "Formule mensuelle")
            {
                PrixFaacture = optionChoisie.Total;
            }
            else if (optionChoisie.Description == "Formule trimestrielle")
            {
                PrixFaacture = optionChoisie.Total;
            }
            else if (optionChoisie.Description == "Formule demi-annuelle")
            {
                PrixFaacture = optionChoisie.Total;
            }
            else if (optionChoisie.Description == "Formule annuelle")
            {
                PrixFaacture = optionChoisie.Total;
            }
            return PrixFaacture;
        }

        private static decimal CalculeReduction(decimal prixPrecalcule, DateTime dateDeb, DateTime dateRes, FormuleDePrix formprix)
        {

            int seuilDeReductionLonq = 30; // en jour
            int seuilDeReductionMyoenne = 15; // en jour
            int seuilDeReductionCourt = 2; // en jour
            decimal prix;


            TimeSpan delaiDeReservation = dateDeb - dateRes;


            if (delaiDeReservation.Days >= seuilDeReductionLonq)
            {
                
                
                    prix = prixPrecalcule - (prixPrecalcule * (decimal)0.30);
               
            }
            else if (delaiDeReservation.Days >= seuilDeReductionMyoenne)
            {
                
                
                    prix = prixPrecalcule - (prixPrecalcule * (decimal)0.20);
                
            }
            else if (delaiDeReservation.Days >= seuilDeReductionCourt)
            {
               
               
                    prix = prixPrecalcule - (prixPrecalcule * (decimal)0.10);
              
            }
            else
            {
                prix = prixPrecalcule - (prixPrecalcule * (decimal)0.05);
            }
            return prix;
        }
       

        // Calcule pour delete ou cloture reservation avant le debut 
        public static decimal CalculePrixDeleteReservatio(Reservation res, FormuleDePrix formprix, List<FormuleDePrix> formuleParking, Personne personne)// avant date debut
        {
            decimal prixDeBase = CalculPrix(res,0,formprix,formuleParking,personne);//Calcule du prix initiale
            
            int seuilDeReductionLonq = 20; // en jour
            int seuilDeReductionMyoenne = 10; // en jour
            int seuilDeReductionCourt = 2; // en jour
            decimal prix;


            TimeSpan delaiDeReservation = res.RES_DateDebut - res.RES_DateReservation;


            if (delaiDeReservation.Days >= seuilDeReductionLonq && res.RES_DureeTotal_Initiale/24 >= 7)
            {
                prix = prixDeBase - (prixDeBase * (decimal)0.90);//formprix.FORM_Prix
            }
            else if (delaiDeReservation.Days >= seuilDeReductionMyoenne && res.RES_DureeTotal_Initiale/24 >= 7)
            {
                prix = prixDeBase - (prixDeBase * (decimal)0.80);             
            }
            else if (delaiDeReservation.Days >= seuilDeReductionCourt)
            {      
                prix = prixDeBase - (prixDeBase * (decimal)0.70);
               
            }
            else
            {
                prix = prixDeBase;
            }
            return prix;
        }

        public static List<ReadFormuleOptionBO> CalculePrixAdaptee(List<FormuleDePrix> formules, DateTime dateDeb, DateTime? dateFin,FormuleDePrix? formule)
        {
            DateTime DateFinReelle = dateFin.HasValue ? dateFin.Value : dateDeb;

            //var totalResJours = DateFinReelle != dateDeb ? (DateFinReelle.AddDays(1) - dateDeb).TotalDays : 0;
            var totalResJours = DateFinReelle != dateDeb ? (DateFinReelle - dateDeb).TotalDays : 0;

            DetailsPrix decoup = RecuperationRepartionFormule(totalResJours, dateDeb, DateFinReelle);

            var totalAnnees = decoup.totalAnnees;
            var totalDemiAnnee = decoup.totalDemiAnnee;
            var totalTrimestre = decoup.totalTrimestre;
            var totalMois = decoup.totalMois;
            var totalSemaines = decoup.totalSemaines;
            var totalJours = decoup.totalJours;
            var totalHeurs = decoup.totalHeurs;

            

            var FormAnnee = formules.Find(f => f.FormuleDePrixType.FORM_Title == "Annee");
            var FormDemiAnnee = formules.Find(f => f.FormuleDePrixType.FORM_Title == "Demi-Année");
            var formTrimestre = formules.Find(f => f.FormuleDePrixType.FORM_Title == "Trimestre");
            var FormMois = formules.Find(f => f.FormuleDePrixType.FORM_Title == "Mois");
            var FormSemaine = formules.Find(f => f.FormuleDePrixType.FORM_Title == "Semaine");
            var FormJour = formules.Find(f => f.FormuleDePrixType.FORM_Title == "Jour");
            var FormHeure = formules.Find(f => f.FormuleDePrixType.FORM_Title == "Heure");

            var options = new List<ReadFormuleOptionBO>();

            if ((formule != null && formule == FormAnnee) || (FormAnnee != null && totalAnnees >= 1))
            {
                //var joursRestants = totalJours - (totalAnnees * 365);
                var joursRestants = totalResJours % 365;
                var joursRestantsQuotient = totalAnnees - (int)totalAnnees;

                if (joursRestantsQuotient >= 0.5)
                {
                    var totalYearPrice = (decimal)totalAnnees * FormAnnee.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormAnnee.FORM_Id,
                        Description = "Formule annuelle",
                        PriceDetails = $"{(int)totalAnnees} x Formule annuelle {FormAnnee.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalYearPrice * 100) / 100,
                         
                        totalAnnees = totalAnnees, 
                        totalJours= joursRestants,
                        PrixAnnee = (double)FormAnnee.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix

                    });
                }
                else  // Si moins de la moitié d'une demi-année est entamée, appliquer un principe similaire pour les jours restants
                {
                    joursRestants = joursRestants == 0 ? 1 : joursRestants;  // Si aucun jour restant, on considère 1 jour entamé
                    var totalDayPrice = (decimal)totalDemiAnnee * FormAnnee.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;

                    // Ajout d'une option avec les détails de la formule demi-annuelle, en incluant les jours restants
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormAnnee.FORM_Id,
                        Description = "Formule annuelle",
                        PriceDetails = $"{(int)totalDemiAnnee} x Formule annuelle {FormAnnee.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalDayPrice * 100) / 100,  // Arrondir au centime près

                        totalAnnees = totalAnnees,
                        totalJours = joursRestants,
                        PrixAnnee = (double)FormAnnee.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    });
                }

            }

            if ((formule != null && formule == FormDemiAnnee) || (FormDemiAnnee != null && totalDemiAnnee >= 1))
            {
                //var joursRestants = totalJours - (totalAnnees * 182);
                var joursRestants = totalResJours % 182;
                var joursRestantsQuotient = totalDemiAnnee - (int)totalDemiAnnee;

                if (joursRestantsQuotient >= 0.5)
                {
                    var totalSemiYearPrice = (decimal)totalAnnees * FormDemiAnnee.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormDemiAnnee.FORM_Id,
                        Description = "Formule demi-annuelle",
                        PriceDetails = $"{(int)totalDemiAnnee} x Formule demi - annuelle {FormDemiAnnee.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalSemiYearPrice * 100) / 100,

                        totalDemiAnnee = totalDemiAnnee,
                        totalJours = joursRestants,
                        PrixDemiAnnee = (double)FormDemiAnnee.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    });
                }
                else  // Si moins de la moitié d'une demi-année est entamée, appliquer un principe similaire pour les jours restants
                {
                    joursRestants = joursRestants == 0 ? 1 : joursRestants;  // Si aucun jour restant, on considère 1 jour entamé
                    var totalDayPrice = (decimal)totalDemiAnnee * FormDemiAnnee.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;

                    // Ajout d'une option avec les détails de la formule demi-annuelle, en incluant les jours restants
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormDemiAnnee.FORM_Id,
                        Description = "Formule demi-annuelle",
                        PriceDetails = $"{(int)totalDemiAnnee} x Formule demi-annuelle {FormDemiAnnee.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalDayPrice * 100) / 100,  // Arrondir au centime près

                        totalDemiAnnee = totalDemiAnnee,
                        totalJours = joursRestants,
                        PrixDemiAnnee = (double)FormDemiAnnee.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    });
                }

            }
            if ((formule != null && formule == formTrimestre) || (formTrimestre != null && totalTrimestre >= 1) )
            {
                // var moisRestants = totalMois - (totalTrimestre * 3);

                var joursRestants = totalResJours % 91;
                var joursRestantsQuotient = totalTrimestre - (int)totalTrimestre;

                if (joursRestantsQuotient >= 0.5)
                {
                    var totalQuarterPrice = (decimal)totalTrimestre * formTrimestre.FORM_Prix + (decimal)joursRestants * FormMois.FORM_Prix;
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = formTrimestre.FORM_Id,
                        Description = "Formule trimestrielle",
                        PriceDetails = $"{(int)totalTrimestre} x Formule trimestrielle {formTrimestre.FORM_Prix}€\n{joursRestants} x mois {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalQuarterPrice * 100) / 100,

                        totalTrimestre = totalTrimestre,
                        totalJours = joursRestants,
                        PrixTrimestre = (double)formTrimestre.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    });                  
                }
                else  // Si moins de la moitié d'un trimestre est entamée, appliquer un principe similaire pour les jours restants
                {
                    joursRestants = joursRestants == 0 ? 1 : joursRestants;  // Si aucun jour restant, on considère 1 jour entamé
                    var totalDayPrice = (decimal)totalTrimestre * formTrimestre.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;

                    // Ajout d'une option avec les détails de la formule trimestrielle, en incluant les jours restants
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = formTrimestre.FORM_Id,
                        Description = "Formule trimestrielle",
                        PriceDetails = $"{(int)totalTrimestre} x Formule trimestrielle {formTrimestre.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalDayPrice * 100) / 100,  // Arrondir au centime près

                        totalTrimestre = totalTrimestre,
                        totalJours = joursRestants,
                        PrixTrimestre = (double)formTrimestre.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    });
                }
            }

            if ((formule != null && formule == FormMois) || (FormMois != null && totalMois >= 1))
            {
                //var joursRestants = totalJours - (totalMois * 30);
                var joursRestants = totalResJours % 30;
                var joursRestantsQuotient = totalMois - (int)totalMois;

                if (joursRestantsQuotient >= 0.5)
                {
                    var totalMonthPrice = (decimal)totalMois * FormMois.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormMois.FORM_Id,
                        Description = "Formule mensuelle",
                        PriceDetails = $"{(int)totalMois} x Formule mensuelle {FormMois.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalMonthPrice * 100) / 100,

                        totalMois = totalMois,
                        totalJours = joursRestants,
                        PrixMois = (double)FormMois.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    });
                }
                else 
                {
                    // Calcul des jours restants après les mois
                    joursRestants = joursRestants == 0 ? 1 : joursRestants;  // Si aucun jour restant, on considère 1 jour entamé
                    var totalDayPrice = (decimal)totalMois * FormMois.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;

                    // Ajout d'une option avec les détails de la formule mensuelle, en incluant les jours restants
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormMois.FORM_Id,
                        Description = "Formule mensuelle",
                        PriceDetails = $"{(int)totalMois} x Formule mensuelle {FormMois.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalDayPrice * 100) / 100,  // Arrondir au centime près

                        totalMois = totalMois,
                        totalJours = joursRestants,
                        PrixMois = (double)FormMois.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    });
                }
            }

            if ((formule != null && formule == FormSemaine) || (FormSemaine != null && totalSemaines >= 1) )
            {
                var joursRestants = totalJours % 7;
                var joursRestantsQuotient = totalSemaines - (int)totalSemaines;

                if (joursRestantsQuotient >= 0.5)
                {
                    var totalWeekPrice = (int)totalSemaines * FormSemaine.FORM_Prix + (decimal)joursRestants * FormJour.FORM_Prix;
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormSemaine.FORM_Id,
                        Description = "Formule hebdomadaire",
                        PriceDetails = $"{(int)totalSemaines} x Formule hebdomadaire {FormSemaine.FORM_Prix}€\n{joursRestants} x jours {FormJour.FORM_Prix}€",
                        Total = Math.Round(totalWeekPrice * 100) / 100,

                        totalSemaines = totalSemaines,
                        totalJours = joursRestants,
                        PrixSemaine = (double)FormSemaine.FORM_Prix,
                        PrixlJour = (double)FormJour.FORM_Prix
                    }); ;
                     
                }
                else 
                {
                   
                    var heuresRestants = totalHeurs % 24;
                    heuresRestants = heuresRestants == 0 ? 1 : heuresRestants; //Permet de mettre en place le principe de heure entamée , heure payée
                    var heursRestantsQuotient = totalJours - (int)totalJours;
                    var totalWeekPrice = (int)totalSemaines * FormSemaine.FORM_Prix + (decimal)heuresRestants * FormHeure.FORM_Prix;
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormSemaine.FORM_Id,
                        Description = "Formule hebdomadaire",
                        PriceDetails = $"{(int)totalSemaines} x Formule hebdomadaire {FormSemaine.FORM_Prix}€\n{heuresRestants} x heures {FormHeure.FORM_Prix}€",
                        Total = Math.Round(totalWeekPrice * 100) / 100,

                        totalSemaines = totalSemaines, 
                        totalHeurs = heuresRestants,
                        PrixSemaine = (double)FormSemaine.FORM_Prix,
                        PrixHeur = (double)FormHeure.FORM_Prix
                    }); ;
                }
                
            }

            if ((formule != null && formule == FormJour) || (FormJour != null && totalJours >= 1))
            {
                var heuresRestants = totalHeurs % 24;
                var heursRestantsQuotient = totalJours - (int)totalJours;
               
                if (heursRestantsQuotient >= 0.5)
                {
                    var totalDayPrice = (int)totalJours * FormJour.FORM_Prix + (decimal)heuresRestants * FormHeure.FORM_Prix;
                    if (totalAnnees < 0 || totalMois < 0 || totalSemaines < 2)
                    {
                        options.Add(new ReadFormuleOptionBO
                        {
                            FormuleId = FormJour.FORM_Id,
                            Description = "Formule journalière",
                            PriceDetails = $"{(int)totalJours} x jours {FormJour.FORM_Prix}€\n{heuresRestants} x heures {FormJour.FORM_Prix}€",
                            Total = totalDayPrice,

                            totalJours = totalJours,
                            totalHeurs = heuresRestants,
                            PrixlJour = (double)FormJour.FORM_Prix,
                            PrixHeur = (double)FormHeure.FORM_Prix
                        });
                    }
                }
                else
                {
                    var totalDayPrice = (int)totalJours * FormJour.FORM_Prix + (decimal)heuresRestants * FormHeure.FORM_Prix;
                    if (totalAnnees < 0 || totalMois < 0 || totalSemaines < 2)
                    {
                        options.Add(new ReadFormuleOptionBO
                        {
                            FormuleId = FormJour.FORM_Id,
                            Description = "Formule journalière",
                            PriceDetails = $"{(int)totalJours} x jours {FormJour.FORM_Prix}€",
                            Total = totalDayPrice,

                            totalJours = totalJours, 
                            PrixlJour = (double)FormJour.FORM_Prix, 
                        });
                    }
                }
               
            }

            if ((formule != null && formule == FormHeure) || (FormHeure != null && totalHeurs >= 1))
            {
                var totalHourPrice = (decimal)totalHeurs * FormHeure.FORM_Prix;
                if (totalJours < 10)
                {
                    options.Add(new ReadFormuleOptionBO
                    {
                        FormuleId = FormHeure.FORM_Id,
                        Description = "Formule horaire",
                        PriceDetails = $"{(int)totalHeurs} x heures {FormHeure.FORM_Prix}€",
                        Total = totalHourPrice,

                        totalHeurs = totalHeurs,
                        PrixHeur = (double)FormHeure.FORM_Prix
                    });
                }
            }

            return options;
        }

        private static DetailsPrix RecuperationRepartionFormule(double totalResJours, DateTime dateDeb, DateTime dateFinReelle)
        {
            return new DetailsPrix()
            {
                totalAnnees = totalResJours / 365,
                totalDemiAnnee = totalResJours / 182,
                totalTrimestre = totalResJours / 91,
                totalMois = totalResJours / 30,
                totalSemaines = (totalResJours / 7),
                totalJours = (totalResJours / 1),//Math.Floor
                totalHeurs = dateFinReelle != dateDeb ? Math.Floor((dateFinReelle.AddDays(1) - dateDeb).TotalHours) : 1
            };
           


        }
    }

    //public static decimal GetFormuleDays(DateTime dd, string nomType)
    //{
    //    DateTime df;
    //    switch (nomType)
    //    {
    //        case "Heure":
    //            df = dd.AddHours(1);
    //            break;

    //        case "Jour":
    //            df = dd.AddDays(1);
    //            break;

    //        case "Semaine":
    //            df = dd.AddDays(7);
    //            break;

    //        case "Mois":
    //            df = dd.AddMonths(1);
    //            break;

    //        case "Trimestre":
    //            df = dd.AddMonths(3);
    //            break;

    //        case "Demi-Année":
    //            df = dd.AddMonths(6);
    //            break;

    //        case "Annee":
    //            df = dd.AddYears(1);
    //            break;

    //        default:
    //            throw new ArgumentException("Le tipe de formule n'est pas correcte");
    //    }
    //    decimal result = CalculeTotalJours(dd, df);
    //    return result;
    //}
    //public static decimal CalculeDifference(decimal dureeInit, decimal dureeReel)
    //{

    //    decimal i = Math.Round(dureeInit);
    //    decimal d = Math.Round(dureeReel);

    //    decimal reponce = (d) - (i);

    //    return reponce;
    //}
}
