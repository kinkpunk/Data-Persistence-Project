using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem; // MIGRATED: New Input System namespace
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    // Добавьте эти Text-объекты на сцену и назначьте в инспекторе
    public Text PlayerNameText;
    public Text HighScoreText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private string m_PlayerName;

    private int m_HighScore;
    private string m_HighScorePlayerName;

    // MIGRATED: InputAction replaces Input.GetKeyDown(KeyCode.Space)
    private InputAction m_LaunchAction;

    void Awake()
    {
        m_LaunchAction = new InputAction("Launch", InputActionType.Button, "<Keyboard>/space");
    }

    void OnEnable()
    {
        m_LaunchAction.Enable();
    }

    void OnDisable()
    {
        m_LaunchAction.Disable();
    }

    void Start()
    {
        // Получаем имя игрока из статического класса
        m_PlayerName = GameData.PlayerName;

        // Загружаем сохранённый рекорд между сессиями
        LoadScore();

        UpdateScoreText();
        UpdateHighScoreText();

        SpawnBricks();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (m_LaunchAction.WasPressedThisFrame()) // MIGRATED: was Input.GetKeyDown(KeyCode.Space)
            {
                m_Started = true;

                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (m_LaunchAction.WasPressedThisFrame()) // MIGRATED: was Input.GetKeyDown(KeyCode.Space)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;

        UpdateScoreText();
        UpdateHighScoreText();
    }

    public void GameOver()
    {
        m_GameOver = true;

        if (GameOverText != null)
        {
            GameOverText.SetActive(true);
        }

        // Если рекорд побит, сохраняем новый рекорд и имя текущего игрока
        if (m_Points > m_HighScore)
        {
            m_HighScore = m_Points;
            m_HighScorePlayerName = m_PlayerName;

            SaveScore();
        }

        UpdateHighScoreText();
    }

    private void SpawnBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);

                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void UpdateScoreText()
    {
        // Вариант 1: если есть отдельный текст для имени игрока
        if (PlayerNameText != null)
        {
            PlayerNameText.text = m_PlayerName;

            if (ScoreText != null)
            {
                ScoreText.text = $"Score : {m_Points}";
            }
        }
        // Вариант 2: если отдельного текста нет, показываем имя рядом со счётом
        else if (ScoreText != null)
        {
            ScoreText.text = $"Score : {m_Points} | {m_PlayerName}";
        }
    }

    private void UpdateHighScoreText()
    {
        if (HighScoreText == null)
        {
            return;
        }

        int displayScore = m_HighScore;
        string displayName = string.IsNullOrEmpty(m_HighScorePlayerName)
            ? "None"
            : m_HighScorePlayerName;

        // Если текущий игрок побил рекорд, показываем его результат сразу
        if (m_Points > m_HighScore)
        {
            displayScore = m_Points;
            displayName = m_PlayerName;
        }

        HighScoreText.text = $"Best Score : {displayScore} | {displayName}";
    }

    public void SaveScore()
    {
        SaveData data = new SaveData
        {
            HighScore = m_HighScore,
            HighScorePlayerName = m_HighScorePlayerName
        };

        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        try
        {
            File.WriteAllText(path, json);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Не удалось сохранить файл рекорда: {e.Message}");
        }
    }

    public void LoadScore()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        if (!File.Exists(path))
        {
            return;
        }

        try
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_HighScore = data.HighScore;
            m_HighScorePlayerName = data.HighScorePlayerName;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Не удалось загрузить файл рекорда: {e.Message}");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public int HighScore;
    public string HighScorePlayerName;
}
