using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cainos.LucidEditor;
using TMPro;

namespace Cainos.PixelArtPlatformer_VillageProps
{
    public class Chest : MonoBehaviour
    {
        public TMP_Text interactText;
        [FoldoutGroup("Reference")]
        public Animator animator;

        [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                animator.SetBool("IsOpened", isOpened);

                if (isOpened)
                    SpawnItem(); // เรียกปล่อยไอเท็มเมื่อเปิดกล่อง
            }
        }
        private bool isOpened;

        [FoldoutGroup("Runtime"), Button("Open"), HorizontalGroup("Runtime/Button")]
        public void Open()
        {
            IsOpened = true;
        }

        [FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
        public void Close()
        {
            IsOpened = false;
        }

        [Header("Interaction")]
        
        private bool canInteract = false;

        [Header("Item Settings")]
        public GameObject itemPrefab; // Prefab ของไอเท็ม
        public Transform spawnPoint;  // จุดเกิดของไอเท็ม (ถ้าไม่กำหนดจะใช้ตำแหน่ง Chest)
        public float spawnForce = 5f; // ความแรงตอนเด้งออก
        public float randomAngle = 30f; // มุมสุ่มกระเด้งออก

        private void Start()
        {
            if (interactText != null)
                interactText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (canInteract && Input.GetKeyDown(KeyCode.F) && !IsOpened)
            {
                Open();
                if (interactText != null)
                    interactText.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !IsOpened)
            {
                canInteract = true;
                if (interactText != null)
                {
                    interactText.text = "Press F to open";
                    interactText.gameObject.SetActive(true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                canInteract = false;
                if (interactText != null)
                    interactText.gameObject.SetActive(false);
            }
        }

        // ฟังก์ชันปล่อยไอเท็มเด้งออก
        private void SpawnItem()
        {
            if (itemPrefab != null)
            {
                Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
                GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

                // เพิ่ม Rigidbody2D ถ้ายังไม่มี
                Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
                if (rb == null)
                    rb = item.AddComponent<Rigidbody2D>();

                rb.gravityScale = 1; // เปิดแรงโน้มถ่วง
                rb.mass = 1;

                // สุ่มมุมและแรงเด้งออก
                float angle = Random.Range(-randomAngle, randomAngle);
                Vector2 force = Quaternion.Euler(0, 0, angle) * Vector2.up * spawnForce;
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
