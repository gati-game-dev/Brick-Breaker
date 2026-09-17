using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovement : MonoBehaviour
{
    public float speed = 7f;
    // public float speedIncrease = 0.2f;
    private Rigidbody2D rb;
    private PaddleMovement paddle;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        paddle = FindAnyObjectByType<PaddleMovement>();
    }

    void Update()
    {
        
    }

    public void SetRandomVelocity()
    {
        float angle;

        angle = Random.Range(30f, 150f);
        float radianAngle = angle * Mathf.Deg2Rad;
        float x = math.cos(radianAngle);
        float y = math.sin(radianAngle);
        Vector2 velocity = new Vector2(x, y) * speed;
        rb.linearVelocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 direction = rb.linearVelocity.normalized;

        // Prevent the ball from becoming almost completely vertical
        if (Mathf.Abs(direction.x) < 0.3f)
        {
            direction.x = direction.x >= 0 ? 0.3f : -0.3f;
        }

        // Prevent the ball from becoming almost completely horizontal
        if (Mathf.Abs(direction.y) < 0.3f)
        {
            direction.y = direction.y >= 0 ? 0.3f : -0.3f;
        }

        // Normalize again so we keep only the direction
        direction = direction.normalized;

        rb.linearVelocity = direction * speed;

    }
}
