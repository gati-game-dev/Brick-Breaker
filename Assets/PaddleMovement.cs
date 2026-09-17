using UnityEngine;
using System.Collections;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 14f;
    public string horizontal = "Horizontal";
    private Rigidbody2D rb;
    private Vector3 originalScale;
    private Coroutine widePaddleCoroutine;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        float input = Input.GetAxis(horizontal);
        rb.linearVelocity = new Vector2(input*speed, 0);
    }
    public void WidePaddlePowerUp()
    {
        if (widePaddleCoroutine != null)
        {
            StopCoroutine(widePaddleCoroutine);    
        }                                        // stop any timer that is running 

        transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y * 1.5f,
            originalScale.z                
        );                               // increase paddle size 

        widePaddleCoroutine = StartCoroutine(WidePaddleTimer());       // start the timer
    }
    IEnumerator WidePaddleTimer()
    {
        yield return new WaitForSeconds(7f);        // pause this function for 7 sec

        transform.localScale = originalScale;       // restore original paddle size

        widePaddleCoroutine = null;            // no timers running now 
    }
}
