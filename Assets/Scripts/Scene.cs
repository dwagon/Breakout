using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    [SerializeField] GameObject brick;
    [SerializeField] GameObject brickArea;
    [SerializeField] float xPadding = 0.2f;
    [SerializeField] float yPadding = 0.2f;
    [SerializeField] Vector2 brickSize; // We can't get brick size until we instantiate it, so can't predetermine
    List<GameObject> bricks;

    void Start()
    {
        BoxCollider2D m_collider = GetComponentInChildren<BoxCollider2D>();
        Vector2 top_left = m_collider.bounds.min;
        Vector2 bottom_right = m_collider.bounds.max;
        TileBricks(top_left, bottom_right);
    }

    void TileBricks(Vector2 tl, Vector2 br)
    {
        for (float x = tl.x + brickSize.x; x < br.x - brickSize.x; x += brickSize.x + xPadding)
        {
            for (float y = tl.y + brickSize.y; y < br.y - brickSize.y; y += brickSize.y + yPadding)
            {
                Vector3 position = new Vector3(x, y, 0f);
                Instantiate(brick, position, Quaternion.identity);
            }
        }
    }

}
