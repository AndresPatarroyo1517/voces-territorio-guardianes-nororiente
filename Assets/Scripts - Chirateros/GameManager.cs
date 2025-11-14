using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int fishRequired = 3;
    private int fishKilled = 0;

    public GameObject[] objectsToActivate;
    public AudioSource victorySound;

    public TMP_Text fishCounter;
    public GameObject counterPanel;
    public ParticleSystem Particulas;

    // CHISAGUA
    public Animator chisaguaAnimator;
    public AudioSource chisaguaAudio;
    public Transform playerTransform;


    void Start()
    {
        foreach (var obj in objectsToActivate)
            obj.SetActive(false);
    }

    void OnEnable()
    {
        Pez.OnFishKilled += AddKill;
    }

    void OnDisable()
    {
        Pez.OnFishKilled -= AddKill;
    }

    void AddKill()
    {
        fishKilled++;
        UpdateCounter();

        if (fishKilled >= fishRequired)
            Win();
    }

    void UpdateCounter()
    {
        if (fishCounter != null)
            fishCounter.text = "Peces: " + fishKilled + " / " + fishRequired;
    }


    void Win()
    {
        foreach (var obj in objectsToActivate)
            obj.SetActive(true);

        if (victorySound != null)
            victorySound.Play();

        // ACTIVAR partículas por 5 segundos exactos
        if (Particulas != null)
        {
            Particulas.Play();
            StartCoroutine(StopParticlesAfterTime());
        }

        if (counterPanel != null)
            counterPanel.SetActive(false);

        StartCoroutine(PlayChisaguaFinal());
    }

    System.Collections.IEnumerator StopParticlesAfterTime()
    {
        yield return new WaitForSeconds(3f);

        if (Particulas != null)
            Particulas.Stop();
    }


    System.Collections.IEnumerator PlayChisaguaFinal()
    {
        // Espera al sonido de victoria (si existe)
        if (victorySound != null && victorySound.clip != null)
            yield return new WaitForSeconds(victorySound.clip.length);


        // Chisagua mira al jugador
        if (chisaguaAnimator != null && playerTransform != null)
        {
            Vector3 lookDir = playerTransform.position - chisaguaAnimator.transform.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
                chisaguaAnimator.transform.rotation = Quaternion.LookRotation(lookDir);
        }

        // ACTIVAR animación (bool anim1 = true)
        if (chisaguaAnimator != null)
            chisaguaAnimator.SetBool("anim1", true);

        // Reproducir audio
        if (chisaguaAudio != null && chisaguaAudio.clip != null)
        {
            chisaguaAudio.Play();

            // Esperar a que termine el audio
            yield return new WaitForSeconds(chisaguaAudio.clip.length);
        }

        // 🔥 DESACTIVAR animación (anim1 = false)
        if (chisaguaAnimator != null)
            chisaguaAnimator.SetBool("anim1", false);
    }
}
