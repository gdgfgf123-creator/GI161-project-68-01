using UnityEngine;
using UnityEngine.UI; // ถ้าใช้ TextMeshPro ให้เปลี่ยนเป็น using TMPro;
using TMPro;
public class GameOverUI : MonoBehaviour
{
    public TMP_Text coinText; // ต้องเป็น TMP_Text 

    void Start()
    {
        // รับค่า Coin จาก PlayerPrefs
        int coinCount = PlayerPrefs.GetInt("CoinCount", 0);

        // แสดงบน UI
        
        coinText.text = "Coins Collected: " + coinCount;
    }
}
