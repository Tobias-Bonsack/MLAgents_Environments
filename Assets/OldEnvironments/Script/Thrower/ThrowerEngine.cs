using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerEngine : MonoBehaviour
{
    [SerializeField] Transform origin;
    [SerializeField] Transform body;
    [SerializeField] GameObject ballPreFab;
    [SerializeField] ThrowerEnviManager manager;

    public void throwBall(float power)
    {
        GameObject gameObject1 = Instantiate(ballPreFab, origin.position, origin.rotation);

        Vector3 direction = (origin.position - body.position).normalized;
        gameObject1.GetComponent<BallEngine>().setAgent(gameObject.GetComponent<ThrowerAgent>());
        gameObject1.GetComponent<BallEngine>().setManager(manager);
        gameObject1.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }
}
