using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEngine : MonoBehaviour
{
    private ThrowerAgent throwerAgent;
    private ThrowerEnviManager manager;
    [SerializeField] float lifeTime;
    void Start()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        throwerAgent.BallDies();
        manager.StartShowEndStatusEvent(0);

        Destroy(gameObject);
    }

    public void setAgent(ThrowerAgent agent)
    {
        throwerAgent = agent;
    }

    public void setManager(ThrowerEnviManager manager)
    {
        this.manager = manager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            throwerAgent.BallHitGoal();
            manager.StartShowEndStatusEvent(1);

            Destroy(gameObject);
        }
    }
}
