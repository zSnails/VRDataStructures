using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float speed = 90f;
    public GameObject mainCamera;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = mainCamera.transform.rotation;
    }
    public void TurnRight()
    {
        StartCoroutine(RotateCamera(65));
    }

    public void ResetCameraRotation()
    {
        StartCoroutine(RotateCameraToInitial());
    }

    private IEnumerator RotateCamera(float angle)
    {
        Quaternion startRotation = mainCamera.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed);
            elapsed += Time.deltaTime * speed / angle;
            yield return null;
        }

        mainCamera.transform.rotation = endRotation;
    }

    private IEnumerator RotateCameraToInitial()
    {
        Quaternion startRotation = mainCamera.transform.rotation;
        float angle = Quaternion.Angle(startRotation, initialRotation);
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, initialRotation, elapsed);
            elapsed += Time.deltaTime * speed / angle;
            yield return null;
        }

        mainCamera.transform.rotation = initialRotation;
    }


}
