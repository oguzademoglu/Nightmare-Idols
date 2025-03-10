using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Vector3 target;

    [System.Obsolete]
    public void SetTarget(Vector3 targetPosition)
    {
        target = targetPosition;
        Vector3 direction = (target - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
