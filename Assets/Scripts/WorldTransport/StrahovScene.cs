using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class StrahovScene : SceneController {
 
 
    public override void Start () {
        base.Start();
 
        
        if (prevScene == "HomeScene") {
            player.position = new Vector2(4.61f, 0.40f);
        }

        if (prevScene == "KampusScene")
        {
            player.position = new Vector2(-2.7f, 0.5f);
        }

        if (prevScene == "PubScene")
        {
            player.position = new Vector2(0.834f, 0.722f);
        }
        if (prevScene == "StrahovScene") // menza Workaround
        {
            player.position = new Vector2(3.413f, -1.299f);
        }
    }
}