using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Biles : Entity
{

    public float value { get; set; }

    public Biles(Vector3 pos, Vector3 vitesse, Vector3 dimension, Sprite s, Image image, float valueP) : base(pos, dimension, vitesse, true, s, image)
    {

        value = valueP;
    }

    public override void update(float dt, World w)
    {
        base.update(dt, w);

        if (isActive && Mathf.Abs(w.getPlayer().position.x - this.position.x) < base.dimension.x+ w.getPlayer().dimension.x && Mathf.Abs(w.getPlayer().position.y - this.position.y) < base.dimension.y + w.getPlayer().dimension.y)
        {
            Debug.Log("getBille");
            isActive = false;
            position = new Vector3(-1000, 0, 0);
            w.getPlayer().facteurVitesse += value;
        }

    }

}
