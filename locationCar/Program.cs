using System;
using System.Collections.Generic;


//class catalog
public class Catalogue
{
    public static int vehiculeNumber = 3;
    static void Main(string[] args)
    {
        FabriqueAutomobileElectrique fabrique = new FabriqueAutomobileElectrique();
        Automobile auto = fabrique.FabriquerAutomobile("Tesla", "Bleu", 250, 2.5);
        auto.afficherCarac();
    }
}

public abstract class Automobile 
{
    protected string modele;
    protected string couleur;
    protected int puissance;
    protected double espace;

    public Automobile(string modele, string couleur, int puissance, double espace)
    {
        this.modele = modele;
        this.couleur = couleur;
        this.puissance = puissance;
        this.espace = espace;
    }

    public abstract void afficherCarac();

}
public class AutomobileElectrique : Automobile
{
    public AutomobileElectrique(string modele, string couleur, int puissance, double espace) : base(modele, couleur, puissance, espace) { }

    public override void afficherCarac()
    {
        Console.WriteLine("Automobile électrique : ");
        Console.WriteLine("Modèle : " + modele);
        Console.WriteLine("Couleur : " + couleur);
        Console.WriteLine("Puissance : " + puissance);
        Console.WriteLine("Espace : " + espace);
    }
}
public interface FabriqueVehicule
{
    public Automobile FabriqueAutomobile(string modele, string couleur, int puissance, double espace);

}



public class FabriqueVehiculeElectrique : FabriqueVehicule
{
    public Automobile createAutomobile(string modele, string couleur, int puissance, double espace)
    {
        return new AutomobileElectrique(modele, couleur, puissance, espace);
    }

    public Automobile FabriqueAutomobile(string modele, string couleur, int puissance, double espace)
    {
        throw new NotImplementedException();
    }
}

//Classe parente abstaitre Scooter
abstract class Scooter
{

}


//Classe ScooterElectrique hérite de la classe abstaitre Scooter
class ScooterElectrique : Scooter
{

}


//Classe ScooterEssance hérite de la classe abstaitre Scooter
class ScooterEssance : Scooter
{

}

/// <summary>
/// implémentation prof
/// </summary>

public abstract class Liasse
{
    protected IList<string> contenu =
        new List<string>();

    public abstract void ajouteDocument(string document);
    public abstract void imprime();
}


public class LiasseHtml : Liasse
{
    public override void ajouteDocument(string document)
    {
        if (document.StartsWith("<HTML>"))
            contenu.Add(document);
    }

    public override void imprime()
    {
        Console.WriteLine("Liasse HTML");
        foreach (string s in contenu)
            Console.WriteLine(s);
    }
}

public class LiassePdf : Liasse
{
    public override void ajouteDocument(string document)
    {
        if (document.StartsWith("<PDF>"))
            contenu.Add(document);
    }

    public override void imprime()
    {
        Console.WriteLine("Liasse PDF");
        foreach (string s in contenu)
            Console.WriteLine(s);
    }
}


public abstract class ConstructeurLiasseVehicule
{
    protected Liasse liasse;

    public abstract void construitBonDeCommande(string
      nomClient);

    public abstract void construitDemandeImmatriculation
      (string nomDemandeur);

    public Liasse resultat()
    {
        return liasse;
    }
}

public class ConstructeurLiasseVehiculeHtml :
 ConstructeurLiasseVehicule
{
    public ConstructeurLiasseVehiculeHtml()
    {
        liasse = new LiasseHtml();
    }

    public override void construitBonDeCommande(string
      nomClient)
    {
        string document;
        document = "<HTML>Bon de commande Client : " +
          nomClient + "</HTML>";
        liasse.ajouteDocument(document);
    }

    public override void construitDemandeImmatriculation
      (string nomDemandeur)
    {
        string document;
        document =
          "<HTML>Demande d'immatriculation Demandeur : " +
          nomDemandeur + "</HTML>";
        liasse.ajouteDocument(document);
    }
}

public class ConstructeurLiasseVehiculePdf :
 ConstructeurLiasseVehicule
{
    public ConstructeurLiasseVehiculePdf()
    {
        liasse = new LiassePdf();
    }

    public override void construitBonDeCommande(string
      nomClient)
    {
        string document;
        document = "<PDF>Bon de commande Client : " +
          nomClient + "</PDF>";
        liasse.ajouteDocument(document);
    }

    public override void construitDemandeImmatriculation
      (string nomDemandeur)
    {
        string document;
        document =
          "<PDF>Demande d'immatriculation Demandeur : " +
          nomDemandeur + "</PDF>";
        liasse.ajouteDocument(document);
    }
}

public class Vendeur
{
    protected ConstructeurLiasseVehicule constructeur;

    public Vendeur(ConstructeurLiasseVehicule constructeur)
    {
        this.constructeur = constructeur;
    }

    public Liasse construit(string nomClient)
    {
        constructeur.construitBonDeCommande(nomClient);
        constructeur.construitDemandeImmatriculation
          (nomClient);
        Liasse liasse = constructeur.resultat();
        return liasse;
    }
}
public class ClientVehicule
{
    static void Main(string[] args)
    {
        ConstructeurLiasseVehicule constructeur;
        Console.WriteLine("Voulez-vous construire " +
          "des liasses HTML (1) ou PDF (2) :");
        string choix = Console.ReadLine();
        if (choix == "1")
        {
            constructeur = new ConstructeurLiasseVehiculeHtml();
        }
        else
        {
            constructeur = new ConstructeurLiasseVehiculePdf();
        }
        Vendeur vendeur = new Vendeur(constructeur);
        Liasse liasse = vendeur.construit("Martin");
        liasse.imprime();
    }
}
