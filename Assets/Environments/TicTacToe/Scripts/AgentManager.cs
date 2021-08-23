using System;
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
        [SerializeField] GameObject _manager;

        [Header("Variable compensations")]
        [SerializeField] FieldManager.Status _id;

        [Header("Observations")]
        [SerializeField] FieldManager[] _fields;
        [SerializeField] bool _isTurn;

        [Header("Rewards")]
        [SerializeField] float _onWrongTurn = -0.5f;
        [SerializeField] float _onDraw = 0.5f;
        [SerializeField] float _onWin = 1f;
        [SerializeField] float _onLose = -1f;

        public override void Initialize()
        {
            _manager.GetComponent<EventManager>()._onEndTurn += ActionOnEndTurn;
            _manager.GetComponent<EventManager>()._onEndGame += ActionOnEndGame;
        }
        public override void OnEpisodeBegin()
        {
            _manager.GetComponent<EventManager>().TriggeronResetGame();
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

            sensor.AddObservation(_isTurn);
        }
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
            int clickedField = 9;
            Mouse mouse = Mouse.current;
            if (mouse != null && mouse.leftButton.isPressed)
            {
                Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
                Debug.DrawLine(ray.origin, ray.GetPoint(20), Color.red, 1f);

                if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.TryGetComponent<FieldManager>(out FieldManager hittedFM))
                    clickedField = hittedFM._number; // if you hitted a field
            }

            discreteActions[0] = clickedField;
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            ActionSegment<int> discreteActions = actions.DiscreteActions;
            if (!_isTurn)
            {
                AddReward(_onWrongTurn);
                return;
            }
            else if (discreteActions[0] == 9) return;

            if (_fields[discreteActions[0]].ActivateField(_id))
                _manager.GetComponent<EventManager>().TriggerOnEndTurn();
        }
        private void ActionOnEndTurn(object sender, EventArgs args)
        {
            _isTurn = !_isTurn;
        }

        private void ActionOnEndGame(object sender, EventManager.OnEndGameEventArg arg)
        {
            if (arg._winner == FieldManager.Status.FREE) AddReward(_onDraw);
            else if (arg._winner == _id) AddReward(_onWin);
            else AddReward(_onLose);

            EndEpisode();
        }
    }
}
