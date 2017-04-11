using UnityEngine;

public class FormationController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float enemySpeed = 5f;
    public float padding = 1f;
    public float spawnDelay = 0.5f;

    private bool movingRight;
    private float xMin;
    private float xMax;

    void Start()
    {
        var distance = transform.position.z - Camera.main.transform.position.z;
        var leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;

        SpawnUntillFull();
    }

    public void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            var enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(child);
        }
    }

    void SpawnUntillFull()
    {
        var nextPos = NextFreePosition();
        if (nextPos)
        {
            var enemy = Instantiate(enemyPrefab, nextPos.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(nextPos);

            Invoke("SpawnUntillFull", spawnDelay);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * enemySpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * enemySpeed * Time.deltaTime;
        }

        var rightEdge = transform.position.x + 0.5 * width;
        var leftEdge = transform.position.x - 0.5 * width;

        if (leftEdge <= xMin)
        {
            movingRight = true;
        }
        else if (rightEdge >= xMax)
        {
            movingRight = false;
        }

        if (AllMembersDead())
        {
            SpawnUntillFull();
        }
    }

    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }

        return null;
    }
}