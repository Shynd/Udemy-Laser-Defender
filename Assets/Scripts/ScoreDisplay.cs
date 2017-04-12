using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = ScoreKeeper.currentScore.ToString();
        ScoreKeeper.Reset();
    }
}
