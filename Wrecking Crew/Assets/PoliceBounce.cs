using UnityEngine;

public class PoliceBounce : MonoBehaviour
{

    [SerializeField] float bounceStrength = 5000.0f;
    private GameLogic gameLogic;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && GameLogic.grace <= 0)
        {
            gameLogic.AddTime(-10);
            Vector2 dir = (collision.gameObject.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = dir * bounceStrength;
            GameLogic.grace = 2;
        }
    }
}
