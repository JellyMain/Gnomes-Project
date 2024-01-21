using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public enum BehavaiorState
{
    Peaceful,
    GroupMove,
    Threat,
    Hunting
}


public class Fish : MonoBehaviour
{
    [Header("AI Settings")]

    [SerializeField] ContextBehavior contextBehaviour;
    [SerializeField] NonContextBehavior nonContextBehavior;
    [SerializeField] ThreatBehavior threatenBehavior;
    [SerializeField] HuntingBehavior huntingBehavior;
    [SerializeField] AgentFilter filter;
    [SerializeField, Range(1, 10)] float neighborRadius = 1.5f;
    [SerializeField, Range(0, 1)] float avoidanceRadiusMultiplier = 0.5f;
    [SerializeField, Range(0.1f, 5f)] float stateSwitchCooldown = 3f;

    [Header("Move Settings")]
    [SerializeField, Range(1, 5)] float standartMoveSpeed = 5f;
    [SerializeField, Range(5, 10)] float maxMoveSpeed = 10f;
    [SerializeField] float rotationSpeed = 2f;

    [Header("Loot Settings")]
    [SerializeField] Item lootItem;
    [SerializeField] float lootItemPushForce = 5;
    [SerializeField] float maxHp = 50;

    private List<BehavaiorState> posibleStates = new List<BehavaiorState>();
    private List<Transform> filteredNeighbors = new List<Transform>();
    private List<Transform> neighbors = new List<Transform>();
    private Collider2D fishCollider;
    private Flock flock;
    private FishAnimator fishAnimator;
    private float squareNeighborRadius;
    private float avoidanceRadius;
    private float squareAvoidanceRadius;
    private Rigidbody2D rb2d;
    private HP hp;
    private BehavaiorState behavaiorState = BehavaiorState.Peaceful;
    private bool canMove = true;
    private bool canSwitchState = true;
    private float switchStateTimer = 0;
    private float threatTimer = 0;


    public Vector2 aPoint;
    public Vector2 bPoint;
    public Vector2 cPoint;
    public Vector2 dPoint;
    public Vector2 randomPoint;


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
    public float ThreatTimer
    {
        get { return threatTimer; }
        set { threatTimer = value; }
    }


    private void Awake()
    {
        fishAnimator = GetComponentInChildren<FishAnimator>();
        fishCollider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        hp = new HP(maxHp);
    }


    private void OnEnable()
    {
        hp.OnDead += Die;
        fishAnimator.OnStartedFading += DropLoot;
    }


    private void OnDisable()
    {
        hp.OnDead -= Die;
        fishAnimator.OnStartedFading -= DropLoot;
    }


    private void Start()
    {
        squareNeighborRadius = neighborRadius * neighborRadius;
        avoidanceRadius = neighborRadius * avoidanceRadiusMultiplier;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        InitStates();
    }


    private void FixedUpdate()
    {
        if (!canMove) return;

        switch (behavaiorState)
        {
            case BehavaiorState.Peaceful:
                Move(nonContextBehavior.CalculateMove(this), standartMoveSpeed);
                break;

            case BehavaiorState.GroupMove:
                Move(contextBehaviour.CalculateMove(this, neighbors, filteredNeighbors, flock), standartMoveSpeed);
                break;

            case BehavaiorState.Threat:
                Move(threatenBehavior.CalculateMove(this, Gnome.Instance.transform), maxMoveSpeed);
                break;

            case BehavaiorState.Hunting:
                Debug.Log("Hunting");
                break;

        }

        InvertSprite();

    }


    private void Update()
    {
        FindNeighbors();

        if (canSwitchState == false)
        {
            switchStateTimer += Time.deltaTime;
            if (switchStateTimer >= stateSwitchCooldown)
            {
                canSwitchState = true;
                switchStateTimer = 0;
            }
        }
    }


    private void Move(Vector2 moveDirection, float moveSpeed)
    {
        rb2d.velocity = moveDirection * moveSpeed;

        float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }


    private void InitStates()
    {
        if (contextBehaviour != null) posibleStates.Add(BehavaiorState.GroupMove);
        if (nonContextBehavior != null) posibleStates.Add(BehavaiorState.Peaceful);
        if (threatenBehavior != null) posibleStates.Add(BehavaiorState.Threat);
        if (huntingBehavior != null) posibleStates.Add(BehavaiorState.Hunting);
    }


    public void InitFlock(Flock flock)
    {
        this.flock = flock;
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


    private void Die()
    {
        canMove = false;
        rb2d.velocity = Vector2.zero;
        fishCollider.enabled = false;
    }


    private void DropLoot()
    {
        Item spawnedLootItem = Instantiate(lootItem, transform.position, Quaternion.identity);
        Vector2 randomDirection = new Vector2(UnityEngine.Random.Range(-1, 1f), UnityEngine.Random.Range(-1, 1f));
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

        if (filter != null)
        {
            filteredNeighbors = filter.GetFilteredAgents(this, neighbors);
        }
    }


    private bool HasState(BehavaiorState behavaiorState)
    {
        foreach (BehavaiorState state in posibleStates)
        {
            if (state == behavaiorState)
            {
                return true;
            }
        }
        return false;
    }


    public bool HasNeighbors()
    {
        return (neighbors.Count != 0 && filter == null) || (filteredNeighbors.Count != 0 && filter != null);
    }


    public void ChangeBehaviorState(BehavaiorState behavaiorState)
    {
        if (canSwitchState && HasState(behavaiorState))
        {
            this.behavaiorState = behavaiorState;
            canSwitchState = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out MelleWeapon weapon))
        {
            hp.TakeDamage(weapon.Damage);
            behavaiorState = BehavaiorState.Threat;
        }
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
