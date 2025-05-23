using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [Header("Movement")] 
   public float moveSpeed;
   private Vector2 curMovementInput;
   public float jumpPower;
   public LayerMask groundLayerMask;

   [Header("Look")]
   public Transform cameraContainer;
   public float minXLook;
   public float maxXLook;
   private float camCurXRot;
   public float lookSensitivity;

   private Vector2 mouseDelta;

   public Action inventory;
   [HideInInspector]
   public bool canLook = true;
   private Rigidbody rb;

   private void Awake()
   {
      rb = GetComponent<Rigidbody>();
   }

   void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
   }
   
   private void FixedUpdate()
   {
      Move();
    
   }

   private void LateUpdate()
   {
      if(canLook)
      {
         CameraLook();
      }
   }

   public void OnLookInput(InputAction.CallbackContext context)
   {
      mouseDelta = context.ReadValue<Vector2>();
   }

   public void OnMoveInput(InputAction.CallbackContext context)
   {
      if (context.phase == InputActionPhase.Performed)
      {
         curMovementInput = context.ReadValue<Vector2>();
      }
      else if (context.phase == InputActionPhase.Canceled)
      {
         curMovementInput = Vector2.zero;
      }
   }

   public void OnJumpInput(InputAction.CallbackContext context)
   {
    //  if (context.phase == InputActionPhase.Started && IsGrounded())
    if (context.phase == InputActionPhase.Started)
      {
         Debug.Log("Jump pressed. Grounded :" +IsGrounded());
         //rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
         if (IsGrounded())
         {
            
        rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            Debug.Log("jump force applied. velocity" + rb.velocity);
         }
      
      }
   }
   public void OnMove(InputAction.CallbackContext context)
   {
      if(context.phase == InputActionPhase.Performed)
      {
         curMovementInput = context.ReadValue<Vector2>();
      }
      else if (context.phase == InputActionPhase.Canceled)
      {
         curMovementInput = Vector2.zero;
      }
      
   }

   private void Move()
   {
      Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
      dir *= moveSpeed;
      dir.y = rb.velocity.y;
      
      rb.velocity = dir;
   }

   void CameraLook()
   {
      camCurXRot +=mouseDelta.y * lookSensitivity;
      camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
      cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
      transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
   }


   bool IsGrounded()
   {
      if(transform == null) return false;
      Ray[] rays = new Ray[4]
      {
         new Ray(transform.position + (transform.forward * 0.3f) + (transform.up * 0.01f), Vector3.down),
         new Ray(transform.position + (-transform.forward * 0.3f) + (transform.up * 0.01f), Vector3.down),
         new Ray(transform.position + (transform.right * 0.3f) + (transform.up * 0.01f), Vector3.down),
         new Ray(transform.position + (-transform.right * 0.3f) + (transform.up * 0.01f), Vector3.down)
      };

      for (int i = 0; i < rays.Length; i++)
      {
         
         Debug.DrawRay(rays[i].origin, rays[i].direction * 0.5f, Color.red);
         if (Physics.Raycast(rays[i], 1.5f, groundLayerMask))
         {
            return true;  
         }
      }
      return false;
   }

   public void OnInventory(InputAction.CallbackContext context)
   {
      if (context.phase == InputActionPhase.Started)
      {
         inventory?.Invoke();
         ToggleCursor();
      }
   }
   public void ToggleCursor()
   {
      bool toggle = Cursor.lockState == CursorLockMode.Locked;
      Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
      canLook = !toggle;
   }
}
