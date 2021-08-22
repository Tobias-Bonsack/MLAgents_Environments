using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class GameManager : MonoBehaviour
    {
        private EventManager _eventManager;
        [SerializeField] GameObject[] _fields;
        private void Awake()
        {
            _eventManager = gameObject.GetComponent<EventManager>();

            _eventManager._onEndTurn += ActionOnEndTurn;
        }

        private void ActionOnEndTurn(object sender, EventArgs args)
        {
            //TODO check if someone wins!
        }
    }
}
