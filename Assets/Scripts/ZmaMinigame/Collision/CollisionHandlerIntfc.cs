using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollisionHandlerIntfc
{
    void CollisionEnter(string colliderName, GameObject other);
}
