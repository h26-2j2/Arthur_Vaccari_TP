using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class AlimentInfo
{
    public string nom;
    public GameObject objet;
    public AudioClip sonNom;
}

public class JeuNiveau1 : MonoBehaviour
{
    [Header("Aliments")]
    public AlimentInfo[] aliments;

    [Header("UI")]
    public TextMeshProUGUI textePoints;
    public GameObject uiVictoire;
    public GameObject uiDefaite;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonBonneReponse;
    public AudioClip sonMauvaiseReponse;
    public AudioClip sonVictoire;
    public AudioClip sonDefaite;

    [Header("Paramètres")]
    public int pointsPourGagner = 5;
    public int erreursMax = 3;

    private int points = 0;
    private int erreurs = 0;
    private int indexDemande = -1;
    private bool partieTerminee = false;

    void Start()
    {
        points = 0;
        erreurs = 0;
        partieTerminee = false;

        if (uiVictoire != null)
        {
            uiVictoire.SetActive(false);
        }

        if (uiDefaite != null)
        {
            uiDefaite.SetActive(false);
        }

        MettreAJourPoints();
        NouvelleDemande();
    }

    void Update()
    {
        if (partieTerminee)
        {
            return;
        }

        // Appuie sur espace pour réentendre la demande
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JouerDemande();
        }
    }

    void MettreAJourPoints()
    {
        if (textePoints != null)
        {
            textePoints.text = "Points : " + points;
        }
    }

    void NouvelleDemande()
    {
        List<int> alimentsDisponibles = new List<int>();

        for (int i = 0; i < aliments.Length; i++)
        {
            if (aliments[i].objet != null && aliments[i].objet.activeSelf)
            {
                alimentsDisponibles.Add(i);
            }
        }

        if (alimentsDisponibles.Count == 0)
        {
            Gagner();
            return;
        }

        indexDemande = alimentsDisponibles[Random.Range(0, alimentsDisponibles.Count)];
        JouerDemande();
    }

    void JouerDemande()
    {
        if (indexDemande < 0 || indexDemande >= aliments.Length)
        {
            return;
        }

        if (audioSource != null && aliments[indexDemande].sonNom != null)
        {
            audioSource.PlayOneShot(aliments[indexDemande].sonNom);
        }
    }

    public void VerifierReponse(int indexChoisi)
    {
        if (partieTerminee)
        {
            return;
        }

        if (indexChoisi < 0 || indexChoisi >= aliments.Length)
        {
            return;
        }

        if (indexChoisi == indexDemande)
        {
            points++;
            MettreAJourPoints();

            if (audioSource != null && sonBonneReponse != null)
            {
                audioSource.PlayOneShot(sonBonneReponse);
            }

            if (aliments[indexChoisi].objet != null)
            {
                aliments[indexChoisi].objet.SetActive(false);
            }

            if (points >= pointsPourGagner)
            {
                Gagner();
            }
            else
            {
                Invoke(nameof(NouvelleDemande), 0.6f);
            }
        }
        else
        {
            erreurs++;

            if (audioSource != null && sonMauvaiseReponse != null)
            {
                audioSource.PlayOneShot(sonMauvaiseReponse);
            }

            if (erreursMax > 0 && erreurs >= erreursMax)
            {
                Perdre();
            }
        }
    }

    void Gagner()
    {
        partieTerminee = true;

        if (uiVictoire != null)
        {
            uiVictoire.SetActive(true);
        }

        if (audioSource != null && sonVictoire != null)
        {
            audioSource.PlayOneShot(sonVictoire);
        }
    }

    void Perdre()
    {
        partieTerminee = true;

        if (uiDefaite != null)
        {
            uiDefaite.SetActive(true);
        }

        if (audioSource != null && sonDefaite != null)
        {
            audioSource.PlayOneShot(sonDefaite);
        }
    }
}