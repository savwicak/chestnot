using UnityEngine;

[CreateAssetMenu(fileName =  "ZombieConfig", menuName = "Enemy/ZombieConfig")]
public class ZombieConfig : ScriptableObject
{
    public int hp = 100;
    public float kecepatan = 2f;
    public float jarakDeteksi = 6f;
    public float jarakSerang = 1.2f;
}
