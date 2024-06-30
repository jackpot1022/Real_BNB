using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Boss : MonoBehaviour
{
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    public float fireVerInterval;
    public int fireArcInterval;
    public ObjectManager objectManager;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("BNB", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BNB() //탄막쏘는 함수
    {
        patternIndex = patternIndex == 4 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch(patternIndex)
        {
            case 0:
                FireVertical();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
            case 4:
                FireBlast();
                break;
        }

    }

    void FireVertical() //직선으로 발사하는 함수
    {
        List<GameObject> bullets =new List<GameObject>();
        List<Rigidbody2D> rigids = new List<Rigidbody2D>();
        for(int i = 0; i < 20 + curPatternCount % 2; i++)
        {
            GameObject bullet = objectManager.MakeObj("BulletVertical");
            bullet.transform.position = transform.position + Vector3.right * ((fireVerInterval * (20 + curPatternCount % 2) / 2) - (i * fireVerInterval));
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.down * 4, ForceMode2D.Impulse);
        }

        
        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireVertical", 0.25f);
        }
        else
        {
            Invoke("BNB", 3);
        }
    }

    void FireShot() //플레이어 위치로 샷건을 쏘는 함수
    {
        for(int index = 0; index < 10; index++)
        {
            GameObject bullet = objectManager.MakeObj("Bullet");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(-3 + (index * 0.75f), 0f);
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 2);
        }
        else
        {
            Invoke("BNB", 3);
        }
    }

    void FireArc() //부채모양으로 발사하는 함수
    {
        GameObject bullet = objectManager.MakeObj("Bullet");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * fireArcInterval * curPatternCount/maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.025f);
        }
        else
        {
            Invoke("BNB", 3);
        }
    }

    void FireAround() //원모양으로 전체공격하는 함수
    {
        int roundNum = curPatternCount % 2 == 0 ? 50 : 40;

        for(int index = 0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("Bullet");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                                        ,Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }


        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.5f);
        }
        else
        {
            Invoke("BNB", 3);
        }
    }

    void FireBlast() //발사 후 퍼지는 탄을 쏘는 함수
    {
        GameObject bullet = objectManager.MakeObj("BulletBlast");
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = player.transform.position - transform.position;
        rigid.AddForce(dirVec.normalized * 1, ForceMode2D.Impulse);

        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireBlast", 1);
        }
        else
        {
            Invoke("BNB", 3);
        }
    }
}
