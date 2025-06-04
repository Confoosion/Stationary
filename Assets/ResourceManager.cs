using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Singleton { get; private set; }

    public GameObject[] Colors_BD;
    public GameObject[] Shapes_BD;
    public GameObject[] Tags_BD;
    public GameObject[] TapeDesigns_BD;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Colors_BD = Resources.LoadAll<GameObject>("Colors");
        Shapes_BD = Resources.LoadAll<GameObject>("Shapes");
        Tags_BD = Resources.LoadAll<GameObject>("Tags");
        TapeDesigns_BD = Resources.LoadAll<GameObject>("TapeDesigns");
    }
}
