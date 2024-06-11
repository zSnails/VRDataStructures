using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Fade : MonoBehaviour
{

    private Material mat;
    
    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    public void FadeIn()
    {
        StartCoroutine(nameof(fadeIn));
    }

    private IEnumerator fadeIn()
    {
        var c = mat.color;
        c.a = 0;
        mat.color = c;

        for (var i = 0.0f; i < 1.0f; i += 0.05f)
        {
            c.a = i;
            mat.color = c;
            yield return new WaitForSeconds(0.01f);
        }
        
        c.a = 1.0f;
        mat.color = c;
    }

    public async Task FadeOut()
    {
        var c = mat.color;
        c.a = 1.0f;

        for (var i = 1f; i > 0f; i -= 0.05f)
        {
            c.a = i;
            mat.color = c;
            await WaitForSeconds(0.01f);
        }
        
        c.a = 0.0f;
        mat.color = c;
    }

    public static async Task WaitForSeconds(float delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
    }
            
}