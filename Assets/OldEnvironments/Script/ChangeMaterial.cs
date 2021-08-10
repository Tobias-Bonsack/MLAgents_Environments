using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] BTFEnviManager manager;
    [SerializeField] ThrowerEnviManager throwerEnviManager;

    [Header("Renderer")]
    [SerializeField] MeshRenderer mRenderer;
    Material originalMaterial;
    private void Awake()
    {
        if (manager != null)
        {
            manager.showEndStatusEvent += NewMaterial;
        }
        else if (throwerEnviManager != null)
        {
            throwerEnviManager.showEndStatusEvent += NewMaterial;
        }
        originalMaterial = mRenderer.material;
    }

    private void NewMaterial(Material material)
    {
        StartCoroutine(ResetMaterial(material));
    }

    IEnumerator ResetMaterial(Material material)
    {
        mRenderer.material = material;
        yield return new WaitForSeconds(2f);
        mRenderer.material = originalMaterial;
    }

    private void OnDestroy()
    {

        if (manager != null)
        {
            manager.showEndStatusEvent -= NewMaterial;
        }
        else if (throwerEnviManager != null)
        {
            throwerEnviManager.showEndStatusEvent -= NewMaterial;
        }
    }
}
