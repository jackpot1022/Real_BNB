using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletVerticalPrefab;
    public GameObject bulletBlastPrefab;
    GameObject[] bullet;
    GameObject[] bulletVertical;
    GameObject[] bulletBlast;

    GameObject[] targetPool;
    // Start is called before the first frame update
    void Awake()
    {
        bullet = new GameObject[1000];
        bulletVertical = new GameObject[1000];
        bulletBlast = new GameObject[100];

        Generate();
    }

    // Update is called once per frame
    void Generate()
    {
        for(int index = 0; index < bullet.Length; index++)
        {
            bullet[index] = Instantiate(bulletPrefab);
            bullet[index].SetActive(false);
        }   

        for(int index = 0; index < bulletVertical.Length; index++)
        {
            bulletVertical[index] = Instantiate(bulletVerticalPrefab);
            bulletVertical[index].SetActive(false);
        }

        for(int index = 0; index < bulletBlast.Length; index++)
        {
            bulletBlast[index] = Instantiate(bulletBlastPrefab);
            bulletBlast[index].SetActive(false);
        }       
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "Bullet":
                targetPool = bullet;
                break;

            case "BulletVertical":
                targetPool = bulletVertical;
                break;

            case "BulletBlast":
                targetPool = bulletBlast;
                break;
        }

        for(int index = 0; index < targetPool.Length; index++)
        {
            if(!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch(type)
        {
            case "Bullet":
                targetPool = bullet;
                break;

            case "BulletVertical":
                targetPool = bulletVertical;
                break;

            case "BulletBlast":
                targetPool = bulletBlast;
                break;
        }

        return targetPool;
    }
}
