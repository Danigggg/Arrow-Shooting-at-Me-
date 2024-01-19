using UnityEngine;

public class EliminateArrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Arrow"))
            Destroy(collision.gameObject);
    }
}
