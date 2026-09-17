using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameManager gameManager;
    public int brickLife = 1;
    private SpriteRenderer spriteRenderer;
    public Sprite damagedSprite;
    public Sprite veryDamagedSprite;
    void Start()
    {
        gameManager = GameManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            brickLife -= 1;
            if (brickLife > 0)
            {
                if (brickLife == 2)
                {
                    spriteRenderer.sprite = damagedSprite;
                }
                if (brickLife == 1)
                {
                    spriteRenderer.sprite = veryDamagedSprite;
                }
            }
            else
            {
                gameManager.UpdateScore();
                gameManager.CountBricks();
                Destroy(gameObject);

            }
        }
    }
}
