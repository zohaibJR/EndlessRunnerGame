using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool moveLeft = false;
    private bool moveRight = false;

    void Update()
    {
        Vector3 pos = transform.position;

        if (moveLeft)
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        if (moveRight)
        {
            pos.x += moveSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }

    // These functions will be called by the UI buttons
    public void OnLeftButtonDown()
    {
        moveLeft = true;
    }

    public void OnLeftButtonUp()
    {
        moveLeft = false;
    }

    public void OnRightButtonDown()
    {
        moveRight = true;
    }

    public void OnRightButtonUp()
    {
        moveRight = false;
    }
}
