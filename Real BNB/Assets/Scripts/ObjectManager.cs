using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletVerticalPrefab;
    GameObject[] bullet;
    GameObject[] bulletVertical;

    GameObject[] targetPool;
    // Start is called before the first frame update
    void Awake()
    {
        bullet = new GameObject[1000];
        bulletVertical = new GameObject[1000];

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
        }

        return targetPool;
    }
}
