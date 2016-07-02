using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Animation
{
    string nom;
    public float timer;
    float deltatemps;
    int nombre;
    public bool hasUpdate = false;
    public Sprite image { get; private set; }
    List<Sprite> listeSprite;
    bool firstTime;

    public Animation(string nomm,  float deltatmp, int nbr)
    {
        nom = nomm;
        timer = 0;
        deltatemps = deltatmp;
        nombre = nbr;
        listeSprite = new List<Sprite>();
        listeSprite.Clear();

        firstTime = true;

    }

    public void update(float dt)
    {
        if (firstTime)
        {
            for (int i = 0; i < nombre; i++)
            {
                Sprite sptm = Resources.Load<Sprite>(nom + "/" + i);
                listeSprite.Add(sptm);
            }
            firstTime = false;

        }
        if (! hasUpdate || nombre != 1) {
            hasUpdate = true;
            timer += dt;
            image = listeSprite[(int)(timer / deltatemps) % nombre];
        }
    }
}
