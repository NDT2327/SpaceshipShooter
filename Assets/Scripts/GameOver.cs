using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver:MonoBehaviour
{

    public void Menu()
    {
        SceneManager.LoadScene(1);

    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }


}
