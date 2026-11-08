using UnityEngine;

public class PenerimaEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        PemancarEvent.TekanTombol += Reaksi;
    }

    void OnDisable()
    {
        PemancarEvent.TekanTombol -= Reaksi;
    }
     void Reaksi()
    {
        Debug.Log("AKU DENGER SPASI1");
    }
}
