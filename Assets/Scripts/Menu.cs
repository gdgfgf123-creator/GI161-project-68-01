using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadedScene()
    {
        // ดึงชื่อ Scene ก่อนตายที่บันทึกไว้
        string lastScene = PlayerPrefs.GetString("LastScene", "");

        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.LogWarning("LastScene not found! Load scene failed.");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
