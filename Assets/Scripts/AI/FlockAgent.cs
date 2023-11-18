using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private Flock flock;
    private Collider2D agentCollider;
    private Rigidbody2D rb2d;

    public Collider2D AgentCollider => agentCollider;
    public Flock Flock => flock;


    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }


    public void Init(Flock flock)
    {
        this.flock = flock;
    }


    public void Move(Vector2 velocity)
    {
        rb2d.velocity = velocity;

        Vector2 moveDirection = (transform.position + (Vector3)velocity - transform.position).normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, flock.NeighborRadius);
    }
}
