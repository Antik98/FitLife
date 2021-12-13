using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyReferencesZMA
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private GameObject beamPrefab;

    [SerializeField]
    private Transform spawnPoint;

    public GameObject ProjectilePrefab { get => projectilePrefab; private set => projectilePrefab = value; }

    public Transform SpawnPoint { get => spawnPoint; private set => spawnPoint = value; }
}
