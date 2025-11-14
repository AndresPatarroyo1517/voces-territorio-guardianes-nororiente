using UnityEngine;

public class Pez : MonoBehaviour
{
    public delegate void FishKilled();
    public static event FishKilled OnFishKilled;

    public void Kill()
    {
        // Desactivar pez cuando lo cazan
        gameObject.SetActive(false);

        OnFishKilled?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spear"))
        {
            Kill();
        }
    }
}
