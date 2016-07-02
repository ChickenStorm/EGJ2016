using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class World
{
    public bool hasLoos=false;
    private Personnage player;
    public List<Plateform> platforms { get; set; }
    public bool hasWin { get; set; }
    public Virus virus { get; set; }
    public List<Biles> billes;
    public Image billeModel;
    public GameObject billeP;
    public Personnage getPlayer()  { return player; }

    public float timer=0;
    public float maxTime = 3 *60;

    public void update(float dt) { // input en entré
        
        player.update(dt, this);
        virus.update(dt, this);
        for (int i = 0; i < platforms.Count; ++i)
        {
            platforms[i].update(dt,this);
        }
        for (int i = 0; i < billes.Count; ++i)
        {
            billes[i].update(dt, this);
        }

        timer += dt;
        if (timer > maxTime) {
            hasLoos = true;
        }
    }


    public World(Personnage p, List<Plateform> pla, Virus v,List<Biles> b,Image billeModelP, GameObject billePP)
    {
 
        player = p;
        platforms = pla;
        virus = v;
        billes = b;
        billeModel = billeModelP;
        billeP = billePP;
    }


};

	
