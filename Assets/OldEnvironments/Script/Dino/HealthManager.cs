using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] HealthEngine[] engines;
    [SerializeField] Material[] materials;
    [SerializeField] DinoEventManager manager;

    private void Start()
    {
        manager.changeLifeEvent += ChangeLifeMaterials;
    }

    private void ChangeLifeMaterials(int currentLife)
    {
        for (int i = 0; i < engines.Length; i++)
        {
            if (i < currentLife)
            {
                engines[i].ChangeMaterialTo(materials[0]);
            }
            else
            {
                engines[i].ChangeMaterialTo(materials[1]);
            }
        }
    }

    private void OnDestroy()
    {
        manager.changeLifeEvent -= ChangeLifeMaterials;
    }
}
