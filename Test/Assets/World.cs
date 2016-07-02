using UnityEngine;
using System.Collections;


public class World
{

    private Personnage player;
    public Plateform[] platforms { get; set; }

    public Virus virus { get; private set; }


    public Personnage getPlayer()  { return player; }


    public void update(float dt) { // input en entré
        player.update(dt, this);
        virus.update(dt, this);

    }


    public World(Personnage p, Plateform[] pla, Virus v)
    {
 
        player = p;
        platforms = pla;
        virus = v;
    }


};

	
