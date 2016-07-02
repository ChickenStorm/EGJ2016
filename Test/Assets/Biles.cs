using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Biles : Entity
{

    Vector3 deplacementCible= new Vector3(0,0,0);


    public float value { get; set; }

    public Biles(Vector3 pos, Vector3 vitesse, Vector3 dimension, Sprite s, Image image, float valueP, Animation anim) : base(pos, dimension, vitesse, true, s, image, anim)
    {

        value = valueP;
    }

    public override void update(float dt, World w)
    {
        base.update(dt, w);

        if (isActive && Mathf.Abs(w.getPlayer().position.x - this.position.x) < base.dimension.x+ w.getPlayer().dimension.x-10 && Mathf.Abs(w.getPlayer().position.y - this.position.y) < base.dimension.y + w.getPlayer().dimension.y-10)
        {
            Debug.Log("getBille");
            isActive = false;
            position = new Vector3(-1000, 0, 0);
            w.getPlayer().facteurVitesse += value;
        }

        deplacer();
        foreach (Plateform p in w.platforms)
            collision(p);
        validerDeplacement();

    }

    public void validerDeplacement()
    {
        position += deplacementCible;
    }

    public void deplacer()
    {
        deplacementCible = new Vector3();
       
        vitesse -= new Vector3(0, 1, 0);
        deplacementCible += vitesse;
    }

    public void collision(Plateform platef)
    {
        if (platef != null)
        {
            Vector3 nPosition = position + deplacementCible;
            //Debug.Log(nPosition.y + ", " + dimension.x + " : " + platef.position.x + " , " + platef.dimension.x);
            if ((nPosition.x + dimension.x > platef.position.x && nPosition.x + dimension.x < platef.position.x + platef.dimension.x) || (nPosition.x < platef.position.x + platef.dimension.x && nPosition.x > platef.position.x))
            {
                //Debug.Log(nPosition);
                if (position.y < platef.position.y + platef.dimension.y && position.y + dimension.y > platef.position.y)
                {
                    // Debug.Log("ok");
                    deplacementCible.x = 0;
                    vitesse = new Vector3(-vitesse.x / 2, vitesse.y/2, vitesse.z);
                }
            }
            if ((nPosition.y + dimension.y > platef.position.y && nPosition.y + dimension.y < platef.position.y + platef.dimension.y) || (nPosition.y < platef.position.y + platef.dimension.y && nPosition.y > platef.position.y))
            {
                if (position.x < platef.position.x + platef.dimension.x && position.x + dimension.x > platef.position.x)
                {
                    deplacementCible.y = 0;
                    vitesse = new Vector3(vitesse.x/2, -vitesse.y/2, vitesse.z);
                }
            }
            //return false;
        }
        else
        {
        }
    }

}
