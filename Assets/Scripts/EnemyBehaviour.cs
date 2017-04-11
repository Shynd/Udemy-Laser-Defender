using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject enemyLaserPrefab;
    public float laserSpeed = 10f;
    public float health = 200;
    public float shotsPerSecond = 0.5f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var projectile = collider.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            health -= projectile.GetDamage();
            projectile.Hit();

            if (health <= 0)
            {
                Destroy(gameObject);
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
        var startPosition = transform.position + new Vector3(0, -1.0f);
        var laser = Instantiate(enemyLaserPrefab, startPosition, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }
}
