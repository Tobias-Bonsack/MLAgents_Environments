using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEngine : MonoBehaviour
{
    [SerializeField] DinoEventManager manager;
    [SerializeField] TMPro.TextMeshProUGUI text;
    void Start()
    {
        manager.showPointsEvent += SetPoints;
    }


    private void SetPoints(int points)
    {
        text.SetText("" + points);

    }

    private void OnDestroy()
    {
        manager.showPointsEvent -= SetPoints;
    }
}
