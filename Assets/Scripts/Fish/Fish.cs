using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] ContextBehavior contextBehaviour;
    [SerializeField] NonContextBehavior nonContextBehavior;
    [SerializeField] AgentFilter filter;
    [SerializeField, Range(1, 10)] float neighborRadius = 1.5f;
    [SerializeField, Range(0, 1)] float avoidanceRadiusMultiplier = 0.5f;

    [Header("Move Settings")]
    [SerializeField, Range(1, 5)] float standartMoveSpeed = 5f;
    [SerializeField, Range(5, 10)] float maxMoveSpeed = 10f;
    [SerializeField] float rotationSpeed = 2f;

    [Header("Loot Settings")]
    [SerializeField] Item lootItem;
    [SerializeField] float lootItemPushForce = 5;
    [SerializeField] float maxHp = 50;

    private List<Transform> filteredNeighbors = new List<Transform>();
    private List<Transform> neighbors = new List<Transform>();
    private Collider2D fishCollider;
    private Flock flock;
    private float squareNeighborRadius;
    private float avoidanceRadius;
    private float squareAvoidanceRadius;
    private Rigidbody2D rb2d;
    private HP hp;


    private Vector3 aPoint;
    private Vector3 bPoint;
    private Vector3 cPoint;
    private Vector3 dPoint;
    private Vector3 randomPoint;


    public float SquareAvoidanceRadius => squareAvoidanceRadius;
    public float AvoidanceRadius => avoidanceRadius;
    public float NeighborRadius => neighborRadius;
    public Collider2D FishCollider => fishCollider;
    public Flock Flock => flock;
    public HP Hp => hp;
    public Rigidbody2D Rb2d => rb2d;
    public float StandartMoveSpeed => standartMoveSpeed;
    public float MaxMoveSpeed => maxMoveSpeed;
    public float RotationSpeed => rotationSpeed;


    private void Awake()
    {
        fishCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        hp = new HP(maxHp);
    }


    private void OnEnable()
    {
        hp.OnDead += DropLoot;
    }


    private void OnDisable()
    {
        hp.OnDead -= DropLoot;
    }


    private void Start()
    {
        squareNeighborRadius = neighborRadius * neighborRadius;
        avoidanceRadius = neighborRadius * avoidanceRadiusMultiplier;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }


    private void FixedUpdate()
    {
        Move();
        InvertSprite();
    }


    private void Update()
    {
        FindNeighbors();
        FilterNeighbors();
    }


    private void Move()
    {
        if (!HasNeighbors())
        {
            nonContextBehavior.CalculateMove(this);
        }
        else
        {
            ContextMove();
        }
    }


    public void Init(Flock flock)
    {
        this.flock = flock;
    }


    private void ContextMove()
    {
        Vector2 moveDirection = contextBehaviour.CalculateMove(this, neighbors, filteredNeighbors, flock);

        rb2d.velocity = moveDirection * standartMoveSpeed;

        float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }


    private void InvertSprite()
    {
        if (transform.right.x < 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


    private void DropLoot()
    {
        Item spawnedLootItem = Instantiate(lootItem, transform.position, Quaternion.identity);
        Vector2 randomDirection = new Vector2(Random.Range(-1, 2f), Random.Range(-1, 2f));
        spawnedLootItem.Rb2d.AddForce(randomDirection * lootItemPushForce, ForceMode2D.Impulse);
    }


    private void FindNeighbors()
    {
        List<Transform> context = new List<Transform>();

        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, neighborRadius);

        foreach (Collider2D contextCollider in contextColliders)
        {
            if (contextCollider != fishCollider)
            {
                context.Add(contextCollider.transform);
            }
        }
        neighbors = context;
    }



    private void FilterNeighbors()
    {
        if (filter != null)
        {
            filteredNeighbors = filter.GetFilteredAgents(this, neighbors);
        }
    }


    private bool HasNeighbors()
    {
        return (neighbors.Count != 0 && filter == null) || (filteredNeighbors.Count != 0 && filter != null);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Weapon weapon))
        {
            hp.TakeDamage(weapon.Damage);
        }
    }


    public void SetDrawPoints(Vector3 aPoint, Vector3 bPoint, Vector3 cPoint, Vector3 dPoint, Vector3 randomPoint)
    {
        this.aPoint = aPoint;
        this.bPoint = bPoint;
        this.cPoint = cPoint;
        this.dPoint = dPoint;
        this.randomPoint = randomPoint;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, neighborRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);


        Gizmos.DrawLine(aPoint, bPoint);
        Gizmos.DrawLine(bPoint, cPoint);
        Gizmos.DrawLine(cPoint, dPoint);
        Gizmos.DrawLine(dPoint, aPoint);

        Gizmos.DrawLine(transform.position, randomPoint);
    }
}
