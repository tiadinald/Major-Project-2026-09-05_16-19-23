using UnityEngine;

public class ButterflyFlight : MonoBehaviour
{
    public float radius = 3f;
    public float speed = 1f;
    public float heightBobAmount = 0.5f;
    public float heightBobSpeed = 2f;

    private Vector3 startPos;
    private float randomOffset;

    void Start()
    {
        startPos = transform.position;
        // Randomize so butterflies don't all move in sync
        randomOffset = Random.Range(0f, 100f);
        speed = Random.Range(speed * 0.7f, speed * 1.3f);
        radius = Random.Range(radius * 0.7f, radius * 1.3f);
    }

    void Update()
    {
        float t = Time.time * speed + randomOffset;

        float x = Mathf.Sin(t) * radius;
        float z = Mathf.Cos(t * 0.7f) * radius;
        float y = Mathf.Sin(Time.time * heightBobSpeed + randomOffset) * heightBobAmount;

        Vector3 newPos = startPos + new Vector3(x, y, z);

        // Face the direction of movement
        Vector3 direction = newPos - transform.position;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        transform.position = newPos;
    }
}
