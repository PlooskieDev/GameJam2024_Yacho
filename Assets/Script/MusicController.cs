using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource asource;

    public AudioClip aclip1;
    public AudioClip aclip2;
    public AudioClip aclip3;
    public AudioClip aclip4;

    public bool pos1 = true;
    public bool pos2 = true;
    public bool pos3 = true;
    public bool pos4 = true;
    
    // Z18 - 1
    // Z32 - 2
    // Z60 - 3
    // Z82 - 4
    void Update()
    {
        if (transform.position.z >= 18 && pos1)
        {
            asource.clip = aclip1;
            asource.Play();
            pos1 = false;
        }
        if (transform.position.z >= 32 && pos2)
        {
            asource.clip = aclip2;
            asource.Play();
            pos2 = false;
        }
        if (transform.position.z >= 60 && pos3)
        {
            asource.clip = aclip3;
            asource.Play();
            pos3 = false;
        }
        if (transform.position.z >= 82 && pos4)
        {
            asource.clip = aclip4;
            asource.Play();
            pos4 = false;
        }
    }
}
