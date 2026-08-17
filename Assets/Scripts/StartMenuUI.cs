using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    public InputField NameInputField;

    public void StartGame()
    {
        string enteredName = "";

        if (NameInputField != null)
        {
            enteredName = NameInputField.text.Trim();
        }

        if (string.IsNullOrEmpty(enteredName))
        {
            GameData.PlayerName = "Player";
        }
        else
        {
            GameData.PlayerName = enteredName;
        }

        // Загружаем основную игровую сцену
        // Если ваша игровая сцена называется не "Main", замените имя
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
