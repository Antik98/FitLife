using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerComponentsZMA 
{
    [SerializeField]
    private Rigidbody2D rigidBody = null;

    [SerializeField]
    private AnyStateAnimator animator = null;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Collider2D collider = null;

    public Rigidbody2D RigidBody { get => rigidBody; }
    public LayerMask GroundLayer { get => groundLayer; }
    public Collider2D Collider { get => collider; }
    public AnyStateAnimator Animator { get => animator; }

}
