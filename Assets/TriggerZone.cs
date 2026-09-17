using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.instance;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            gameManager.BallLost(other.gameObject);
        }
    }
}
