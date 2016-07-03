using UnityEngine;
using System.Collections;

public class Animation
{
    string nom;
    public float timer;
    float deltatemps;
    int nombre;
    public bool hasUpdate = false;
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
        
        if (! hasUpdate || nombre != 1) {
            hasUpdate = true;
            timer += dt;
            image = Resources.Load<Sprite>(nom + "/" + (int)(timer / deltatemps) % nombre);
        }
    }
}
