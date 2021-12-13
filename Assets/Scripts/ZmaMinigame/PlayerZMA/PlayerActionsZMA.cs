using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsZMA 
{
    private PlayerZMA player;

    public PlayerActionsZMA(PlayerZMA player)
    {
        this.player = player;
    }
    public void Move(Transform transform)
    {
        player.Components.RigidBody.velocity = new Vector2(player.Stats.Direction.x * player.Stats.Speed * Time.deltaTime, player.Components.RigidBody.velocity.y);

        if( player.Stats.Direction.x != 0 )
        {
            transform.localScale = new Vector3(player.Stats.Direction.x < 0 ? 2 : -2, 2, 1);
            player.Components.Animator.TryToPlayAnimation("Torso_Walk");
            player.Components.Animator.TryToPlayAnimation("Legs_Walk");
        }

        else if (player.Components.RigidBody.velocity == Vector2.zero) {
            player.Components.Animator.TryToPlayAnimation("Torso_Idle");
            player.Components.Animator.TryToPlayAnimation("Legs_Idle");
        }
    }

    public void Jump()
    {
        if(player.Utilities.IsGrounded() )
        {
            player.Components.RigidBody.AddForce(new Vector2(0, player.Stats.JumpForce), ForceMode2D.Impulse);
            player.Components.Animator.TryToPlayAnimation("Legs_Jump");
            player.Components.Animator.TryToPlayAnimation("Torso_Jump");
        }
        
    }

    public void Shoot(string animation)
    {
        if(animation == "Shoot")
        {
            //GameObject go = GameObject.Instantiate(player.References.ProjectilePrefab, Quaternion.identity);

            Vector3 direction = new Vector3(player.transform.localScale.x, 0);

            //go.GetComponent<Projectile>().Setup(direction);
        }
    }

    public void Collide(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Debug.Log("Collected");
        }
    }

    public void TakeHit()
    {
        UIManager.Instance.RemoveLife();
    }
}
