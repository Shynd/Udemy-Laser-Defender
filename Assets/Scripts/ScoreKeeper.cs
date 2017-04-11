using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int currentScore;

    public void Score(int points)
    {
        currentScore += points;
        GetComponent<Text>().text = currentScore.ToString();
    }

    public void Reset()
    {
        currentScore = 0;
        GetComponent<Text>().text = currentScore.ToString();
    }
}
