using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private AudioSource audioSource;

     void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
        OnDestroy();
    }
    private void OnDestroy()
    {
        Destroy(gameObject, 0.5f);// Hủy sau 0.5s 
    }
}
