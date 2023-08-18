using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    private const float _WIDTH = 17.92f;
    private const float _HALF_WIDTH = _WIDTH * 0.5f;

    [SerializeField] private Transform _environmentTransform;
    private Vector2 _position;

    private GameState _gameState;

    private void Update()
    {
        if (_gameState == GameState.Level)
        {
            _position = _environmentTransform.position;
            _position += Vector2.left * Level.MOVE_X_VELOCITY * Time.deltaTime;

            if (_position.x <= -_HALF_WIDTH)
            {
                _position = Vector2.zero;
            }

            _environmentTransform.position = _position;
        }
    }

    public void SetGameState(GameState state)
    {
        _gameState = state;
    }
}