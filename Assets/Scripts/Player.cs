using UnityEngine;

public class Player : MonoBehaviour
{
    int score = 0;
    UI m_ui;

    void Start()
    {
        m_ui = FindAnyObjectByType<UI>();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        m_ui.UpdateScore(score);
    }
}
