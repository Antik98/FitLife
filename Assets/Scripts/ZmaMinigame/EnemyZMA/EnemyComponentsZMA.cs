using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyComponentsZMA
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private AnyStateAnimator animator;

    [SerializeField]
    private Collider2D collider;

    public Rigidbody2D RigidBody { get => rigidBody; }

    public AnyStateAnimator Animator { get => animator; }
    public Collider2D Collider { get => collider; }

}
