using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZMA : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsZMA stats;

    [SerializeField]
    private PlayerComponentsZMA components;

    [SerializeField]
    private PlayerReferencesZMA references;

    [SerializeField]
    private PlayerUtilitiesZMA utilities;

    private PlayerActionsZMA actions;

    public PlayerComponentsZMA Components { get => components; }
    public PlayerStatsZMA Stats { get => stats;}
    public PlayerActionsZMA Actions { get => actions; }
    public PlayerUtilitiesZMA Utilities { get => utilities; }

    private void Start()
    {
        actions = new PlayerActionsZMA(this);
        utilities = new PlayerUtilitiesZMA(this);
        stats.Speed = stats.WalkSpeed;

        AnyStateAnimation[] animations = new AnyStateAnimation[] {
            new AnyStateAnimation(RIG.TORSO, "Torso_Idle"),
            new AnyStateAnimation(RIG.TORSO, "Torso_Walk", "Torso_Jump", "Torso_Fall"),
            new AnyStateAnimation(RIG.TORSO, "Torso_Jump"),
            new AnyStateAnimation(RIG.TORSO, "Torso_Fall"),

            new AnyStateAnimation(RIG.LEGS, "Legs_Idle"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Walk", "Legs_Jump", "Legs_Fall"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Jump"),
            new AnyStateAnimation(RIG.LEGS, "Legs_Fall")
        };

        Components.Animator.AddAnimations(animations);
        
       // Components.Animator.AnimationTriggerEvent += Actions.Shoot;

        UIManager.Instance.AddLife(stats.Lives);
    }

    private void Update()
    {
        utilities.HandleInput();
        utilities.HandleAir();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        actions.Move(transform);
    }
}
