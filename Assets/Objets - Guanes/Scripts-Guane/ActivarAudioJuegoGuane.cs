using System.Collections.Generic;
using UnityEngine;

public class ActivarAudioJuegoGuane : MonoBehaviour
{
    [Header("Key Items Settings")]
    public Transform[] keyItemSockets; 
    public string[] validKeyTags; 
    public float detectionRadius = 0.2f;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip activationClip;

    private bool audioPlayed = false;

    private void Start()
    {
        InvokeRepeating("CheckKeyItems", 0f, 0.5f);
    }

    private void CheckKeyItems()
    {
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

        if (allSocketsHaveItems && !audioPlayed)
        {
            PlayActivationAudio();
        }
    }

    private bool IsValidKeyItem(GameObject item)
    {
        if (validKeyTags != null && validKeyTags.Length > 0)
        {
            foreach (string tag in validKeyTags)
            {
                if (item.CompareTag(tag))
                    return true;
            }
        }
        return false;
    }

    private void PlayActivationAudio()
    {
        if (audioSource != null && activationClip != null)
        {
            audioSource.PlayOneShot(activationClip);
            audioPlayed = true;
        }
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
