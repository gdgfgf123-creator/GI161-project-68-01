using UnityEngine;
using UnityEngine.UI;
public class player : Character, IShootable
{
    [field : SerializeField]public GameObject Bullet { get; set; }
    [field : SerializeField]public Transform ShootPoint { get; set ; }
    public float ReloadTime { get ; set ; }
    public float WaitTime { get ; set; }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        HP.maxValue = 100;
        base.Intialize(100);
        ReloadTime = 1.0f;
        WaitTime = 1.0f;
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
   
        }
    }

}
