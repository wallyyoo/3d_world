
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheck;
    public float maxCheckDistance;
    public LayerMask layerMask; 
    
    public GameObject curInteractObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI text;
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - lastCheck > checkRate)
        {
            lastCheck = Time.time;
        }
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
       
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red);
        
        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            Debug.Log($"[RayHit] {hit.collider.gameObject.name}");
            if (hit.collider.gameObject != curInteractObject)
            {
                curInteractObject = hit.collider.gameObject;
                curInteractable = curInteractObject.GetComponent<IInteractable>();
           
                if (curInteractable != null)
                {
             
                SetText();
                }
                
                else
                {   
                
                text.gameObject.SetActive(false);
                }
            }

        }
        else
        {
           
            curInteractObject = null;
            curInteractable = null;
            text.gameObject.SetActive(false);
        }
    }

    private void SetText()
    {
        text.gameObject.SetActive(true);
        text.text = curInteractable.GetInteractText();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractObject = null;
            curInteractable = null;
            text.gameObject.SetActive(false);
        }
    }
    
}
