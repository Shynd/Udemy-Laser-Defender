using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static int currentScore;
    private static Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public static void Score(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }

    public static void Reset()
    {
        currentScore = 0;
    }
}
