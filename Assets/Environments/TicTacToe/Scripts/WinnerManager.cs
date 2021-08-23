using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTT
{
    public class WinnerManager : MonoBehaviour
    {
        [SerializeField] GameObject _manager;
        [SerializeField] FieldManager.Status _status;
        private void Awake() => _manager.GetComponent<EventManager>()._onEndGame += ActionOnendGame;

        private void ActionOnendGame(object sender, EventManager.OnEndGameEventArg args)
        {
            if (_status == args._winner) gameObject.GetComponent<Animator>().SetTrigger("_isWinner");
        }

    }
}
