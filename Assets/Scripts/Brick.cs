using UnityEngine;

public class Brick : MonoBehaviour
{

    void Update()
    {
        DeleteOffScreen();
    }

    void DeleteOffScreen()
    {
        Vector2 screen_position = Camera.main.WorldToScreenPoint(transform.position);

        if (screen_position.y < 0)    // Bottom of screen
        {
            Destroy(gameObject);
        }

    }
}
