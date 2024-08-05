using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class GameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public Shell initialShell;
    public Shell activeObject;
    public GameObject pointer;
    public float speed = 8.0F;
    public int delay;
    void Start()
    {
        activeObject = initialShell;
        activeObject.redraw();
        // activeObject.transform.parent.gameObject.SetActive(true);
    }

    public async Task wait(int sec)
    {
         await Task.Delay(TimeSpan.FromSeconds(sec));
    }   

    public IEnumerator Iwait() {
        yield return new WaitForSeconds(5);
    }

    public async void release() {
        activeObject.locked = false;
        activeObject.falling = true;
        activeObject = Instantiate(activeObject, new Vector3(0F, 3.818466F, 0F), Quaternion.identity);
        pointer.transform.position = new Vector3(0F, -0.5F, 0F);
        activeObject.GetComponent<Shell>().level = UnityEngine.Random.Range(0, 3);
        activeObject.redraw();
        activeObject.locked = true;
        activeObject.falling = false;
        await wait(delay);
        StartCoroutine(Iwait());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            release();
            // await wait(delay);
            StartCoroutine(Iwait());
        }
        if(activeObject.locked) {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if((activeObject.transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f)).x < 3.29) {
                    activeObject.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                    pointer.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if((activeObject.transform.position - new Vector3(speed * Time.deltaTime, 0f, 0f)).x > -3.29) {
                    activeObject.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
                    pointer.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
                }
            }
        }
    }
}
