using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
Vector2 direction;
bool directionChanged;
Vector2 startPosition;
public GameLoop gl;

public float screenOffset;
public float screenFactor;

public enum directions {left, right, up, down};
public directions dir;

void start() {
    Input.simulateMouseWithTouches = true;
}

void Update() {
   if (Input.touchCount > 0) {
     Touch touch = Input.GetTouch(0);
      switch (touch.phase) {

       case TouchPhase.Began:
         startPosition = touch.position;
          // Handle the start of the touch (e.g., record initial position).
          break;

        case TouchPhase.Moved:
         direction = touch.position - startPosition;
         if((touch.position.x / Screen.width / screenFactor) + screenOffset < 3.29 && (touch.position.x / Screen.width / screenFactor) + screenOffset > -3.29) {
            gl.activeObject.transform.position = new Vector3((touch.position.x / Screen.width / screenFactor) + screenOffset, 3.818466F, 0f);
            gl.pointer.transform.position = new Vector3((touch.position.x / Screen.width / screenFactor) + screenOffset, -0.5f, 0f);
         }
         if(direction.x < 0) {
            // Debug.Log("Left");
            // gl.activeObject.transform.position = new Vector3(direction.x, 0f, 0f);
            // gl.pointer.transform.position = new Vector3(direction.x, 0f, 0f);
            dir = directions.left;
         }
         else if(direction.x > 0) {
            // Debug.Log("Right");
            // gl.activeObject.transform.position += new Vector3(gl.speed * Time.deltaTime, 0f, 0f);
            // gl.pointer.transform.position += new Vector3(gl.speed * Time.deltaTime, 0f, 0f);
            dir = directions.right;
         }
         else if(direction.y < 0) {
            // Debug.Log("Down");
            // gl.release();
            dir = directions.down;
         }
         else if(direction.y > 0) {
            // Debug.Log("Up");
            dir = directions.up;
         }
         // Handle touch movement (e.g., drag an object, get dorection of drag).
         break;

        case TouchPhase.Stationary:
           // Handle a stationary touch (e.g., long-press actions).
           break;

        case TouchPhase.Ended:
          directionChanged = true;
        //   if(dir == directions.down) {
        //     gl.release();
        //   }
          // Handle the end of the touch (e.g., release a dragged object).
           break;
         }
     }
}
}
