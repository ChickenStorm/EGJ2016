using UnityEngine;
using System.Collections;
using UnityEngine.UI;




public class Virus : Entity
{
    public Virus(Vector3 pos, Vector3 vitesse, Vector3 dimension, Sprite s,Image image) : base(pos, dimension, vitesse, false, s,image) { }


    public override void update(float dt, World w) {
        
        //base.update(dt,w);
        base.im.rectTransform.position = position;
        //im.rectTransform. = dimension;
        if (Mathf.Abs(w.getPlayer().position.x - this.position.x) < 100  && Mathf.Abs(w.getPlayer().position.y - this.position.y) < 100) {
            w.hasWin = true;
            Debug.Log("win");
        }
        
    }
    




}
