using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBlast : MonoBehaviour
{
    public int damage;
    public int spread;
    public bool doOnce;
    public ObjectManager objectManager;
    // Start is called before the first frame update

    // Update is called once per frame
    void OnEnable()
    {
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        spread = 30;
        if (this.gameObject.activeSelf)
        {
            Invoke("Blast", 3.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            gameObject.SetActive(false);
        }
    }

    void Blast()
    {
        if (this.gameObject.activeSelf)
        {
            for (int index = 0; index < spread; index++)
            {
                GameObject bullet = objectManager.MakeObj("Bullet");

                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / spread), Mathf.Sin(Mathf.PI * 2 * index / spread));
                rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

                Vector3 rotVec = Vector3.forward * 360 * index / spread + Vector3.forward * 90;
                bullet.transform.Rotate(rotVec);
            }
            gameObject.SetActive(false);
        }
    }
}
