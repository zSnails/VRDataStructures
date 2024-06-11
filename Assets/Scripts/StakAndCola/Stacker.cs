using System.Collections.Generic;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    public GameObject stackElement;
    [Range(0, 1)] public float blockDistance;

    public GameObject @base;

    private Stack<GameObject> stack;

    // Start is called before the first frame update
    private void Start()
    {
        stack = new Stack<GameObject>();
    }

    public void Push()
    {
        var x = transform.position.x;
        float y;

        Debug.Log(blockDistance);
        if (stack.Count == 0)
        {
            y = @base.transform.position.y;
        }
        else
        {
            var top = stack.Peek();
            y = top.transform.position.y + top.transform.lossyScale.y + blockDistance;
        }

        var z = transform.position.z;
        var cube = Instantiate(stackElement, new Vector3(x, y, z), Quaternion.identity);
        var fader = cube.GetComponent<Fade>();
        fader.FadeIn();
        cube.transform.parent = transform;

        stack.Push(cube);
    }

    public async void Pop()
    {
        var could = stack.TryPop(out var got);
        if (!could) return;
        var fader = got.GetComponent<Fade>();
        await fader.FadeOut();
        Destroy(got);
    }
}