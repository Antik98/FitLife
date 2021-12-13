using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferencesZMA 
{
    [SerializeField]
    private GameObject projectilePrefab; 

    public GameObject ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
}
