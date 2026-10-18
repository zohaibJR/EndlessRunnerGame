using UnityEngine;

public class MagnetPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMagnet pm = other.GetComponent<PlayerMagnet>();
            if (pm != null) pm.ActivateMagnet();
            Destroy(gameObject);
        }
    }
}
