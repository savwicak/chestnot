using UnityEngine;
using System;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    public float ms = 2f;
    protected Transform player;

    [Header("Pengaturan State macin")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;

    private StateZombie satet = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    public static event Action<Enemy> OnZombieMati;
    [SerializeField] private UnityEvent onZombieDeadVisual;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {

        PeriksaTransisi();

        switch (satet)
        {
            case StateZombie.IDLE: perilakuIdle(); break;
            case StateZombie.PATROL: perilakuPatrol(); break;
            case StateZombie.ATTACK: perilakuAttack(); break;
            case StateZombie.CHASE: perilakuChase(); break;
        }
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang)
            satet = StateZombie.ATTACK;
        else if (jarak <= jarakDeteksi)
            satet = StateZombie.CHASE;
        else
            satet = StateZombie.PATROL;

    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, HP sisa: {hp}");

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log($"{gameObject.name} mati!");
        //OnZombieMati?.Invoke(this);
        onZombieDeadVisual?.Invoke();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Serang();
    }

    void perilakuIdle()
    {
        Debug.Log("Idle");
    }

    void perilakuPatrol()
    {
        Debug.Log("Patroli");
    }
    
    void perilakuAttack()
    {
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        } 
    }

    void perilakuChase()
    {
        Kejar();
    }
}