using UnityEngine;

public class Spider : Enemy
{
    private const float _MIN_MOVE_Y_VELOCITY = 0.4f;
    private const float _MAX_MOVE_Y_VELOCITY = 1.6f;

    private float _startPositionY;
    private float _endPositionY;

    private float _moveYDistance;

    private float _time;

    private bool _isMoveY;

    private void Update()
    {
        if (_isMoveY)
        {
            _time += Time.deltaTime;

            float distance = _time * _moveVelocity;
            float t = distance / _moveYDistance;
            _positionY = Mathf.Lerp(_startPositionY, _endPositionY, t);

            if (t >= 1.0f)
            {
                _isMoveY = false;
            }
        }

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

        _startPositionY = _position.y;

        float centerY = (LevelSpawner.MIN_SPAWN_Y + LevelSpawner.MAX_SPAWN_Y) * 0.5f;

        if (_position.y >= centerY)
        {
            _endPositionY = LevelSpawner.MIN_SPAWN_Y;
        }
        else
        {
            _endPositionY = LevelSpawner.MAX_SPAWN_Y;
        }

        _moveYDistance = Mathf.Abs(_endPositionY - _startPositionY);

        _moveVelocity = Random.Range(_MIN_MOVE_Y_VELOCITY, _MAX_MOVE_Y_VELOCITY);

        _positionY = 0.0f;
        _time = 0.0f;
        _isMoveY = true;
    }
}