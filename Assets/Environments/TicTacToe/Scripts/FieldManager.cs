using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class FieldManager : MonoBehaviour
    {
        [Header("Privates")]
        [SerializeField] GameObject[] _preFabs;
        [SerializeField] float _distanceToSpawn;

        [Header("Public")]
        public Status _status;
        public int _number;
        public void ActivateField(Status player)
        {
            if (_status != Status.FREE) return;

            GameObject objectToCreate = player == Status.CROSS ? _preFabs[0] : _preFabs[1];

            GameObject createdObject = Instantiate(objectToCreate, gameObject.transform);
            createdObject.transform.localPosition += Vector3.up * _distanceToSpawn;

            _status = player;
        }

        public enum Status
        {
            FREE, CROSS, TORUS

        }
    }
}

