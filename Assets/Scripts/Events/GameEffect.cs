using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEffect : MonoBehaviour
{
    public bool done { get; protected set; }
    // Start is called before the first frame update
    public abstract IEnumerator execute();

    // Update is called once per frame
}