using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoEventManager : MonoBehaviour
{
    public event SpawnObstacleDelegate spawnObstacleEvent;
    public event ChangeLifeDelegate changeLifeEvent;

    public event ShowPointsDelegate showPointsEvent;


    public void StartSpawnObstacleEvent()
    {
        spawnObstacleEvent();
    }

    public void StartChangeLifeEvent(int currentLife)
    {
        changeLifeEvent(currentLife);
    }

    public void StartShowPointsEvent(int points)
    {
        showPointsEvent(points);
    }
}
