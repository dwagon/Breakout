using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    Vector2 m_move;
    void Start()
    {

    }

    void Update()
    {
        Vector2 location;
        location = transform.position;
        location.x += moveSpeed * m_move.x * Time.deltaTime;
        transform.position = location;
        CheckOffScreen();

    }

    void CheckOffScreen()
    {
        Vector2 screen_position = Camera.main.WorldToScreenPoint(transform.position);

        if (screen_position.x < 0)    // Left of screen
        {
            m_move = new Vector2(0f, 0f);
        }
        if (screen_position.x > Screen.width)    // Right of screen
        {
            m_move = new Vector2(0f, 0f);
        }
    }

    void OnMove(InputValue input)
    {
        m_move = input.Get<Vector2>();
    }
}
