using UnityEngine;
using TMPro;   // ✅ Required for TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager coinInstance;

    public int CoinsCount = 0;
    public TextMeshProUGUI coinText;  // ✅ Reference to the UI Text

    void Awake()
    {
        if (coinInstance == null)
        {
            coinInstance = this;
        }
        else if (coinInstance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCoinUI();  // ✅ Show initial value
    }

    public void IncrementCoin()
    {
        CoinsCount++;
        Debug.Log("Coins Collected: " + CoinsCount);
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "" + CoinsCount.ToString();
        }
    }
}
