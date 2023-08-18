using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    private const string _GAME_SCENE = "Game";

    private void Awake()
    {
        SceneManager.LoadScene(_GAME_SCENE);
    }
}