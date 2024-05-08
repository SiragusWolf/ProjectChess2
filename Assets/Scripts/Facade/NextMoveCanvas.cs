using System;
using UnityEngine;
using UnityEngine.UI;

namespace Facade
{
    public class NextMoveCanvas : MonoBehaviour
    {
        [SerializeField] private Sprite rookSprite;
        [SerializeField] private Sprite horseSprite;
        [SerializeField] private Sprite bishopSprite;
        [SerializeField] private Sprite queenSprite;
        [SerializeField] private Sprite kingSprite;
        [SerializeField] private Sprite pawnSprite;
        
        [SerializeField] private Image _currentMovement;
        [SerializeField] private Image _nextMovement;
        [SerializeField] private Image _nextNextMovement;

        

        public void UpdateImages(PlayerMovement.Movement current, PlayerMovement.Movement next, PlayerMovement.Movement nextNext)
        {
            _currentMovement.sprite = getSprite(current);
            _nextMovement.sprite = getSprite(next);
            _nextNextMovement.sprite = getSprite(nextNext);
        }

        private Sprite getSprite(PlayerMovement.Movement movement)
        {
            Sprite sprite = rookSprite;
            switch (movement)
            {
                case PlayerMovement.Movement.Horse:
                    sprite = horseSprite;
                    break;
                case PlayerMovement.Movement.Bishop:
                    sprite = bishopSprite;
                    break;
                case PlayerMovement.Movement.Rook:
                    sprite = rookSprite;
                    break;
                case PlayerMovement.Movement.Queen:
                    sprite = queenSprite;
                    break;
                case PlayerMovement.Movement.King:
                    sprite = kingSprite;
                    break;
                case PlayerMovement.Movement.Pawn:
                    sprite = pawnSprite;
                    break;
            }
            return sprite;
        }
    }
}
