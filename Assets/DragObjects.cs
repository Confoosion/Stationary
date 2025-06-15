using UnityEngine;
using UnityEngine.EventSystems;

public class DragObjects : MonoBehaviour
{
    public static DragObjects Singleton;

    public LayerMask m_DragLayers;

    public bool invalidDrag = false;
    public bool isDragging = false;

    private Rigidbody2D draggedRB;
    private Transform draggedObject;
    public float throwPower = 20f;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    void Update()
    {
        // Calculate the world position for the mouse.
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0) && !isDragging)
        {
            // Fetch the first collider.
            // NOTE: We could do this for multiple colliders.
            var collider = Physics2D.OverlapPoint(worldPos, m_DragLayers);
            if (!collider)
            {
                invalidDrag = true;
                return;
            }

            // Clicked on Requirement Paper so no need to drag anything
            if (collider.gameObject.layer == LayerMask.NameToLayer("RequirementPaper"))
            {
                UIManager.Singleton.ShowPaperUI(collider.transform.parent.gameObject.GetComponent<Cart>());
                return;
            }

            if (collider.gameObject.layer == LayerMask.NameToLayer("PassThrough"))
                {
                    collider.gameObject.layer = LayerMask.NameToLayer("Draggable");
                }

            // Fetch the collider body.
            var body = collider.GetComponent<Rigidbody2D>();
            if (!body)
            {
                invalidDrag = true;
                return;
            }

            if (!invalidDrag)
            {
                draggedRB = body;
                draggedRB.gravityScale = 0f;

                isDragging = true;
                draggedObject = body.transform;
                draggedObject.gameObject.layer = LayerMask.NameToLayer("Dragging");
                body.linearVelocity = Vector3.zero;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            invalidDrag = false;
            if (isDragging)
            {
                isDragging = false;
                draggedObject.gameObject.layer = LayerMask.NameToLayer("Draggable");
                ThrowObject(draggedRB, worldPos);
                draggedRB.gravityScale = 1f;
                draggedRB = null;
            }
            return;
        }

        // Update the position if dragging
        if (isDragging)
        {
            draggedObject.position = Vector2.Lerp(draggedObject.position, worldPos, Time.deltaTime * 5f);
        }
    }
    
    void ThrowObject(Rigidbody2D rb, Vector3 pos)
    {
        var throwDirection = pos - rb.transform.position;
        float throwDistance = Vector3.Distance(pos, rb.transform.position);
        rb.AddForce(throwDirection * throwDistance * throwPower, ForceMode2D.Force);
    }

    public bool CheckDragging()
    {
        return (isDragging);
    }
}
