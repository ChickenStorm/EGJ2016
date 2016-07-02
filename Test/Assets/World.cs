using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class World
{

    private Personnage player;
    public List<Plateform> platforms { get; set; }
    public bool hasWin { get; set; }
    public Virus virus { get; private set; }
    


    public Personnage getPlayer()  { return player; }


    public void update(float dt) { // input en entré
        player.update(dt, this);
        virus.update(dt, this);
        for (int i = 0; i < platforms.Count; ++i)
        {
            platforms[i].update(dt,this);
        }

    }


    public World(Personnage p, List<Plateform> pla, Virus v)
    {
 
        player = p;
        platforms = pla;
        virus = v;
    }


};

	
