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
    [SerializeField] int scorePerBrick = 10;
    [SerializeField] int scoreAcceleration = 10;

    Rigidbody2D m_rigidbody;
    Player m_player;
    bool isOffScreenHandled = false;
    int currScorePerBrick = 0;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_player = FindAnyObjectByType<Player>();
        currScorePerBrick = scorePerBrick;
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
            HitBrick(collision);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            currScorePerBrick = scorePerBrick;
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            collision.GetContacts(contacts);
            BatHit(contacts[0].point);
        }
    }

    void HitBrick(Collision2D collision)
    {

        m_player.AddScore(currScorePerBrick);
        currScorePerBrick += scoreAcceleration;

        StartCoroutine(DropBrick(collision.gameObject));
        StartCoroutine(DeleteBrick(collision.gameObject));
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
