using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;

    void Start()
    {
        // TODO: hitung jumlah koin di scene saat mulai
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;

        // TODO: jika koinTerkumpul == totalKoin, panggil Menang()
        if (koinTerkumpul == totalKoin)
        {
            Invoke(nameof(Menang), 0.1f);
            //Menang();
        }
    }

    public void Menang()
    {
        Debug.Log("KAMU MENANG!");
    }

    private void OnEnable()
    {
        Enemy.OnZombieMati += TambahSkor;
    }

    private void OnDisable()
    {
        Enemy.OnZombieMati -= TambahSkor;
    }

    void TambahSkor(Enemy DeadZombie)
    {
        koinTerkumpul += 10;
        Debug.Log("Koin : " + koinTerkumpul);
    }
}