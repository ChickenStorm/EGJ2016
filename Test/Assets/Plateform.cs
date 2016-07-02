using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Plateform : Entity{

    public Plateform(Vector3 pos, Vector3 dim,Image im,Animation anim) : base (pos, dim, new Vector3(), true, new Sprite(),im,anim)
    {

    }
    public override void update(float dt, World w)
    {
        base.update(dt, w);

    }
}
