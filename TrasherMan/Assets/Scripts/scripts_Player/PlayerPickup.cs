using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Transform holdPosition;           // Empty child on player
    public float pickupRange = 5f;
    public float throwForce = 18f;           // Good force for throwing into bins
    public float rotationSpeed = 150f;

    [Header("References")]
    public GameObject player;

    private GameObject heldObj;
    private Rigidbody heldRb;
    private bool isRotating = false;

    // All 5 trash tags
    private readonly string[] trashTags =
    {
        "whatIsGarbage",
        "whatIsRecyclable",
        "whatIsCompost",
        "whatIsElectronics",
        "whatIsGlass"
    };

    void Update()
    {
        // === LEFT MOUSE CLICK - Pick Up ===
        if (Input.GetMouseButtonDown(0) && heldObj == null)   // Left click
        {
            TryPickup();
        }

        // === RIGHT MOUSE CLICK - Drop or Throw ===
        if (Input.GetMouseButtonDown(1) && heldObj != null)   // Right click
        {
            if (IsLookingAtBin())
            {
                ThrowIntoBin();
            }
            else
            {
                DropObject();           // Normal drop when not looking at bin
            }
        }

        // Rotation while holding (left shift key)
        if (heldObj != null)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRotating = true;
                RotateHeldObject();
            }
            else
            {
                isRotating = false;
                MoveToHoldPosition();
            }
        }
    }

    private void TryPickup()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (IsTrash(hit.collider.gameObject))
            {
                PickupObject(hit.collider.gameObject);
            }
        }
    }

    private bool IsTrash(GameObject obj)
    {
        if (obj == null) return false;
        foreach (string tag in trashTags)
        {
            if (obj.CompareTag(tag)) return true;
        }
        return false;
    }

    private void PickupObject(GameObject obj)
    {
        heldObj = obj;
        heldRb = obj.GetComponent<Rigidbody>();

        if (heldRb != null)
        {
            heldRb.isKinematic = true;
        }

        heldObj.transform.SetParent(holdPosition);
        heldObj.transform.localPosition = Vector3.zero;
        heldObj.transform.localRotation = Quaternion.identity;

        // Ignore collision with player
        if (player != null)
        {
            Collider pCol = player.GetComponent<Collider>();
            Collider oCol = heldObj.GetComponent<Collider>();
            if (pCol && oCol)
                Physics.IgnoreCollision(oCol, pCol, true);
        }
    }

    private void DropObject()
    {
        if (heldObj == null) return;
        ReleaseObject();
        heldObj = null;
        heldRb = null;
    }

    private void ThrowIntoBin()
    {
        if (heldObj == null || heldRb == null) return;

        ReleaseObject();

        // Throw toward where the camera is looking (great for aiming into bins)
        Vector3 throwDir = Camera.main.transform.forward;
        heldRb.AddForce(throwDir * throwForce, ForceMode.Impulse);

        heldObj = null;
        heldRb = null;
    }

    private bool IsLookingAtBin()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange + 2f))
        {
            // You can expand this later with bin tags like "whatIsBin"
            // For now, any object with "bin" in its name or specific tags
            string hitTag = hit.collider.tag;
            if (hitTag.Contains("bin") || hitTag.Contains("Bin") ||
                hitTag == "whatIsGarbage" || hitTag == "whatIsRecyclable" ||
                hitTag == "whatIsCompost" || hitTag == "whatIsElectronics" ||
                hitTag == "whatIsGlass")
            {
                return true;
            }
        }
        return false;
    }

    private void ReleaseObject()
    {
        if (heldObj == null) return;

        heldObj.transform.SetParent(null);

        if (heldRb != null)
        {
            heldRb.isKinematic = false;
        }

        if (player != null)
        {
            Collider pCol = player.GetComponent<Collider>();
            Collider oCol = heldObj.GetComponent<Collider>();
            if (pCol && oCol)
                Physics.IgnoreCollision(oCol, pCol, false);
        }
    }

    private void MoveToHoldPosition()
    {
        if (heldObj != null && holdPosition != null)
        {
            heldObj.transform.position = holdPosition.position;
            heldObj.transform.rotation = holdPosition.rotation;
        }
    }

    private void RotateHeldObject()
    {
        if (heldObj == null) return;

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        heldObj.transform.Rotate(Vector3.up, -mouseX, Space.World);
        heldObj.transform.Rotate(Camera.main.transform.right, mouseY, Space.World);
    }

    private void OnDrawGizmosSelected()
    {
        if (Camera.main != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * pickupRange);
        }
    }
}