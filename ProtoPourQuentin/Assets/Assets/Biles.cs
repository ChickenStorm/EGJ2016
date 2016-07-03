using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Biles : Entity
{
    float timer;
    const float maxTime = 30;
    Vector3 deplacementCible= new Vector3(0,0,0);
    AudioSource pickUpAudio;

    public float value { get; set; }
    float scale;

    public Biles(Vector3 pos, Vector3 vitesse, Vector3 dimension, Sprite s, Image image, float valueP, Animation anim, AudioSource pickUpAudioP) : base(pos, dimension, vitesse, true, s, image, anim)
    {

        value = valueP;
        scale = Screen.width / 1600.0f;
        im.rectTransform.sizeDelta *= scale;
        pickUpAudio = pickUpAudioP;
    }

    public override void update(float dt, World w)
    {
        base.update(dt, w);
        timer += dt;

        if (timer > maxTime) {
            isActive = false;
            position = new Vector3(-1000, 0, 0);
        }

        if (isActive && Mathf.Abs(w.getPlayer().position.x - this.position.x) < base.dimension.x+ w.getPlayer().dimension.x-10 && Mathf.Abs(w.getPlayer().position.y - this.position.y) < base.dimension.y + w.getPlayer().dimension.y-10)
        {
            //Debug.Log("getBille");
            pickUpAudio.PlayOneShot(pickUpAudio.clip, 0.4f);
            isActive = false;
            position = new Vector3(-1000, 0, 0);
            w.getPlayer().facteurVitesse += value;
        }
        if (isActive)
        {
            deplacer(dt);
            foreach (Plateform p in w.platforms)    
            {
                collision(p);
            }
            validerDeplacement();
        }

        

    }

    public void validerDeplacement()
    {
        position += deplacementCible;
    }

    public void deplacer(float dt)
    {
        deplacementCible = new Vector3();
       
        vitesse -= new Vector3(0, 1, 0)*30*dt;
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
