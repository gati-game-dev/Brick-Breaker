using UnityEngine;

public class PowerUpWidePaddle : MonoBehaviour
{
    public float speed = 4f;
    private PaddleMovement paddle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paddle = FindAnyObjectByType<PaddleMovement>();
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
            paddle.WidePaddlePowerUp();
            Destroy(gameObject);
        }
    }
}
