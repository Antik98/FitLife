using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, CollisionHandlerIntfc
{
    [SerializeField]
    private float speed;

    private Vector2 direction;

    [SerializeField]
    private string targetTag;
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Setup(Vector2 direction)
    {
        this.direction = direction;
    }

    public void CollisionEnter(string colliderName, GameObject o)
    {
        if (colliderName == "DamageArea" && o.CompareTag("Player"))
        {
            o.GetComponent<PlayerZMA>().Actions.TakeHit();
            Destroy(this.gameObject);
        }

        else if (colliderName == "DamageArea" && o.CompareTag("Edge"))
        {
            Destroy(this.gameObject);
        }

    }
}
