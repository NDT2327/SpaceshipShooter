using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuGame:MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(0);
        Debug.Log("Start Game Clicked");
    
    }

    public void Tutorial() {
        SceneManager.LoadScene(3);
        Debug.Log("Tutorial Clicked");
    
    }

    public void Quit() {
        Application.Quit();
        Debug.Log("Quit Game Clicked");
    }
}
