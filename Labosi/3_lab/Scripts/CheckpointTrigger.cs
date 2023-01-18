using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private GameObject teleportTo;

    [SerializeField] private float transitionForce = 3;
    [SerializeField] private int damage = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && teleportTo != null)
        { 
            var player = collision.gameObject;
            player.GetComponent<PlayerMovement>().isTransporting = true;
            player.GetComponent<PlayerMovement>().transitionForce = transitionForce;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<Rigidbody2D>().position = new Vector2(player.GetComponent<Rigidbody2D>().position.x, transform.position.y - 1.4f);
            player.GetComponent<PlayerLife>().Hurt(damage);
        }

        if (collision.gameObject.name == "Player" && teleportTo == null)
        {
            var player = collision.gameObject;
            collision.gameObject.GetComponent<PlayerMovement>().isTransporting = false;
            collision.gameObject.transform.position = transform.position;
            var playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(0, 0);
            playerRb.gravityScale = 5;
        }
    }
}