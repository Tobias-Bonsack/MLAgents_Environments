using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class FieldManager : MonoBehaviour
    {
        public Status _status;
        public int _number;
        public void ActivateField(Status player)
        {
            //TODO create symbol and change status, also check if change is possible (agent checks automatic, but player dont)
        }

        public enum Status
        {
            FREE, CROSS, TORUS

        }
    }
}

