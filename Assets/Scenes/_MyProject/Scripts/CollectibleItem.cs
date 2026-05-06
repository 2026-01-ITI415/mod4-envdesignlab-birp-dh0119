using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int value = 1;
    public float rotationSpeed = 90f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.2f;

    private Vector3 startPosition;
    private GameManager gameManager;

    void Start()
    {
        startPosition = transform.position;
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.AddCollectible(value);
            }

            Destroy(gameObject);
        }
    }
}