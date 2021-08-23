using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class GameManager : MonoBehaviour
    {
        private EventManager _eventManager;
        [SerializeField] FieldManager[] _fields;
        private void Awake()
        {
            _eventManager = gameObject.GetComponent<EventManager>();

            _eventManager._onEndTurn += ActionOnEndTurn;
        }

        private void ActionOnEndTurn(object sender, EventArgs args)
        {
            FieldManager.Status winner = FieldManager.Status.FREE; //free == draw
            for (int i = 0; i < 3; i++) //check rows and columns
            {
                for (int j = 0; j < 3; j++) //check columns
                {
                    if (_fields[j]._status == _fields[j + 3]._status && _fields[j]._status == _fields[j + 6]._status)
                    {
                        winner = _fields[j]._status;
                        if (winner != FieldManager.Status.FREE) goto FoundWinner;
                    }
                }
                for (int j = 0; j < 7; j += 3) //check rows
                {
                    if (_fields[j]._status == _fields[j + 1]._status && _fields[j]._status == _fields[j + 2]._status)
                    {
                        winner = _fields[j]._status;
                        if (winner != FieldManager.Status.FREE) goto FoundWinner;
                    }
                }
            }

            if (_fields[2]._status == _fields[4]._status && _fields[2]._status == _fields[6]._status || // check dia left-right
                _fields[0]._status == _fields[4]._status && _fields[0]._status == _fields[8]._status) //check dia right-left
            {
                winner = _fields[4]._status;
                if (winner != FieldManager.Status.FREE) goto FoundWinner;
            }

        FoundWinner:
            bool gameIsOver = true;
            foreach (FieldManager fM in _fields)
            {
                if (fM._status == FieldManager.Status.FREE)
                {
                    gameIsOver = false;
                    break;
                }
            }
            if (gameIsOver || winner != FieldManager.Status.FREE) _eventManager.TriggerOnEndGame(new EventManager.OnEndGameEventArg { _winner = winner });
        }
    }
}
