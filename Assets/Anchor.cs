using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public bool IsCollided;

    void Ray(Vector2 origin, Vector2 dir)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(origin, dir, 1);
        if(hit2D.collider != null)
        {
            IsCollided = true;
        }
        Debug.DrawRay(origin, dir, Color.black);
    }

    void Start()
    {
        Vector2 Pos;
        switch (this.name)
        {
            case "Anchor right":
                Pos = new Vector2(this.transform.position.x + this.transform.localScale.x / 2 + 0.1f, this.transform.position.y);
                Ray(Pos,Vector2.right);
                break;
            case "Anchor left":
                Pos = new Vector2(this.transform.position.x - this.transform.localScale.x / 2 - 0.1f, this.transform.position.y);
                Ray(Pos, Vector2.left);
                break;
            case "Anchor top":
                Pos = new Vector2(this.transform.position.x, this.transform.position.y + this.transform.localScale.y / 2 + 0.1f);
                Ray(Pos, Vector2.up);
                break;
            case "Anchor bottom":
                Pos = new Vector2(this.transform.position.x, this.transform.position.y - this.transform.localScale.y / 2 - 0.1f);
                Ray(Pos, Vector2.down);
                break;
        }
       
    }
}
