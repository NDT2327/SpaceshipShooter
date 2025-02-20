using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuGame:MonoBehaviour
{
    public void start() {
        SceneManager.LoadScene(0);
    
    }

    public void control() {
        SceneManager.LoadScene(3);
    
    }

    public void end() {
        Application.Quit();
    }
}
