using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionsZMA
{
    private EnemyZMA enemy;

    public EnemyActionsZMA(EnemyZMA enemy)
    {
        this.enemy = enemy;
    }

    public void Shoot(string animation)
    {
        if (animation == "Shoot")
        {
            enemy.Components.Animator.TryToPlayAnimation("Torso_Throw");
            enemy.Components.Animator.TryToPlayAnimation("Legs_Throw");

            GameObject go = GameObject.Instantiate(enemy.References.ProjectilePrefab, enemy.References.SpawnPoint.position, Quaternion.identity);

            Vector3 direction = new Vector3(enemy.transform.localScale.x, 0);

            go.GetComponent<Projectile>().Setup(direction);
        }
    }

    public void Move(Transform transform)
    {
        enemy.Components.RigidBody.velocity = new Vector2(0, enemy.Components.RigidBody.velocity.y);
    }


    public void Collide(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Debug.Log("Collected");
        }
    }
}
