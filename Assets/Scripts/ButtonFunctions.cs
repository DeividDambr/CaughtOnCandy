using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] private AudioSource button;
    [SerializeField] private AudioClip hoverSound;

    public void HoverSound()
    {
        button.PlayOneShot(hoverSound);
    }
}
