using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float timeTillBrickDeletes = 4f;
    [SerializeField] float timeTillBrickDrops = 2f;
    [SerializeField] GameObject bat;
    Rigidbody2D m_rigidbody;
    bool isOffScreenHandled = false;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isOffScreenHandled && IsOffScreen())
        {
            isOffScreenHandled = true;
            StartCoroutine(ResetLocation());
        }
    }

    IEnumerator ResetLocation()
    {
        yield return new WaitForSeconds(1f);
        Vector3 bat_position = bat.transform.position;
        transform.position = new Vector3(bat_position.x, bat_position.y + 1, bat_position.z);
        transform.rotation = Quaternion.identity;
        m_rigidbody.linearVelocity = new Vector2(0f, 0f);
        isOffScreenHandled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            StartCoroutine(DropBrick(collision.gameObject));
            StartCoroutine(DeleteBrick(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            collision.GetContacts(contacts);
            BatHit(contacts[0].point);
        }
    }

    void BatHit(Vector2 hitPoint)
    {
        Vector2 force;
        float difference = Math.Abs(bat.transform.position.x - hitPoint.x);
        float direction = Math.Sign(bat.transform.position.x - hitPoint.x); // 1 = Left; -1=Right
        if (direction < 0)
        {
            force = transform.right;
        }
        else
        {
            force = -transform.right;
        }
        m_rigidbody.AddForce(5 * difference * force, ForceMode2D.Impulse);
    }


    IEnumerator DropBrick(GameObject brick)
    {
        brick.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(timeTillBrickDrops);
        if (brick != null)
        {
            brick.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    IEnumerator DeleteBrick(GameObject brick)
    {
        yield return new WaitForSeconds(timeTillBrickDeletes);
        Destroy(brick);
    }

    bool IsOffScreen()
    {
        Vector2 screen_position = Camera.main.WorldToScreenPoint(transform.position);

        if (screen_position.y < 0)    // Bottom of screen
        {
            return true;
        }
        return false;
    }
}
