using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMagnet : MonoBehaviour
{
    [Header("Magnet Settings")]
    public Transform magnetPoint;
    public float pullSpeed = 15f;
    public float magnetDuration = 20f;      // 1 minute
    public float zTriggerDistance = 2f;

    [Header("UI")]
    public Image magnetImage;   // 🔥 drag your UI image here

    private bool magnetActive = false;
    private Coroutine timerRoutine;
    private float magnetTimer;

    void Start()
    {
        if (magnetImage != null)
            magnetImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!magnetActive) return;

        // 🔹 Update UI fill
        magnetTimer -= Time.deltaTime;
        magnetImage.fillAmount = magnetTimer / magnetDuration;

        if (magnetTimer <= 0f)
        {
            DeactivateMagnet();
            return;
        }

        // Find coins
        Coin[] coins = FindObjectsOfType<Coin>();

        foreach (Coin coin in coins)
        {
            if (coin.IsCollected) continue;

            float zDiff = Mathf.Abs(transform.position.z - coin.transform.position.z);

            if (zDiff <= zTriggerDistance)
            {
                coin.MoveToward(magnetPoint.position, pullSpeed);
            }
        }
    }

    public void ActivateMagnet()
    {
        Debug.Log("Magnet Activated");

        magnetActive = true;
        magnetTimer = magnetDuration;

        if (magnetImage != null)
        {
            magnetImage.gameObject.SetActive(true);
            magnetImage.fillAmount = 1f;
        }
    }

    private void DeactivateMagnet()
    {
        magnetActive = false;

        if (magnetImage != null)
            magnetImage.gameObject.SetActive(false);

        Debug.Log("Magnet Finished");
    }
}
