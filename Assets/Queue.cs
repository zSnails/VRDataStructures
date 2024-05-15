using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
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

    private void Start()
    {
        this.queue = new Queue<GameObject>();
    }

    public void Enqueue()
    {
        float y;

        if (queue.Count == 0)
        {
            y = @base.transform.position.y + blockDistance*3;
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
        dequeueButton.enabled = false;
        var could = queue.TryDequeue(out var got);
        if (!could) return;
        var fader = got.GetComponent<Fade>();
        
        await fader.FadeOut();
        
        Destroy(got);
        
        foreach (var o in queue)
        {
            o.transform.parent = null;
        }
        
        could = queue.TryPeek(out var next);
        if (!could)
        {
            dequeueButton.enabled = true;
            return;
        }
        
        var pos = container.transform.position;
        pos.y = next.transform.position.y - blockDistance*2;
        container.transform.position = pos;
        
        foreach (var o in queue)
        {
            o.transform.parent = container.transform;
        }
        var y = @base.transform.position.y;
        await MoveY(container, y);
        dequeueButton.enabled = true;
    }

    private async Task MoveY(GameObject obj, float target)
    {
        var original = obj.transform.position.y;
        for (var t = 0f; t < 1; t += 0.1f)
        {
            obj.transform.position = new Vector3(transform.position.x, Mathf.Lerp(original, target, t), transform.position.z);
            await Fade.WaitForSeconds(0.05f);
        }
    }
}
