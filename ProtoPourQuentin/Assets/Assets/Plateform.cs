using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Plateform : Entity{

    public bool isSchaky { get; set; }

    private float timer;
    private Vector3 posInit;

    private const float amplitudeX = 10f;
    private const float amplitudeY = 0f; 
    private const float dephasage = 0f; // en radiant

    private const float periodeX = 0.1f; // en sec
    private const float periodeY = 0.5f; // en sec

    public Plateform(Vector3 pos, Vector3 dim,Image im,Animation anim,bool isSchakyP = false) : base (pos, dim, new Vector3(), true, new Sprite(),im,anim)
    {
        isSchaky = isSchakyP;
        posInit = pos;
        timer = 0;

    }
    public override void update(float dt, World w)
    {
        if (isSchaky) {
            timer += dt;
            position = posInit + new Vector3(Mathf.Sin(timer / periodeX * Mathf.PI * 2f)*amplitudeX, Mathf.Sin(timer / periodeY * Mathf.PI * 2f+dephasage) * amplitudeY, 0);
            //w.getPlayer().position = new Vector3(w.getPlayer().vitesse.x,)
        }
        base.update(dt, w);

    }
}
