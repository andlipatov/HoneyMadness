using UnityEngine;

public class Bird : Enemy
{
    private const float _MIN_MOVE_Y_DISTANCE = 0.6f;
    private const float _MAX_MOVE_Y_DISTANCE = 1.4f;

    private const float _MIN_MOVE_Y_VELOCITY = 0.4f;
    private const float _MAX_MOVE_Y_VELOCITY = 1.6f;

    private float _minPositionY;
    private float _maxPositionY;

    private float _moveYDistance;
    private float _halfMoveYDistance;

    private void Update()
    {
        float t = Mathf.PingPong(Time.time * _moveVelocity, _moveYDistance) / _moveYDistance;
        _positionY = Mathf.Lerp(_minPositionY, _maxPositionY, t);

        _position += Vector2.left * Level.MOVE_X_VELOCITY * Time.deltaTime;
        _position = new Vector2(_position.x, _positionY);

        _transform.position = _position;

        if (_position.x <= -LevelSpawner.SPAWN_X)
        {
            SetDestroyState();
        }
    }

    public override void Initialize(Bee bee, Vector2 position)
    {
        base.Initialize(bee, position);

        _moveYDistance = Random.Range(_MIN_MOVE_Y_DISTANCE, _MAX_MOVE_Y_DISTANCE);
        _halfMoveYDistance = Random.Range(_MIN_MOVE_Y_DISTANCE, _MAX_MOVE_Y_DISTANCE);

        if (_position.y + _halfMoveYDistance >= LevelSpawner.MAX_SPAWN_Y)
        {
            _maxPositionY = LevelSpawner.MAX_SPAWN_Y;
            _minPositionY = _maxPositionY - _moveYDistance;
        }
        else if (_position.y - _halfMoveYDistance <= LevelSpawner.MIN_SPAWN_Y)
        {
            _minPositionY = LevelSpawner.MIN_SPAWN_Y;
            _maxPositionY = _minPositionY + _moveYDistance;
        }
        else
        {
            _minPositionY = _position.y - _moveYDistance;
            _maxPositionY = _position.y + _moveYDistance;
        }

        _positionY = 0.0f;

        _moveVelocity = Random.Range(_MIN_MOVE_Y_VELOCITY, _MAX_MOVE_Y_VELOCITY);
    }
}