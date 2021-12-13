using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private CollisionHandlerIntfc handler;

    private void Start()
    {
        handler = GetComponentInParent<CollisionHandlerIntfc>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        handler.CollisionEnter(gameObject.name, collision.gameObject);
    }
}
