using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isRotate;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(isRotate)
        {
            transform.Rotate(Vector3.forward * 10);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            gameObject.SetActive(false);
        }
    }
}
