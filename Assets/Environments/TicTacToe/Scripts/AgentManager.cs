using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.InputSystem;

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
            foreach (FieldManager fieldManager in _fields)
            {
                int value = -1;
                switch (fieldManager._status)
                {
                    case FieldManager.Status.FREE:
                        value = 0;
                        break;
                    case FieldManager.Status.CROSS:
                        value = _id == FieldManager.Status.CROSS ? 1 : 2;
                        break;
                    case FieldManager.Status.TORUS:
                        value = _id == FieldManager.Status.TORUS ? 1 : 2;
                        break;
                    default:
                        Debug.LogWarning("No fieldstatus available!");
                        break;
                }
                sensor.AddObservation(value);
            }
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            //TODO create heuristic
            Mouse mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.isPressed)
            {
                Debug.Log("is clicked!");
                Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            }
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
