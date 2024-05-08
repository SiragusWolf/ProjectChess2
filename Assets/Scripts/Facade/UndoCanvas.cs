using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Facade
{
    public class UndoCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button undoButton;
        private Player _player;
        

        private void Awake()
        {
            undoButton.onClick.RemoveAllListeners();
            undoButton.onClick.AddListener(GameManager.Instance.UndoLastTurn);
        }

        public void updateUndoLeft(int newUndoLeft)
        {
            _text.text = newUndoLeft.ToString();
        }
    }
}
