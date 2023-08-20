using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text _inputText;
    [SerializeField] private TMP_Text _honeyText;
    [SerializeField] private TMP_Text _recordText;
    
    private GameController _gameController;

    private GameState _gameState;

    private void Update()
    {
        if (_gameState == GameState.Menu)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
            {
                _gameController.SetGameState(GameState.Level);
            }
        }
    }

    public void Initialize(GameController gameController)
    {
        _gameController = gameController;

#if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = true;
#endif
    }

    public void Subscribe(Honey honey)
    {
        honey.HoneyChangedEvent += HandleHoneyChanged;
    }

    public void Unsubscribe(Honey honey)
    {
        honey.HoneyChangedEvent -= HandleHoneyChanged;
    }

    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
        _inputText.enabled = _gameState == GameState.Menu;
    }

    public void HandleHoneyChanged(int currentValue, int recordValue)
    {
        _honeyText.text = currentValue.ToString();
        _recordText.text = recordValue.ToString();
    }
}