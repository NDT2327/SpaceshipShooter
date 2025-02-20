using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text textScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        textScore.text = playerScore.ToString();
    }
}
