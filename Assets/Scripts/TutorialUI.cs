using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialUI;
    public Button backButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialUI.SetActive(true);
        backButton.onClick.AddListener(GoBackToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMainMenu();
        }
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
