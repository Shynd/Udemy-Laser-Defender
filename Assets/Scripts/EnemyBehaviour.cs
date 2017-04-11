using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject enemyLaserPrefab;
    public float laserSpeed = 10f;
    public float health = 200;
    public float shotsPerSecond = 0.5f;
    public int scoreValue = 150;
    public AudioClip laserAudioClip;
    public AudioClip deathAudioClip;

    private ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
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
                Die();
            }
        }
    }

    void Update()
    {
        var probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        AudioSource.PlayClipAtPoint(laserAudioClip, transform.position);

        var laser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathAudioClip, transform.position);
        scoreKeeper.Score(scoreValue);
        Destroy(gameObject);
    }
}
