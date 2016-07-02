using UnityEngine;
using System.Collections;
using UnityEngine.UI;





public class Virus : Entity
{
    private float dropTime;
    private Image bille;
    private float internalTime;

    public Virus(Vector3 pos, Vector3 vitesse, Vector3 dimension, Sprite s,Image image,float dropTimeP, Image billeP) : base(pos, dimension, vitesse, false, s,image) {
        dropTime = dropTimeP;
        bille = billeP;
    }

    private void dropBille(World w) {
        Image b = Object.Instantiate(w.billeModel);
        b.transform.SetParent(w.billeP.transform);
        w.billes.Add(new Biles(position, new Vector3(0, 0, 0), new Vector3(10, 10, 0), Resources.Load<Sprite>("DSC02576"), b, 1));
    }


    public override void update(float dt, World w) {

        internalTime += dt;
        //Debug.Log(internalTime);
        if (internalTime > dropTime) {
            
            dropBille(w);
            internalTime -= dropTime;
        }

        base.update(dt,w);
        //position += vitesse * dt;

        //base.im.rectTransform.position = position;
        //im.rectTransform. = dimension;

        // TODO mettre les dims 

        if (Mathf.Abs(w.getPlayer().position.x - this.position.x) < 100  && Mathf.Abs(w.getPlayer().position.y - this.position.y) < 100) {
            w.hasWin = true;
            Debug.Log("win");
        }
        
    }
    




}
