using Cainos.PixelArtPlatformer_VillageProps;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class player : Character, IShootable
{
    
    [field : SerializeField]public GameObject Bullet { get; set; }
    [field : SerializeField]public Transform ShootPoint { get; set ; }
    public float ReloadTime { get ; set ; }
    public float WaitTime { get ; set; }

    public float dashSpeed = 50f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private float dashTimer;
    [SerializeField]private TrailRenderer trail;
    [SerializeField] private ParticleSystem GunFireVFX;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        HP.maxValue = 1000;
        base.Intialize(1000);
        ReloadTime = 1.0f;
        WaitTime = 1.0f;

        // ถ้ามี TrailRenderer ในตัว ให้เก็บไว้ใช้
        //trail = GetComponent<TrailRenderer>();
        if (trail != null)
            trail.emitting = false; // ปิดไว้ก่อน
    }

    public void OnHitWith(Enemy enemy)
    {
        anim.SetTrigger("hurt");
        TakeDamage(enemy.DamgeHit);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null){
            OnHitWith(enemy);
        }
    }
    
    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Shoot();
        WaitTime += Time.deltaTime;
        HP.value = Health;

        // เรียก Dash ด้วยปุ่ม Shift (หรือปรับได้)
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time > dashTimer)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        dashTimer = Time.time + dashCooldown;
        anim.SetBool("isRun", false);
        // เปิด trail ตอน dash
        if (trail != null)
            trail.emitting = true;

        float originalGravity = rd.gravityScale;

        // ปิดแรงโน้มถ่วงชั่วคราว (กันตก)
        rd.gravityScale = 0;
        rd.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);

        // รอ dashTime วินาที
        yield return new WaitForSeconds(dashTime);

        // ปิด trail แล้วกลับสู่ปกติ
        if (trail != null)
            trail.emitting = false;

        rd.gravityScale = originalGravity;
        rd.velocity = Vector2.zero;
        isDashing = false;

    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && WaitTime > ReloadTime)
        {
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
            Banana banana = bullet.GetComponent<Banana>();
            if (banana != null)
                banana.InitWeapon(20,this);
            WaitTime = 0.0f;
            anim.SetTrigger("attack");
            GunFireVFX.Play();
        }
    }
    internal void Heal(int healAmount)
    {
        Health += healAmount;

        // จำกัดไม่ให้เกินค่าสูงสุด (ถ้ามีระบบ max health)
        if (Health > HP.maxValue)
            Health = (int)HP.maxValue;

        Debug.Log($"{this.name} healed by {healAmount}. Current Health: {this.Health}");
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Chest") && Input.GetKeyDown(KeyCode.E))
        {
            Chest chest = other.GetComponent<Chest>();
            if (chest != null)
            {
                chest.Open();  // เปิดกล่อง
            }
        }
    }
}
