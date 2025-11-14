using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TP : MonoBehaviour
{
    public string sceneName;
    public TMP_Text interactText;
    public KeyCode key = KeyCode.F;

    private bool canSwitch = false;

    void Start()
    {
        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (canSwitch && Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // สำคัญ! 2D ต้องใช้ OnTriggerEnter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSwitch = true;
            interactText.text = "Press F to respond";
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSwitch = false;
            interactText.gameObject.SetActive(false);
        }
    }
}
