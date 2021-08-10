using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BTFAgent : Agent
{
    [SerializeField] BTFEnviManager manager;

    [Header("Player")]
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    [Header("Observations")]
    public bool isFoodSpawned;

    [Header("Rewards")]
    [SerializeField] float rewordForStep;
    [SerializeField] float rewardOnWrongButtonUse;
    [SerializeField] float rewardOnGoal;

    [Header("Decision Requester")]
    private int sinceLastRequest = 0;

    public override void OnEpisodeBegin()
    {
        Debug.Log("Start Episode!");
        manager.StartResetPositionEvent();
        manager.destroyFood();
        isFoodSpawned = false;
        transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(0f, 1f), 0f, Random.Range(0f, 1f)));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sinceLastRequest = 0;
        sensor.AddObservation(transform.rotation.eulerAngles.y / 360.0f);
        sensor.AddObservation(isFoodSpawned);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.W))
        {
            discreteActions[0] = 1; // forward
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActions[0] = 2; // turn left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            discreteActions[0] = 3; // Turn right
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            discreteActions[0] = 4; // Push button
        }
        else
        {
            discreteActions[0] = 0; // nothing
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        sinceLastRequest++;
        int move = actions.DiscreteActions[0];
        switch (move)
        {
            case 1:
                GetComponent<CharacterController>().Move(transform.forward * speed * Time.deltaTime);
                break;
            case 2:
                transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
                break;
            case 3:
                transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                break;
            case 4:
                PressButton();
                break;
            default:
                break;
        }
        AddReward(rewordForStep);
    }

    private void PressButton()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, Vector3.one * 2, transform.rotation);
        foreach (Collider collider in colliders)
        {
            if (!isFoodSpawned && collider.TryGetComponent<FoodSpawner>(out FoodSpawner foodSpawner))
            {
                isFoodSpawned = true;
                foodSpawner.SpawnFood();
                AddReward(rewardOnGoal);
                Debug.Log("Found Button!");
                return;
            }
        }

        if(sinceLastRequest == 1) {
            AddReward(rewardOnWrongButtonUse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            isFoodSpawned = false;
            AddReward(rewardOnGoal);
            manager.StartShowEndStatusEvent(1);
            manager.destroyFood();
            Debug.Log("Found Food!");
            EndEpisode();
        }
    }
}
