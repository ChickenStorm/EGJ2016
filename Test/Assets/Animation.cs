using UnityEngine;
using System.Collections;

public class Animation
{
    string nom;
    float timer;
    float deltatemps;
    int nombre;
    public Sprite image { get; private set; }

    public Animation(string nomm,  float deltatmp, int nbr)
    {
        nom = nomm;
        timer = 0;
        deltatemps = deltatmp;
        nombre = nbr;
    }

    public void update(float dt)
    {
        timer += dt;
        image = Resources.Load<Sprite>(nom + "_" +  (int) (timer / deltatemps) % nombre);
        Debug.Log((int)(timer / deltatemps) % nombre);
    }
}
