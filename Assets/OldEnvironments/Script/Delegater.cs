using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ResetPositionDelegate();
public delegate void ShowEndStatusDelegate(Material material);
public delegate void ShowPointsDelegate(int points);
public delegate void SpawnObstacleDelegate();
public delegate void ChangeLifeDelegate(int currentLife);
public class Delegater : MonoBehaviour
{
}
