﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;





public class Virus : Entity
{
    private float dropTime;
    private Image bille;
    private float internalTime;
    private Animation billeAnim;
    private Vector3 intialPos;
    private AudioSource pickUpAudio;

    private const float maxDistStop = 1500;
    private const float distRepriseForward = 1000;
    private bool isWaiting = false;

    public void setToInitialPosWithOfSet(int death,Personnage p) {
        position = new Vector3(p.position.x + +death * 300 + 200, intialPos.y , 0 );
    }

    public Virus(Vector3 pos, Vector3 vitesse, Vector3 dimension, Sprite s,Image image,float dropTimeP, Image billeP, Animation anim, Animation animB,AudioSource pickUpAudioP) : base(pos, dimension, vitesse, false, s,image,anim) {
        dropTime = dropTimeP;
        bille = billeP;
        billeAnim = animB;
        intialPos = pos;
        pickUpAudio = pickUpAudioP;

    }

    private void dropBille(World w) {
        Image b = Object.Instantiate(w.billeModel);
        b.transform.SetParent(w.billeP.transform);

        System.Random hasard = new System.Random();
        float h1 = hasard.Next(-10, 11);
        float h2 = hasard.Next(-10, 11);

        w.billes.Add(new Biles(position, new Vector3(h1, h2, 0), new Vector3(50, 50, 0), Resources.Load<Sprite>("DSC02576"), b, 6, billeAnim, pickUpAudio));
    }


    public override void update(float dt, World w) {
        //anim.update(dt);
        //im.sprite = anim.image;

        if (w.virus.position.x - w.getPlayer().position.x > maxDistStop) {
            isWaiting = true;
        }
        if (isWaiting && w.virus.position.x - w.getPlayer().position.x < distRepriseForward) {
            isWaiting = false;
        }

        internalTime += dt;
        //Debug.Log(internalTime);
        if (internalTime > dropTime) {
            if (!isWaiting)
            {
                dropBille(w);
            }
            internalTime -= dropTime;
        }

        anim.update(dt);
        im.sprite = anim.image;
        im.rectTransform.position = position; //- new Vector3(+40,-40,0) ;
        if (!isStatique && ! isWaiting)
        {
            position += vitesse;
        }

        //base.update(dt,w);
        //position += vitesse * dt;

        //base.im.rectTransform.position = position;
        //im.rectTransform. = dimension;

        // TODO mettre les dims 

        if (Mathf.Abs(w.getPlayer().position.x - this.position.x) < (base.dimension.x + w.getPlayer().dimension.x)/2f && Mathf.Abs(w.getPlayer().position.y - this.position.y) < (base.dimension.y + w.getPlayer().dimension.y)/2f) {
            w.hasWin = true;
            //Sce
        }
        
    }
    




}
