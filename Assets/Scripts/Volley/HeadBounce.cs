using UnityEngine;

public class HeadBounce : MonoBehaviour
{
    public float bounceForce = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("HeadBounce triggered with: " + other.name);
            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                // Reset Y velocity before adding upward force
                Vector3 vel = ballRb.linearVelocity;
                vel.y = 0;
                ballRb.linearVelocity = vel;

                // Apply upward force
                ballRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
