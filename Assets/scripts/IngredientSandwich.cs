using UnityEngine;

public class IngredientSandwich : MonoBehaviour
{
    private Niveau2 niveau2;

    void Start()
    {
        niveau2 = FindObjectOfType<Niveau2>();
    }

    void OnMouseDown()
    {
        niveau2.AjouterIngredient(gameObject);
    }
}