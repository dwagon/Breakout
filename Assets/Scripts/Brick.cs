using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] float timeTillBrickDeletes = 4f;
    [SerializeField] float timeTillBrickDrops = 2f;
    bool isHit = false;

    ParticleSystem m_particle;

    void Start()
    {
        m_particle = GetComponent<ParticleSystem>();
    }

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


    // Brick has been hit by a ball - return true if freshly hit
    public bool Hit()
    {
        if (!isHit)
        {
            isHit = true;
            m_particle.Play();
            StartCoroutine(DropBrick());
            StartCoroutine(DeleteBrick());
            return true;
        }
        return false;
    }

    IEnumerator DropBrick()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(timeTillBrickDrops);
        if (gameObject != null)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    IEnumerator DeleteBrick()
    {
        yield return new WaitForSeconds(timeTillBrickDeletes);
        Destroy(gameObject);
    }
}
