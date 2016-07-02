using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Entity {
    public Vector3 position { get; set; }
    public Vector3 dimension { get; set; }
    public Vector3 vitesse { get; set; }
    public bool isStatique { get; set; }
    public Sprite image { get; set; }
    public Image im { get; set; }

    public Entity(Vector3 pos, Vector3 dim, Vector3 vit, bool isstat, Sprite im,Image impara)
    {
        vitesse = vit;
        position = pos;
        isStatique = isstat;
        image = im;
        dimension = dim;
    }
    public Entity()
    {
        vitesse = new Vector3(0, 0, 0);
        position = new Vector3(0, 0, 0);
        //image = new Sprite();
        isStatique = false;
        dimension = new Vector3();
    }


    public void update(float dt) {

        position += vitesse;
        // TODO update


    }



    
}
