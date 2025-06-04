using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Singleton { get; private set; }

    public Dictionary<string, GameObject> Colors_BD = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> Shapes_BD = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> Tags_BD = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> TapeDesigns_BD = new Dictionary<string, GameObject>();
    // public GameObject[] Colors_BD;
    // public GameObject[] Shapes_BD;
    // public GameObject[] Tags_BD;
    // public GameObject[] TapeDesigns_BD;

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
        InitializeBD(Colors_BD, Resources.LoadAll<GameObject>("Colors"));
        InitializeBD(Shapes_BD, Resources.LoadAll<GameObject>("Shapes"));
        InitializeBD(Tags_BD, Resources.LoadAll<GameObject>("Tags"));
        InitializeBD(TapeDesigns_BD, Resources.LoadAll<GameObject>("TapeDesigns"));
        // Colors_BD = Resources.LoadAll<GameObject>("Colors");
        // Shapes_BD = Resources.LoadAll<GameObject>("Shapes");
        // Tags_BD = Resources.LoadAll<GameObject>("Tags");
        // TapeDesigns_BD = Resources.LoadAll<GameObject>("TapeDesigns");
    }

    private void InitializeBD(Dictionary<string, GameObject> dictionary, GameObject[] resourceArray)
    {
        for (int i = 0; i < resourceArray.Length; i++)
        {
            string BD_name = resourceArray[i].name;
            BD_name = BD_name.Replace("_BD", "");

            dictionary.Add(BD_name, resourceArray[i]);
        }
    }

}
