using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(HideCanvas());
    }
    
    private IEnumerator HideCanvas() {
        yield return new WaitForSeconds(15f);
        canvas.enabled = false;
    }
}
