using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace TTT
{
    public class AgentManager : Agent
    {
        [Header("Variable compensations")]
        [SerializeField] FieldManager.Status _id;

        [Header("Observations")]
        [SerializeField] FieldManager[] _fields;

        public override void OnEpisodeBegin()
        {
            //TODO create reset methods
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            //TODO collect observations

            foreach (FieldManager fieldManager in _fields)
            {
                if (fieldManager._status == FieldManager.Status.FREE)
                {
                    sensor.AddObservation(0);
                }
                else
                {
                    int owner = _id == FieldManager.Status.CROSS ? 0 : 1;//TODO make it work
                }
            }
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            //TODO create heuristic
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            //TODO create controller
        }

        public void AddExternalReward(float reward)
        {
            Debug.Log("Reward: " + reward);
            AddReward(reward);
        }

        public void EndExternalEpisode()
        {
            Debug.Log("End Episode");
            EndEpisode();
        }
    }
}
