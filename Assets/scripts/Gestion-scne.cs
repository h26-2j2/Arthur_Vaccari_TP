using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangerScene : MonoBehaviour
{
    public void AllerAuNiveau1()
    {
        SceneManager.LoadScene("Niveau1");
    }
}