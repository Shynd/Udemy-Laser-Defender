using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float padding = 1f;

    private float xMin;
    private float xMax;

    void Start()
    {
        var distance = transform.position.z - Camera.main.transform.position.z;
        var leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmost.x + padding;
        xMax = rightmost.x - padding;
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

        // Restrict the player to the game space
        var newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
