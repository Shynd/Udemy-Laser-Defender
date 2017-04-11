using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject laserPrefab;
    public float health = 1000f;
    public float playerSpeed = 10f;
    public float projectileSpeed = 15f;
    public float firingRate = 0.25f;
    public float padding = 1f;

    private float xMin;
    private float xMax;
    private ScoreKeeper scoreKeeper;

    void Start()
    {
        var distance = transform.position.z - Camera.main.transform.position.z;
        var leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;

        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }

        // Instantiate laser obj
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.0001f, firingRate);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        // Restrict the player to the game space
        var newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Fire()
    {
        var startPosition = transform.position + new Vector3(0, 1.0f);
        var laser = Instantiate(laserPrefab, startPosition, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var projectile = collider.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            health -= projectile.GetDamage();
            projectile.Hit();

            if (health <= 0)
            {
                scoreKeeper.Reset();
                Destroy(gameObject);
            }
        }
    }
}
