using UnityEngine;

public class PowerUpThreeBalls : MonoBehaviour
{
    public float speed = 4f;
    private PaddleMovement paddle;
    private GameManager gameManager;
    void Start()
    {
        paddle = FindAnyObjectByType<PaddleMovement>();
        gameManager = GameManager.instance;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down*speed*Time.deltaTime;
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            GameManager.instance.ActivateThreeBallPowerUp();
            Destroy(gameObject);
        }
    }
}
