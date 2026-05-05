using UnityEngine;

public class Niveau2 : MonoBehaviour
{
    public int ingredientsNecessaires = 5;
    public int ingredientsCorrects = 0;

    public GameObject imageVictoire;
    public GameObject sandwichPret;

    void Start()
    {
        if (imageVictoire != null)
        {
            imageVictoire.SetActive(false);
        }

        if (sandwichPret != null)
        {
            sandwichPret.SetActive(false);
        }
    }

    public void ChoisirIngredient(Ingredient ingredient)
    {
        if (ingredient.estCorrect)
        {
            ingredientsCorrects++;

            ingredient.gameObject.SetActive(false);

            Debug.Log("Bon ingrédient!");

            if (ingredientsCorrects >= ingredientsNecessaires)
            {
                Victoire();
            }
        }
        else
        {
            Debug.Log("Mauvais ingrédient!");
        }
    }

    void Victoire()
    {
        Debug.Log("Victoire!");

        if (sandwichPret != null)
        {
            sandwichPret.SetActive(true);
        }

        if (imageVictoire != null)
        {
            imageVictoire.SetActive(true);
        }
    }
}