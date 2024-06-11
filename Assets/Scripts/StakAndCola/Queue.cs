using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Queue : MonoBehaviour
{
    // Start is called before the first frame update
    private Queue<GameObject> queue;
    public GameObject @base;
    [Range(0, 1)] public float blockDistance;
    public GameObject stackElement;
    public GameObject container;
    public Button dequeueButton;

    private void Awake()
    {
        queue = new Queue<GameObject>();
    }

    public void Enqueue()
    {
        float y;

        if (queue.Count == 0)
        {
            y = @base.transform.position.y + blockDistance * 3;
        }
        else
        {
            var last = queue.Last();
            y = last.transform.position.y + last.transform.lossyScale.y + blockDistance;
        }

        var z = transform.position.z;
        var x = transform.position.x;
        var cube = Instantiate(stackElement, new Vector3(x, y, z), Quaternion.identity);
        var fader = cube.GetComponent<Fade>();
        fader.FadeIn();
        cube.transform.parent = transform;

        queue.Enqueue(cube);
    }

    public async void Dequeue()
    {
        var could = queue.TryDequeue(out var got);
        if (!could)
            return;

        var fader = got.GetComponent<Fade>();
        await fader.FadeOut();

        Destroy(got);

        could = queue.TryPeek(out var next);
        if (!could)
            return;
        
        foreach (var o in queue) o.transform.parent = null;

        var pos = container.transform.position;
        pos.y = next.transform.position.y;// - blockDistance * 2;
        container.transform.position = pos;

        foreach (var o in queue) o.transform.parent = container.transform;

        var y = @base.transform.position.y + blockDistance*2;

        _cancellationTokenSource?.Cancel();

        if (_latestTask != null)
        {
            StopCoroutine(nameof(MoveY));
        }
        _latestTask = StartCoroutine(nameof(MoveY), y);
    }
    
    private Coroutine _latestTask = null;
    private CancellationTokenSource _cancellationTokenSource;

    public IEnumerator MoveY(float target)
    {
        var original = container.transform.position.y;
        for (var t = 0f; t < 1; t += 0.1f)
        {
            container.transform.position =
                new Vector3(transform.position.x, Mathf.Lerp(original, target, t), transform.position.z);
            yield return new WaitForSeconds(0.05f);
        }
    }
}