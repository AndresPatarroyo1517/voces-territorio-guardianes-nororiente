using System.Collections.Generic;
using UnityEngine;

public class ControllerEstatua : MonoBehaviour
{
    [Header("Key Items Settings")]
    public Transform[] keyItemSockets;
    public string[] validKeyTags;
    public float detectionRadius = 0.2f;

    [Header("Objects To Activate")]
    public GameObject estatua;
    public GameObject mascara;

    [Header("Effects for Mask")]
    public ParticleSystem mascaraParticles;
    public AudioSource mascaraAudio;       // Primer audio
    public AudioSource segundoAudio;       // Segundo audio

    private bool alreadyActivated = false;
    private bool secondAudioPlayed = false;

    private void Start()
    {
        InvokeRepeating("CheckKeyItems", 0f, 0.5f);
    }

    private void Update()
    {
        // Cuando termine el primer audio, reproducir el segundo
        if (alreadyActivated && mascaraAudio != null && segundoAudio != null)
        {
            if (!mascaraAudio.isPlaying && !secondAudioPlayed)
            {
                segundoAudio.Play();
                secondAudioPlayed = true;
            }
        }
    }

    private void CheckKeyItems()
    {
        if (alreadyActivated)
            return;

        if (keyItemSockets == null || keyItemSockets.Length == 0)
            return;

        bool allSocketsHaveItems = true;

        foreach (Transform socket in keyItemSockets)
        {
            bool socketHasValidItem = false;

            Collider[] nearbyObjects = Physics.OverlapSphere(socket.position, detectionRadius);

            foreach (Collider col in nearbyObjects)
            {
                if (IsValidKeyItem(col.gameObject))
                {
                    socketHasValidItem = true;
                    break;
                }
            }

            if (!socketHasValidItem)
            {
                allSocketsHaveItems = false;
                break;
            }
        }

        if (allSocketsHaveItems)
        {
            ActivarObjetos();
        }
    }

    private bool IsValidKeyItem(GameObject item)
    {
        foreach (string tag in validKeyTags)
        {
            if (item.CompareTag(tag))
                return true;
        }
        return false;
    }

    private void ActivarObjetos()
    {
        alreadyActivated = true;

        // 1. Activar estatua
        if (estatua != null)
            estatua.SetActive(true);

        // 2. Activar máscara
        if (mascara != null)
            mascara.SetActive(true);

        // 3. Activar partículas
        if (mascaraParticles != null)
            mascaraParticles.Play();

        // 4. Reproducir audio 1
        if (mascaraAudio != null)
            mascaraAudio.Play();
    }

    private void OnDrawGizmosSelected()
    {
        if (keyItemSockets != null)
        {
            Gizmos.color = Color.yellow;
            foreach (Transform socket in keyItemSockets)
            {
                if (socket != null)
                    Gizmos.DrawWireSphere(socket.position, detectionRadius);
            }
        }
    }
}
