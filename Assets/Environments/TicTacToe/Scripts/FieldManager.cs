using System;
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
        [SerializeField] GameObject _manager;

        [Header("Public")]
        public Status _status;
        public int _number;

        private void Awake()
        {
            _manager.GetComponent<EventManager>()._onResetGame += ActionOnResetgame;
        }
        public bool ActivateField(Status player)
        {
            if (_status != Status.FREE) return false;

            GameObject objectToCreate = player == Status.CROSS ? _preFabs[0] : _preFabs[1];

            GameObject createdObject = Instantiate(objectToCreate, gameObject.transform);
            createdObject.transform.localPosition += Vector3.up * _distanceToSpawn;

            _status = player;
            return true;
        }

        private void ActionOnResetgame(object sender, EventArgs args)
        {
            _status = Status.FREE;
            if (gameObject.transform.childCount != 0) Destroy(gameObject.transform.GetChild(0).gameObject);
        }

        public enum Status
        {
            FREE, CROSS, TORUS

        }
    }
}

