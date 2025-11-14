using UnityEngine;

public class SpearTip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            Pez pez = other.GetComponent<Pez>();
            if (pez != null)
            {
                pez.Kill();   // <-- Este método dispara el evento y el GameManager escucha
            }
        }
    }
}
