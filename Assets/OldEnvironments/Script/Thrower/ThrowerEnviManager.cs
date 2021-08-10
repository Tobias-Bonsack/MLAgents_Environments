using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerEnviManager : MonoBehaviour
{

    [SerializeField] GameObject[] texts;
    [SerializeField] Material[] materials;

    public event ResetPositionDelegate resetPositionEvent;
    public event ShowPointsDelegate showPointsEvent;
    public event ShowEndStatusDelegate showEndStatusEvent;

    private void Start()
    {
        showPointsEvent += ShowPoints;
    }

    public void StartResetPositionEvent()
    {
        resetPositionEvent();
    }

    public void StartShowPointsEvent(int points)
    {
        showPointsEvent(points);
    }

    public void StartShowEndStatusEvent(int index) {
        showEndStatusEvent(materials[index]);
    }

    private void ShowPoints(int points)
    {
        foreach (GameObject text in texts)
        {
            text.GetComponent<TMPro.TextMeshProUGUI>().SetText("" + points);
        }
    }



}
