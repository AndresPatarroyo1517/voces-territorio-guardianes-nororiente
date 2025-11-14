using UnityEngine;

public class Chisagua : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    [Header("UI del letrero")]
    public GameObject letrero; // ← TU LETRERO EN UI

    void Start()
    {
        // Asegurar que el letrero inicie oculto SIEMPRE
        if (letrero != null)
            letrero.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                if (animator != null)
                {
                    animator.SetBool("anim1", true);
                    Debug.Log("Animación activada: anim1 = true");
                }

                // Se espera lo que dura el audio
                Invoke("ResetAnimation", audioSource.clip.length);
            }
        }
    }

    void ResetAnimation()
    {
        animator.SetBool("anim1", false);
        Debug.Log("Animación finalizada: anim1 = false");

        // Mostrar el letrero justo cuando termina la animación de hablar
        MostrarLetrero();
    }

    void MostrarLetrero()
    {
        if (letrero != null)
        {
            letrero.SetActive(true);
            Debug.Log("Letrero mostrado por 5 segundos");

            Invoke("OcultarLetrero", 6f);
        }
    }

    void OcultarLetrero()
    {
        if (letrero != null)
        {
            letrero.SetActive(false);
            Debug.Log("Letrero ocultado");
        }
    }
}
