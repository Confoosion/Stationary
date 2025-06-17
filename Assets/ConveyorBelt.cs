using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int conveyorLvl = GameManager.Singleton.GetConveyorLevel();
        if (conveyorLvl == 1)
        {
            conveyorSpeed = 5f;
        }
        else if (conveyorLvl == 2)
        {
            conveyorSpeed = 8f;
        }
        else if (conveyorLvl == 3)
        {
            conveyorSpeed = 11f;
        }
        else
        {
            conveyorSpeed = 14f;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        collider.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(1f, 0f) * conveyorSpeed, collider.transform.position);
    }
}
