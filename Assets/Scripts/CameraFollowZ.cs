using UnityEngine;

public class CameraFollowZ : MonoBehaviour
{
    public Transform player;      // Assign the player in Inspector
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Behind and above the player
    public float smoothSpeed = 5f; // How smoothly the camera follows

    void LateUpdate()
    {
        if (player != null)
        {
            // Desired camera position is always at a fixed offset from player
            Vector3 targetPosition = player.position + offset;

            // Smoothly interpolate to the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
