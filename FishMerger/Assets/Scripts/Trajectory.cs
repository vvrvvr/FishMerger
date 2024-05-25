using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject dotParent;
    [SerializeField] private int dotsNumber;
    [SerializeField] private float dotSpacing;
    [SerializeField] [Range(0.05f, 0.1f)] private float dotMinScale;
    [SerializeField] [Range(0.3f, 0.8f)] private float dotMaxScale;

    private Transform[] dotsList;
    private Vector3 pos;
    private float timeStamp;

    void Start()
    {
        Hide();
        PrepareDots();
    }
    private void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;
        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;
        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if(scale > dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
    }

    public void UpdateDots(Vector3 ballPos, Vector3 forceApplied)
    {
        timeStamp = dotSpacing;
        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics.gravity.magnitude * timeStamp * timeStamp) / 2f;
            pos.z = ballPos.z;
            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }

    }

    public void Show()
    {
        dotParent.SetActive(true);
    }

    public void Hide()
    {
        dotParent.SetActive(false);
    }
}
