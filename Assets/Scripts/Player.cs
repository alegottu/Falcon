using UnityEngine;

public class Player : MonoBehaviour, IGivesDamage
{
    public bool nearGround = false;

    [SerializeField] private PlayerStats stats = null;
    [SerializeField] private Rigidbody2D rb = null;

    private Vector2 initMouse = Vector2.zero;
    private float timeSinceClick = 0;
    private bool countTime = false;
    private float timeMultiplier = 1;
    private Vector2 mousePos = Vector2.zero;
    private float swipeDistance = 0;
    private float distanceMultiplier = 1;
    private bool addFlight = false;
    private RaycastHit2D gustHit = new RaycastHit2D();

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Fly();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            initMouse = Input.mousePosition;
            countTime = true;
        }

        timeSinceClick = countTime ? timeSinceClick + Time.deltaTime : 0;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            timeMultiplier = Mathf.Clamp(timeSinceClick / stats.longestSwipe * stats.timeMultiplierBase, 1, stats.maxTimeMultiplier);

            swipeDistance = Vector2.Distance(initMouse, mousePos);
            distanceMultiplier = Mathf.Clamp(swipeDistance / stats.farthestSwipe * stats.distanceMultiplierBase, 1, stats.maxDistanceMultiplier);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            countTime = false;
        }

        mousePos = Input.mousePosition;
        addFlight = Input.GetKey(KeyCode.Mouse0) && swipeDistance >= stats.minSwipeDistance && timeSinceClick <= stats.longestSwipe;
    }

    private void Fly()
    {
        if (addFlight)
        {
            Vector2 flightDirection = -(mousePos - initMouse).normalized;
            if (nearGround && flightDirection.y < 0.25f)
            {
                return;
            }

            rb.AddForce(flightDirection * timeMultiplier * distanceMultiplier * stats.flightPower);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Rad2Deg * -Mathf.Atan2(flightDirection.x, flightDirection.y));

            Vector2 size = stats.gustSize * timeMultiplier * distanceMultiplier;
            if (Physics2D.BoxCast(transform.position, size, 0, Vector2.zero, size.x, stats.ignoreRaycast))
            {
                gustHit = Physics2D.BoxCast(transform.position, size, 0, Vector2.zero, size.x, stats.ignoreRaycast);
                gustHit.rigidbody.AddForce(nearGround ? 
                    Vector2.Perpendicular(flightDirection * stats.gustPower) * Mathf.Clamp(Mathf.RoundToInt(transform.position.x - gustHit.point.x), -1, 1) 
                    : -flightDirection * stats.gustPower);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            int damage = Mathf.RoundToInt(stats.attackPower * timeMultiplier * distanceMultiplier);
            damage = damage >= stats.minDamage ? damage : 0;
            health.TakeDamage(damage);

            rb.AddForce(Vector2.up * stats.bounceForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, stats.gustSize * timeMultiplier * distanceMultiplier);
    }

    public void GiveDamage(Health target)
    {
        target.TakeDamage(Mathf.RoundToInt(stats.attackPower * timeMultiplier * distanceMultiplier));
    }
}