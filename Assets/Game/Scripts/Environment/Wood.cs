using UnityEngine;

public class Wood : MonoBehaviour
{
    private const float _SPAWN_Y = 0.0f;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;

    private Transform _transform;

    private Bee _bee;

    private Vector2 _position;

    private GameState _gameState;

    private void Update()
    {
        if (_gameState == GameState.Level)
        {
            _position = _transform.position;
            _position += Vector2.left * Level.MOVE_X_VELOCITY * Time.deltaTime;

            _transform.position = _position;

            if (_position.x <= -LevelSpawner.SPAWN_X)
            {
                SetDestroyState();
            }
        }
    }

    public void Initialize(Bee bee)
    {
        _bee = bee;

        _position = new Vector2(LevelSpawner.SPAWN_X, _SPAWN_Y);
        _transform = transform;
        _transform.position = _position;

        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        _bee.PlayEvent += HandlePlay;
        _bee.DefeatEvent += HandleDefeat;

        _gameState = GameState.Level;
    }

    private void SetDestroyState()
    {
        _bee.PlayEvent -= HandlePlay;
        _bee.DefeatEvent -= HandleDefeat;

        Destroy(gameObject);
    }

    private void HandlePlay()
    {
        _gameState = GameState.Level;
    }

    private void HandleDefeat()
    {
        _gameState = GameState.Menu;
    }
}