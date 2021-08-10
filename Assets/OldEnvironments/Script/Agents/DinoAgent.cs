using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DinoAgent : Agent
{

    [Header("Player")]
    [SerializeField] Rigidbody rB;
    [SerializeField] float jumpPower;
    [SerializeField] int life;
    private int dinoLife;
    [SerializeField] float reloadJumpTime;
    private float relativeReloatJumpTime = 0f;

    [Header("Observation")]
    private bool canJump = true;
    [SerializeField] SpawnManager spawnManager;

    [Header("Rewards")]
    [SerializeField] float notAllowedJump;
    [SerializeField] float forStep;
    [SerializeField] float hitObstacle;

    [Header("Other")]
    [SerializeField] DinoEventManager eventManager;
    [SerializeField] GameObject[] optic;
    [SerializeField] GameObject[] kindOfMovement;
    private int kindIndex = 0;
    [SerializeField] float timeToSpawnObstacle;

    public override void OnEpisodeBegin()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeMesh());
        StartCoroutine(SpawnEnemy());

        spawnManager.SetSpeedMultiplicator(1f);
        canJump = true;
        dinoLife = life;
        eventManager.StartChangeLifeEvent(dinoLife);
        eventManager.StartShowPointsEvent(0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(canJump);
        sensor.AddObservation(spawnManager.GetSpeedMultiplicator());
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> da = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.D)) // Jump
        {
            da[0] = 2;
        }
        else if (Input.GetKey(KeyCode.C)) // Duck
        {
            da[0] = 1;
        }
        else // Stand
        {
            da[0] = 0;
        }

        ActionSegment<float> ca = actionsOut.ContinuousActions;
        ca[0] = 1f;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<int> da = actions.DiscreteActions;
        ActionSegment<float> ca = actions.ContinuousActions;

        int newForm = kindIndex;
        switch (da[0])
        {
            case 0:
                newForm = 0;
                break;
            case 1:
                newForm = 2;
                break;
            case 2:
                float power = ((ca[0] + 1) / 2);
                relativeReloatJumpTime = reloadJumpTime * power;
                Jump(jumpPower * power);
                break;

        }

        if (newForm != kindIndex)
        {
            kindIndex = newForm;
            ChangeShownObject(kindOfMovement);
        }

        AddReward(forStep);
        eventManager.StartShowPointsEvent(StepCount);
    }

    private void Jump(float power)
    {
        if (canJump)
        {
            StartCoroutine(JumpReload());
            rB.AddForce(Vector3.up * power, ForceMode.Impulse);
        }
        else
        {
            AddReward(notAllowedJump);
        }

    }

    private IEnumerator JumpReload()
    {
        canJump = false;
        yield return new WaitForSeconds(relativeReloatJumpTime);
        canJump = true;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawnObstacle);
            eventManager.StartSpawnObstacleEvent();
        }
    }

    private IEnumerator ChangeMesh()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ChangeShownObject(optic);
        }
    }

    private void ChangeShownObject(GameObject[] list)
    {
        foreach (GameObject go in list)
        {
            go.SetActive(!go.activeSelf);
        }
    }

    public void GetHitByObstacle()
    {
        AddReward(hitObstacle);
        if (--dinoLife <= 0)
        {
            EndEpisode();
            return;
        }
        eventManager.StartChangeLifeEvent(dinoLife);
    }
}
