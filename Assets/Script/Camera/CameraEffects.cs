
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraEffects : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.2f;

    public Image flashImage;

    private Vector3 originalPosition;
    private float shakeTimer;

    void Awake()
    {
        originalPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        if (shakeTimer > 0)
        {
            Vector2 offset = Random.insideUnitCircle * shakeMagnitude;

            transform.localPosition = originalPosition +
                new Vector3(offset.x, offset.y, 0);

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }

    public void HitEffect()
    {
        shakeTimer = shakeDuration;

        StopAllCoroutines();
        StartCoroutine(Flash());
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
