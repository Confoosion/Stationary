using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        collider.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(1f, 0f) * conveyorSpeed, collider.transform.position);
    }
}
