using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over!");
            // Here you can add more game over logic, like stopping the player movement, showing a game over screen, etc.
            Destroy(gameObject);
        }
    }
}
