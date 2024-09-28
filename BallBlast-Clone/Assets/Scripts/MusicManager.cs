using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // if (instance != null && instance != this)
        // {
        //     Destroy(this.gameObject);
        //     return;
        // }
        // else
        // {
        //     instance = this;
        // }

        // DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Música está tocando.");

            audioSource.volume = 1.0f;
        }
        else
        {
            Debug.LogError("AudioSource não encontrado no GameObject.");
        }
    }
}
