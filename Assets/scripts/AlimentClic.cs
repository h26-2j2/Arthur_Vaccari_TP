using UnityEngine;

public class AlimentClic : MonoBehaviour
{
    public JeuNiveau1 jeu;
    public int indexAliment;
    public Collider2D colliderMonstre;

    private Vector3 positionInitiale;
    private float zCamera;

    void Start()
    {
        positionInitiale = transform.position;
        zCamera = Camera.main.WorldToScreenPoint(transform.position).z;
    }

    void OnMouseDown()
    {
        positionInitiale = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 positionSouris = Input.mousePosition;
        positionSouris.z = zCamera;

        Vector3 positionMonde = Camera.main.ScreenToWorldPoint(positionSouris);
        positionMonde.z = transform.position.z;

        transform.position = positionMonde;
    }

    void OnMouseUp()
    {
        Collider2D monCollider = GetComponent<Collider2D>();

        if (monCollider != null && colliderMonstre != null && monCollider.bounds.Intersects(colliderMonstre.bounds))
        {
            if (jeu != null)
            {
                jeu.VerifierReponse(indexAliment);
            }

            if (gameObject.activeSelf)
            {
                transform.position = positionInitiale;
            }
        }
        else
        {
            transform.position = positionInitiale;
        }
    }
}