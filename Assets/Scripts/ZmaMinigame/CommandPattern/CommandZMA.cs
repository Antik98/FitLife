using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandZMA
{
    public KeyCode Key { get; private set; }

    public CommandZMA(KeyCode key)
    {
        this.Key = key;
    }

    public virtual void GetKeyDown()
    {
    }

    public virtual void GetKeyUp()
    {
    }
    public virtual void GetKey()
    {
    }
}
