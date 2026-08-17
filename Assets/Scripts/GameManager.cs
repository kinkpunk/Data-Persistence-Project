using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Делаем синглтон, чтобы к нему можно было обратиться из любой сцены
    public static GameManager Instance;

    // Это имя будет сохраняться в памяти между сценами
    [HideInInspector] public string PlayerName = "Player";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Объект не будет уничтожаться при смене сцены
    }
}
