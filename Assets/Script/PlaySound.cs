using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public AudioSource sourcePlay;
    AudioSource buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playThisSoundEffect()
    {
        sourcePlay.Play();
    }
    private void OnMouseDown()
    {
        buttonSound.PlayOneShot(buttonSound.clip);
    }
}
