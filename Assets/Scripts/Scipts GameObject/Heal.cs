using UnityEngine;

public class Heal : MonoBehaviour
{
    public int healAmount = 50;  // จำนวน HP ที่จะฟื้นให้ player
    public Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();

        if (player != null)
        {
            // เรียกฟังก์ชัน Heal ใน Player
            player.Heal(healAmount);

            // เล่นอนิเมชันหรือเอฟเฟกต์ก่อนหายไป (เช่น แสงหรือเสียง)
            if (anim != null)
            {
                anim.SetTrigger("collected");
            }

            // ทำลายวัตถุหลังจาก 0.2 วินาที (เพื่อให้อนิเมชันเล่นได้)
            Destroy(gameObject, 0.2f);
        }
    }
}
