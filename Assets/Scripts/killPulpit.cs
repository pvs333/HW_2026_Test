using UnityEngine;
using System.Collections;

public class killPulpit : MonoBehaviour
{
    [SerializeField] private bool first;
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakePositionAmount = 0.08f;
    [SerializeField] private float shakeRotationAmount = 2f;
    private Animator anim;
    public bool isDestroyed = false;
    float minTime, maxTime, desTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        minTime = gameManager.GetMinPulpitDestroyTime();
        maxTime = gameManager.GetMaxPulpitDestroyTime();
        desTime = Random.Range(minTime, maxTime);
        if (first) desTime = 5f;
        StartCoroutine(DestroyPulpit(desTime));
    }

    IEnumerator DestroyPulpit(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(ShakeBeforeBreak());
        anim.SetTrigger("break");
        yield return new WaitForSeconds(1f);
        isDestroyed = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    IEnumerator ShakeBeforeBreak()
    {
        Vector3 originalPosition = transform.localPosition;
        Quaternion originalRotation = transform.localRotation;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float progress = elapsed / shakeDuration;
            float intensity = progress * progress;
            float frequency = Mathf.Lerp(2f, 14f, progress);
            float time = elapsed * frequency;

            transform.localPosition = originalPosition + new Vector3(
                Mathf.Sin(time * 1.7f) * shakePositionAmount * intensity,
                Mathf.Sin(time * 2.1f) * shakePositionAmount * intensity,
                Mathf.Sin(time * 1.3f) * shakePositionAmount * intensity);
            transform.localRotation = originalRotation * Quaternion.Euler(
                Mathf.Sin(time * 1.9f) * shakeRotationAmount * intensity,
                Mathf.Sin(time * 1.5f) * shakeRotationAmount * intensity,
                Mathf.Sin(time * 2.3f) * shakeRotationAmount * intensity);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
    }
}
