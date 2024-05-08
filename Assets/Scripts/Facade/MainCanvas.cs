using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Facade
{
    public class MainCanvas : MonoBehaviour, INextMoveCanvasProvider, IUndoCanvasProvider
    {
        [SerializeField] private UndoCanvas _undoCanvas;
        [SerializeField] private NextMoveCanvas _nextMoveCanvas;
        [SerializeField] private Image GameOverScreen;
        [SerializeField] private Image WinScreen;

        public UndoCanvas UndoCanvas => _undoCanvas;
        public NextMoveCanvas NextMoveCanvas => _nextMoveCanvas;

        public static MainCanvas Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void GameOver()
        {
            GameOverScreen.enabled = true;
        }

        public void WinGame()
        {
            WinScreen.enabled = true;
        }


}
}
