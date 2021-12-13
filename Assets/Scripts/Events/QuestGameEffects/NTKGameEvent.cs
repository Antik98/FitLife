using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTKGameEvent : GameEffect
{
    public override IEnumerator execute()
    {
        yield return new WaitForSeconds(1);
        this.GetComponent<ChangeScene>().Activate();
    }
}
