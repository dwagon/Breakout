using UnityEngine;
using TMPro;
public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
