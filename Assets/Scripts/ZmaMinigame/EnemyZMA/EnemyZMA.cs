using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZMA : MonoBehaviour
{
    [SerializeField]
    private EnemyStatsZMA stats;

    [SerializeField]
    private EnemyReferencesZMA references;

    [SerializeField]
    private EnemyComponentsZMA components;

    private EnemyActionsZMA actions;

    public EnemyStatsZMA Stats { get => stats; }
    public EnemyReferencesZMA References { get => references; }
    public EnemyComponentsZMA Components { get => components; }
    public EnemyActionsZMA Actions { get => actions; }

    private void Start()
    {
        actions = new EnemyActionsZMA(this);

        AnyStateAnimation[] animations = new AnyStateAnimation[] {
            new AnyStateAnimation(RIG.TORSO, "Torso_Idle", "Torso_Throw", "Torso_Charge"),
            new AnyStateAnimation(RIG.TORSO, "Torso_Charge"),
            new AnyStateAnimation(RIG.TORSO, "Torso_Throw"),

            new AnyStateAnimation(RIG.LEGS, "Legs_Idle", "Legs_Throw", "Legs_Charge"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Charge"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Throw")
        };

        components.Animator.AnimationTriggerEvent += Actions.Shoot;

        Components.Animator.AddAnimations(animations);

    }

    private void Update()
    {
        if ( Components.RigidBody.velocity == Vector2.zero)
        {
           Components.Animator.TryToPlayAnimation("Torso_Idle");
           Components.Animator.TryToPlayAnimation("Legs_Idle");
        }

        if (Time.time >= Stats.NextBulletTime )
        {
            float rand = Random.Range(Stats.MinBulletSpawnTimer, Stats.MaxBulletSpawnTimer);
            Stats.NextBulletTime = Time.time + rand;
            actions.Shoot("Shoot");
        }
    }

    private void FixedUpdate()
    {
    }
}
