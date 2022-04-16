using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNothingGameEffect : GameEffect
{
    public override IEnumerator execute()
    {
        yield return null;
    }
}
