using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class ThrowerAgent : Agent
{

    [Header("Observation-Parameter")]
    [SerializeField] Transform goalTransform;
    bool canThrow = true;

    [Header("Reward-Parameter")]
    [SerializeField] float notAllowedToThrow;
    [SerializeField] float onStep;
    [SerializeField] float onHit;
    [SerializeField] float noHit;
    
    [Header("Agent")]
    [SerializeField] float reloadTime;
    [SerializeField] int throwPower;
    private int hitCount = 0;

    [Header("Ohters")]
    [SerializeField] Transform rotateObject;
    [SerializeField] ThrowerEnviManager manager;

    public override void OnEpisodeBegin()
    {
        manager.StartResetPositionEvent();
        manager.StartShowPointsEvent(0);

        StopAllCoroutines();

        canThrow = true;
        hitCount = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float distance = Mathf.Abs(transform.localPosition.z - goalTransform.localPosition.z);

        sensor.AddObservation(distance);
        sensor.AddObservation(canThrow);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Vertical");
        continuousActions[1] = Input.GetAxis("Horizontal");

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<float> continuousActions = actions.ContinuousActions;

        float angle = (continuousActions[0] + 1f) * -45f;
        rotateObject.localEulerAngles = new Vector3(angle, rotateObject.localEulerAngles.y, rotateObject.localEulerAngles.z);

        float power = (continuousActions[1] + 1f) * (throwPower / 2);

        ActionSegment<int> discreteActions = actions.DiscreteActions;
        bool willThrow = discreteActions[0] == 1;

        if (canThrow && willThrow)
        {
            StartCoroutine(Reload());
            GetComponent<ThrowerEngine>().throwBall(power);
        }
        else if(!canThrow && willThrow)
        {
            Debug.Log("Try to throw illegal!");
            AddReward(notAllowedToThrow);
        }

        AddReward(onStep);
    }


    public void BallHitGoal()
    {
        AddReward(onHit);
        Debug.Log("Hit Goal!");
        if (++hitCount >= 10)
        {
            EndEpisode();
        }
        manager.StartShowPointsEvent(hitCount);
    }
    public void BallDies()
    {
        AddReward(noHit);
        Debug.Log("No Goal!");
    }

    IEnumerator Reload()
    {
        canThrow = false;
        yield return new WaitForSeconds(reloadTime);
        canThrow = true;
    }


}
