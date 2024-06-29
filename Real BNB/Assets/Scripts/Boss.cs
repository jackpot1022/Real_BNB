using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
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
        }

    }

    void FireVertical() //직선으로 발사하는 함수
    {
        Debug.Log("hi");
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
        curPatternCount++;

        if(curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 2);
        }
        else
        {
            Invoke("BNB", 3);
        }
    }
}
