using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public InputField NameInputField;

    // Ссылки на панели UI в инспекторе
    public GameObject MenuPanel;
    public GameObject GamePanel;

    public void StartGame()
    {
        // Сохраняем имя в статическую переменную MainManager
        if (NameInputField != null && !string.IsNullOrEmpty(NameInputField.text))
        {
            MainManager.CurrentPlayerName = NameInputField.text;
        }
        else
        {
            MainManager.CurrentPlayerName = "Player";
        }

        // Скрываем меню и показываем игровой UI
        if (MenuPanel != null) MenuPanel.SetActive(false);
        if (GamePanel != null) GamePanel.SetActive(true);
    }
}
