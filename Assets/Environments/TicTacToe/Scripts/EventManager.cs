using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class EventManager : MonoBehaviour
    {
        #region events
        public EventHandler<EventArgs> _onEndTurn;
        public EventHandler<OnEndGameEventArg> _onEndGame;
        public EventHandler<EventArgs> _onResetGame;
        #endregion

        #region event args
        public class OnEndGameEventArg
        {
            public FieldManager.Status _winner;
        }
        #endregion

        #region triggers
        public void TriggerOnEndTurn() => _onEndTurn?.Invoke(null, null);
        public void TriggerOnEndGame(OnEndGameEventArg arg) => _onEndGame?.Invoke(null, arg);
        public void TriggerOnResetGame() => _onResetGame?.Invoke(null, null);
        #endregion
    }
}
