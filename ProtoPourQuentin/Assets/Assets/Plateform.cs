using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Plateform : Entity{

    public bool isSchaky { get; set; }

    private float timer;
    private Vector3 posInit;

    public float amplitudeX = 10f;
    public float amplitudeY = 0f; 
    private const float dephasageY = 0f; // en radiant
    public float dephasageX { get; set; } // en radiant
    public float periodeX; // en sec
    private const float periodeY = 1f; // en sec

    public Vector3 deplacement{get;private set;}

    public Plateform(Vector3 pos, Vector3 dim,Image im,Animation anim,float dephasageXP=0, float ampx = 10f, float frecX = 1f, bool isSchakyP = false) : base (pos, dim, new Vector3(), true, new Sprite(),im,anim)
    {
        isSchaky = isSchakyP;
        posInit = pos;
        timer = 0;
        dephasageX = dephasageXP;
        amplitudeX = ampx;
        periodeX = frecX;

    }
    public override void update(float dt, World w)
    {
        if (isSchaky) {
            //float timerPre= timer;
            Vector3 oldPos = position;
            timer += dt;
            position = posInit + new Vector3(Mathf.Sin(timer / periodeX * Mathf.PI * 2f+ dephasageX) *amplitudeX, Mathf.Sin(timer / periodeY * Mathf.PI * 2f+dephasageY) * amplitudeY, 0);
            //w.getPlayer().position = new Vector3(w.getPlayer().vitesse.x,)
            //deplacement = (new Vector3(Mathf.Sin(timer / periodeX * Mathf.PI * 2f)*amplitudeX,Mathf.Sin(timer / periodeY * Mathf.PI * 2f+dephasage) * amplitudeY,0)- new Vector3(Mathf.Sin(timerPre / periodeX * Mathf.PI * 2f) * amplitudeX, Mathf.Sin(timerPre / periodeY * Mathf.PI * 2f + dephasage) * amplitudeY, 0))/2 ;
            deplacement = (position - oldPos)/2;
        }
        base.update(dt, w);

    }
}
