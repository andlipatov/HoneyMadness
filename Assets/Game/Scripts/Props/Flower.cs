using UnityEngine;

public class Flower : MonoBehaviour
{
    private const int _HONEY_AMOUNT = 1;

    private const float _FOLLOWING_DISTANCE = 2.0f;
    private const float _INTERACT_DISTANCE = 0.5f;

    private const float _FOLLOWING_VELOCITY = Level.MOVE_X_VELOCITY - 0.2f;

    private const float _ROTATION_VELOCITY = 720.0f;

    [SerializeField] private Transform _spriteTransform;

    [SerializeField] private GameObject _flowerEffectPrefab;

    private Transform _transform;

    private GameController _gameController;
    private Bee _bee;

    private Vector2 _position;
    private Vector2 _direction;

    private float _distance;

    private State _state;

    private enum State
    {
        Idle,
        Following
    }

    private void Update()
    {
        _distance = Vector3.Distance(_transform.position, _bee.Position);

        switch (_state)
        {
            case State.Idle:
            {
                _position += Vector2.left * Level.MOVE_X_VELOCITY * Time.deltaTime;
                _transform.position = _position;

                if (_distance <= _FOLLOWING_DISTANCE)
                {
                    _state = State.Following;
                }

                if (_position.x <= -LevelSpawner.SPAWN_X)
                {
                    SetDestroyState();
                }

                break;
            }
            case State.Following:
            {
                if (_distance <= _INTERACT_DISTANCE)
                {
                    _gameController.HoneyIncrement(_HONEY_AMOUNT);

                    SetDestroyState();
                }
                else if (_distance > _FOLLOWING_DISTANCE)
                {
                    _state = State.Idle;
                }
                else
                {
                    _direction = (_bee.Position - _transform.position).normalized;
                    _position += (Vector2.left * Level.MOVE_X_VELOCITY + _direction * _FOLLOWING_VELOCITY) * Time.deltaTime;
                    _transform.position = _position;

                    _spriteTransform.Rotate(Vector3.forward, _ROTATION_VELOCITY * Time.deltaTime);
                }

                break;
            }
        }
    }

    public void Initialize(GameController gameController, Bee bee, Vector2 position)
    {
        _gameController = gameController;

        _bee = bee;
        _bee.DefeatEvent += HandleDefeat;

        _position = position;

        _transform = transform;
        _transform.position = _position;

        _state = State.Idle;
    }

    private void SetDestroyState()
    {
        _bee.DefeatEvent -= HandleDefeat;

        Instantiate(_flowerEffectPrefab, _transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void HandleDefeat()
    {
        SetDestroyState();
    }
}