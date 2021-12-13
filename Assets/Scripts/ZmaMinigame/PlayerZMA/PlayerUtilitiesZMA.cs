using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilitiesZMA
{
    PlayerZMA player;

    private List<CommandZMA> commands = new List<CommandZMA>();

    public PlayerUtilitiesZMA(PlayerZMA player)
    {
        this.player = player;

        commands.Add(new JumpCommand(player, KeyCode.Space));  
    }
    public void HandleInput()
    {
        player.Stats.Direction = new Vector2(Input.GetAxisRaw("Horizontal"), player.Components.RigidBody.velocity.y);

        foreach (CommandZMA command in commands)
        {
            if(Input.GetKeyDown(command.Key))
            {
                command.GetKeyDown();
            }

            if (Input.GetKeyUp(command.Key))
            {
                command.GetKeyUp();
            }

            if (Input.GetKey(command.Key))
            {
                command.GetKey();
            }

        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.Components.Collider.bounds.center, player.Components.Collider.bounds.size, 0, Vector2.down, 0.1f, player.Components.GroundLayer);
        
        return hit.collider != null;
    }

    public void HandleAir()
    {
        if(IsFalling())
        {
            player.Components.Animator.TryToPlayAnimation("Torso_Fall");
            player.Components.Animator.TryToPlayAnimation("Legs_Fall");
        }

        if(!IsFalling())
        {
            player.Components.Animator.OnAnimationDone("Torso_Fall");
            player.Components.Animator.OnAnimationDone("Legs_Fall");
        }

    }

    private bool IsFalling()
    {
        if( player.Components.RigidBody.velocity.y < 0 && !IsGrounded() )
        {
            return true;
        }

        return false;
    }
}
