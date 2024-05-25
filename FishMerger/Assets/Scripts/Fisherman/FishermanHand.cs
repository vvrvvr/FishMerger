using UnityEngine;
using System.Collections;

public class FishermanHand : MonoBehaviour
{
    public float handYOffset;
    public Transform objectToFollow;
    public Transform hand;
    private Coroutine catchFishCoroutine;
    private float initialHandYOffset;
    public float fishHeight = 2.5f;
    public float catchTime = 0.2f;

    void Start()
    {
        if (objectToFollow == null)
        {
            Debug.LogError("objectToFollow не назначен. Пожалуйста, назначьте объект в инспекторе.");
        }

        initialHandYOffset = handYOffset;
    }

    void Update()
    {
        if (objectToFollow != null)
        {
            Vector3 newPosition = hand.position;
            newPosition.y = objectToFollow.position.y + handYOffset;
            hand.position = newPosition;
        }
    }

    public void CatchFish()
    {
        if (catchFishCoroutine != null)
        {
            StopCoroutine(catchFishCoroutine);
        }
        catchFishCoroutine = StartCoroutine(CatchFishCoroutine(fishHeight, catchTime));
    }

    private IEnumerator CatchFishCoroutine(float fishHeight, float catchTime)
    {
        float elapsedTime = 0f;
        float startYOffset = handYOffset;

        while (elapsedTime < catchTime)
        {
            handYOffset = Mathf.Lerp(startYOffset, fishHeight, elapsedTime / catchTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        handYOffset = fishHeight;
    }

    public void ResetHandYOffset()
    {
        if (catchFishCoroutine != null)
        {
            StopCoroutine(catchFishCoroutine);
        }
        handYOffset = initialHandYOffset;
    }
}