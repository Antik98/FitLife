using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatsZMA 
{
    public Vector2 Direction { get; set; }
    public float Speed { get; set; }
    public float WalkSpeed { get => walkSpeed; }
    public float JumpForce { get => jumpForce; }
    public int Lives { get => lives; set => lives = value; }

    [SerializeField]
    private float jumpForce = 0;

    [SerializeField]
    private float walkSpeed = 0;

    [SerializeField]
    private float runSpeed = 0; 

    [SerializeField]
    private int lives = 0;


}
