using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public int level;
    public bool locked = true;
    public bool falling = false;
    public Rigidbody2D rigidBody;

    public Color[] colors = {
        new Color(1, 0, 0, 1),
        new Color(0, 1, 0, 1),
        new Color(0, 0, 1, 1),
        new Color(1, 0, 1, 1),
        new Color(1, 1, 0, 1),
        new Color(0, 1, 1, 1),
    };
    // Start is called before the first frame update
    
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        if(locked) {
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigidBody.simulated = false;
        }
    }

    public void redraw() {
        float scale = 0.5F + 0.3F * level;
        this.gameObject.transform.localScale = new Vector3(scale, scale, 0);
        //Set the GameObject's Color quickly to a set Color (blue)
        if(level < colors.Length) {
            GetComponent<SpriteRenderer>().color = colors[level];
        }
        // float mass = 1 * Math.pow(1.2, level);
    }
    
    void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("shell")) {
            if(!falling) {
                // Debug.Log("Collision");
                if(collision.gameObject.GetComponent<Shell>().level == level) {
                    level++;
                    redraw();
                    Destroy(collision.gameObject);
                }
            }
        }
        else if (collision.gameObject.tag.Equals("end") && falling) { 
            Debug.Log("End");
        }
        falling = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(locked == false) {
            rigidBody.constraints = RigidbodyConstraints2D.None;
            rigidBody.simulated = true;
        }   
    }
}
