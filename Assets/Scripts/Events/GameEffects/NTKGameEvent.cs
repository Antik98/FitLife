using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTKGameEvent : GameEffect
{
    public override IEnumerator execute()
    {
        yield return null;
        this.GetComponent<ChangeScene>().Activate();
    }
}
