using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public void Play()
    {
        FindObjectOfType<AudioManager>().Play("Select Sound");
    }
}
