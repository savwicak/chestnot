using UnityEngine;
using System;

public class BelajarDelegate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public delegate void AksiDelegate();

    private void Start()
    {
        CobaDelegate1();
        CobaDelegate2();
        CobaDelegate3();
    }

    void CobaDelegate1()
    {
        AksiDelegate halo = PanggilHallo;
        halo();
    }

    void CobaDelegate2()
    {
        AksiDelegate halo = PanggilHallo;
        halo += PanggilWorld;
        halo();
    }

    void CobaDelegate3()
    {
        Action kotak = PanggilHallo;
        kotak += PanggilWorld;
        kotak();
    }

    void PanggilWorld()
    {
        Debug.Log("WORLD!");
    }

    void PanggilHallo()
    {
        Debug.Log("halo");
    }
}
