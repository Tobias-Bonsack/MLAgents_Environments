using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class EventManager : MonoBehaviour
    {
        #region events
        public EventHandler<EventArgs> OnChooseField;
        #endregion

        #region event args
        public class OnChooseFieldEventArgs
        {
            public int _fieldNumber;
        }
        #endregion
    }
}
