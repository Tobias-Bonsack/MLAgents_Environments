using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class FieldManager : MonoBehaviour
    {
        public Status _status;
        public int _number;
        public enum Status
        {
            FREE, CROSS, TORUS

        }
    }
}

