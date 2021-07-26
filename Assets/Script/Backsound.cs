using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Backsound : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClip;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource==null)
        {
            Debug.LogError("Audio Source for music background is NULL");
        }
        else
        {
            _audioSource.clip = audioClip[2];
            _audioSource.Play();
        }      
    }
    public void DropdownPlay(int val)
    {
        _audioSource.clip = audioClip[val];
        _audioSource.Play();
    }
}
