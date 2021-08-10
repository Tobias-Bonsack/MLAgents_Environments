using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEngine : MonoBehaviour
{
    [SerializeField] MeshRenderer mR;

    public void ChangeMaterialTo(Material m)
    {
        mR.material = m;
    }
}
