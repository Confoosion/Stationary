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
    
    void Start()
    {
        InitializeBD(Colors_BD, Resources.LoadAll<GameObject>("Colors"));
        InitializeBD(Shapes_BD, Resources.LoadAll<GameObject>("Shapes"));
        InitializeBD(Tags_BD, Resources.LoadAll<GameObject>("Tags"));
        InitializeBD(TapeDesigns_BD, Resources.LoadAll<GameObject>("TapeDesigns"));
    }

    private void InitializeBD(Dictionary<string, GameObject> dictionary, GameObject[] resourceArray)
    {
        for (int i = 0; i < resourceArray.Length; i++)
        {
            string BD_name = resourceArray[i].name;
            BD_name = BD_name.Replace("_BD", "");

            Debug.Log("Adding: " + BD_name + " to Resources!");

            dictionary.Add(BD_name, resourceArray[i]);
        }
    }

}
