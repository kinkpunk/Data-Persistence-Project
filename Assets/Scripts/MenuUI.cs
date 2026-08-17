using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUI : MonoBehaviour
{
    public TMP_InputField NameInputField;

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

        // Если ваша игровая сцена называется не Main, замените имя сцены
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
