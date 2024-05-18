using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    private CanvasGroup mat;

    private void Awake()
    {
        mat = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        StartCoroutine(nameof(fadeIn));
    }

    private IEnumerator fadeIn()
    {
        mat.interactable = true;
        var c = mat.alpha;
        mat.alpha = c;

        for (var i = 0.0f; i < 1.0f; i += 0.05f)
        {
            c = i;
            mat.alpha = c;
            yield return new WaitForSeconds(0.01f);
        }

        c = 1.0f;
        mat.alpha = c;
    }
    
    public void FadeOut()
    {
        StartCoroutine(nameof(fadeOut));
    }

    private IEnumerator fadeOut()
    {
        mat.interactable = false;
        var c = mat.alpha;
        c = 1.0f;

        for (var i = 1f; i > 0f; i -= 0.05f)
        {
            c = i;
            mat.alpha = c;
            yield return new WaitForSeconds(0.01f);
        }

        c = 0.0f;
        mat.alpha = c;
    }


}
