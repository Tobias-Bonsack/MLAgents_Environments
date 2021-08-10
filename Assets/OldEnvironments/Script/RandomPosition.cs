using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] BTFEnviManager manager;
    [SerializeField] ThrowerEnviManager throwerEnviManager;

    Vector3 origin;
    [Header("Randomizer")]
    [SerializeField] float radiusX;
    [SerializeField] float radiusZ;
    [SerializeField] float startX;
    [SerializeField] float startZ;

    private void Start()
    {
        if (manager != null)
        {
            manager.resetPositionEvent += ResetTransform;
        }
        else if (throwerEnviManager != null)
        {
            throwerEnviManager.resetPositionEvent += ResetTransform;
        }
        origin = resetTransform.localPosition;
    }

    public void ResetTransform()
    {
        CharacterControllerState(false);

        float x = Random.Range(-radiusX, radiusX);
        x += (x > 0 ? startX : -startX);
        float z = Random.Range(-radiusZ, radiusZ);
        z += (z > 0 ? startZ : -startZ);
        resetTransform.localPosition = origin + new Vector3(x, 0f, z);
        CharacterControllerState(true);
    }

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.resetPositionEvent -= ResetTransform;
        }
        else if (throwerEnviManager != null)
        {
            throwerEnviManager.resetPositionEvent -= ResetTransform;
        }
    }

    private void CharacterControllerState(bool state)
    {
        if (TryGetComponent<CharacterController>(out CharacterController cc))
        {
            cc.enabled = state;
        }
    }

    public void SetValues(int newRX, int newRZ, int newSX, int newSZ)
    {
        radiusX = newRX;
        radiusZ = newRZ;
        startX = newSX;
        startZ = newSZ;
    }

    public void SetOrigin(Vector3 vector)
    {
        this.origin = vector;
    }
}
