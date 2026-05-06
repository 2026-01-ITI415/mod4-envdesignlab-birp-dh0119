using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int value = 1;
    public float rotationSpeed = 90f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate so the collectible looks alive.
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Gently move up and down.
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected item: " + gameObject.name);

            // Later we will connect this to score and UI.
            Destroy(gameObject);
        }
    }
}
