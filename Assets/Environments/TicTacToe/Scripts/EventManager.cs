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
        public EventHandler<EventArgs> _onEndGame;
        #endregion

        #region event args
        #endregion

        #region triggers
        public void TriggerOnEndTurn() => _onEndTurn?.Invoke(null, null);
        public void TriggerOnEndGame() => _onEndGame?.Invoke(null, null);
        #endregion
    }
}
