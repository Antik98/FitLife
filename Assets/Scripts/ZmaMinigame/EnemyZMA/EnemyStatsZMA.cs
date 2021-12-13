using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStatsZMA
{
    [SerializeField]
    private float jumpForce = 0;

    [SerializeField]
    private float walkSpeed = 0;

    [SerializeField]
    private float minBulletSpawnTimer = 0f;

    [SerializeField]
    private float maxBulletSpawnTimer = 0f;

    private float nextBulletTime = 1f;

    public float WalkSpeed { get => walkSpeed; }
    public float JumpForce { get => jumpForce; }
    public float NextBulletTime { get => nextBulletTime; set => nextBulletTime = value; }
    public float MinBulletSpawnTimer { get => minBulletSpawnTimer; }
    public float MaxBulletSpawnTimer { get => maxBulletSpawnTimer; }

}
