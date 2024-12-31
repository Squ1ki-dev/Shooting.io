using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyCheckStates : MonoBehaviour
    {
        [SerializeField] private EnemyMoveToPlayer _enemyMove;
        [SerializeField] private EnemyAttack _enemyAttack;

        private GameState _gameState;

        private void Start()
        {
            _gameState = FindObjectOfType<GameState>();
            CheckGameState();
        }

        private void Update() => CheckGameState();

        private void CheckGameState()
        {
            if(_gameState.CurrentState == GameStates.Game)
            {
                _enemyMove.enabled = true;
                _enemyAttack.enabled = true;
            }
            else
            {
                _enemyMove.enabled = false;
                _enemyAttack.enabled = false;
            }
        }
    }
}