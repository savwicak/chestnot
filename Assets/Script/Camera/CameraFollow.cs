
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothness = 2f;

    [Header("Camera Shake")]
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.15f;

    [Header("Camera Flash")]
    public Image flashImage;

    private Vector3 currentOffset;
    private Coroutine shakeCoroutine;

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(
            player.position.x,
            player.position.y,
            -10f
        );

        Vector3 followPos = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothness * Time.deltaTime
        );

        transform.position = followPos + currentOffset;
    }

    public void HitEffect()
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(Shake());
        StartCoroutine(Flash());
    }

    IEnumerator Shake()
    {
        float timer = 0;

        while (timer < shakeDuration)
        {
            currentOffset = new Vector3(
                Random.Range(-1f, 1f) * shakeMagnitude,
                Random.Range(-1f, 1f) * shakeMagnitude,
                0
            );

            timer += Time.deltaTime;
            yield return null;
        }

        currentOffset = Vector3.zero;
        shakeCoroutine = null;
    }

    IEnumerator Flash()
    {
        if (flashImage == null)
            yield break;

        flashImage.color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(0.08f);

        flashImage.color = new Color(1, 1, 1, 0);
    }
}
