using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    void Start()
    {
        Debug.Log("CoinCollision script attached to " + gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // ✅ Use static instance of CoinManager
            if (CoinManager.coinInstance != null)
            {
                CoinManager.coinInstance.IncrementCoin();
            }

            Destroy(gameObject); // ✅ Destroy coin after collection
        }
    }
}
