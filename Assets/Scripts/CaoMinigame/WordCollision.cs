using UnityEngine;

public class WordCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.collider.tag == "Edge" )
        {
            Destroy( gameObject );
        }
    }
}
