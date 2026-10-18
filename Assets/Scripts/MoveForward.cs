using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5f; // Speed in units per second

    void Update()
    {
        // Move the object forward (Z direction) every frame
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
