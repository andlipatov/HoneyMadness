using System.Collections;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public const float SPAWN_X = 12.0f;

    public const float MIN_SPAWN_Y = -2.0f;
    public const float MAX_SPAWN_Y = 2.6f;

    private const float _MIN_WOOD_SPAWN_DELAY = 1.5f;
    private const float _MAX_WOOD_SPAWN_DELAY = 3.0f;

    private const float _MIN_SPAWN_DELAY = 0.3f;
    private const float _MAX_SPAWN_DELAY = 1.5f;

    private readonly Type[] _types =
    {
        Type.Flower,
        Type.Bird,
        Type.Bird,
        Type.Spider,
        Type.Fish,
    };

    [SerializeField] private Wood _woodPrefab;
    [SerializeField] private Flower _flowerPrefab;
    [SerializeField] private Bird _birdPrefab;
    [SerializeField] private Spider _spiderPrefab;
    [SerializeField] private Fish _fishPrefab;

    private Transform _levelTransform;
    private Transform _charactersTransform;

    private GameController _gameController;
    private Bee _bee;

    private Coroutine _woodsCoroutine;
    private Coroutine _flowersAndEnemiesCoroutine;

    private enum Type
    {
        Flower,
        Bird,
        Spider,
        Fish
    }

    public void Initialize(GameController gameController, Bee bee, Transform levelTransform, Transform charactersTransform)
    {
        _gameController = gameController;
        _bee = bee;
        _levelTransform = levelTransform;
        _charactersTransform = charactersTransform;
    }

    public void SetGameState(GameState state)
    {
        if (state == GameState.Menu)
        {
            if (_woodsCoroutine != null)
            {
                StopCoroutine(_woodsCoroutine);
                _woodsCoroutine = null;
            }

            if (_flowersAndEnemiesCoroutine != null)
            {
                StopCoroutine(_flowersAndEnemiesCoroutine);
                _flowersAndEnemiesCoroutine = null;
            }
        }
        else
        {
            _woodsCoroutine = StartCoroutine(SpawnWoods());
            _flowersAndEnemiesCoroutine = StartCoroutine(SpawnFlowersAndEnemies());
        }
    }

    private IEnumerator SpawnWoods()
    {
        while (true)
        {
            float spawnDelay = Random.Range(_MIN_WOOD_SPAWN_DELAY, _MAX_WOOD_SPAWN_DELAY);
            yield return new WaitForSeconds(spawnDelay);

            SpawnWood();
        }
    }

    private IEnumerator SpawnFlowersAndEnemies()
    {
        while (true)
        {
            float spawnDelay = Random.Range(_MIN_SPAWN_DELAY, _MAX_SPAWN_DELAY);
            yield return new WaitForSeconds(spawnDelay);

            int typeIndex = Random.Range(0, _types.Length);
            Type type = _types[typeIndex];

            switch (type)
            {
                case Type.Flower:
                {
                    SpawnFlower();
                    break;
                }
                case Type.Bird:
                {
                    SpawnBird();
                    break;
                }
                case Type.Spider:
                {
                    SpawnSpider();
                    break;
                }
                case Type.Fish:
                {
                    SpawnFish();
                    break;
                }
            }
        }
    }

    private void SpawnWood()
    {
        Wood wood = Instantiate(_woodPrefab, Vector3.zero, Quaternion.identity);
        wood.transform.parent = _levelTransform;
        wood.Initialize(_bee);
    }

    private void SpawnFlower()
    {
        Flower flower = Instantiate(_flowerPrefab, Vector3.zero, Quaternion.identity);
        flower.transform.parent = _levelTransform;
        flower.Initialize(_gameController, _bee, GetRandomSpawnPosition());
    }

    private void SpawnBird()
    {
        Bird bird = Instantiate(_birdPrefab, Vector3.zero, Quaternion.identity);
        bird.transform.parent = _charactersTransform;
        bird.Initialize(_bee, GetRandomSpawnPosition());
    }

    private void SpawnSpider()
    {
        Spider spider = Instantiate(_spiderPrefab, Vector3.zero, Quaternion.identity);
        spider.transform.parent = _charactersTransform;
        spider.Initialize(_bee, GetRandomSpawnPosition());
    }

    private void SpawnFish()
    {
        Fish fish = Instantiate(_fishPrefab, Vector3.zero, Quaternion.identity);
        fish.transform.parent = _charactersTransform;
        fish.Initialize(_bee);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(SPAWN_X, Random.Range(MIN_SPAWN_Y, MAX_SPAWN_Y));
    }
}
