using UnityEngine;
using System.Collections;

public class CarCollision : MonoBehaviour
{
    public GameObject GameOverPanel;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Log("Car Collision Script Started");
        GameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Player Dead!");
            isDead = true;

            // Trigger death animation
            animator.SetTrigger("Dead");

            // Start coroutine to wait 1.78 seconds before showing GameOver
            StartCoroutine(ShowGameOverAfterDelay(1.78f));
        }
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Death animation finished. Showing GameOver panel.");
        GameOverPanel.SetActive(true);
    }
}
