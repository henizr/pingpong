using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] Rigidbody2D rigidbody2d;
    public static event Action<string> OnGoal;

    public override void OnStartServer()
    {
        rigidbody2d.simulated = true;
        rigidbody2d.velocity = Vector2.right * speed;
    }

    [ServerCallback]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.GetComponent<Player>()) return;
        float x = rigidbody2d.velocity.x > 0 ? -1 : 1;
        float y = (transform.position.y - collision.transform.position.y) / collision.collider.bounds.size.y;
        Vector2 direction = new Vector2(x, y).normalized;
        rigidbody2d.velocity = direction * speed;
    }

    [ServerCallback]
    void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.tag)

        {

            case "Left":

            case "Right":

                OnGoal?.Invoke(collision.tag);

                break;

        }

    }
}
