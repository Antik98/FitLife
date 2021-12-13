using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : CommandZMA
{
    private PlayerZMA player;
    public JumpCommand(PlayerZMA player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKeyDown()
    {
        player.Actions.Jump();
    }
}
