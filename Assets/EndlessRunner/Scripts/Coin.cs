using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool IsCollected { get; private set; } = false;
    private bool isMoving = false;
    private Vector3 target;
    private float speed;

    void Update()
    {
        if (isMoving && !IsCollected)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }

    public void MoveToward(Vector3 magnetPoint, float pullSpeed)
    {
        if (IsCollected) return;
        isMoving = true;
        target = magnetPoint;
        speed = pullSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsCollected)
        {
            IsCollected = true;
            // Add score, play sound, etc.
            Destroy(gameObject);
        }
    }
}
