using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Player : MonoBehaviour
{
    Vector2 JoystickInput;
    Vector3 MoveStep;
    Vector3 LookDelta;
    NavMeshAgent agent;
    RaycastHit hit;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask AcceptableCollisionLayer;
    PlayerInput playerInput;
    InputAction Move;
    InputAction Fire;
    InputAction Look;
    InputAction Ultimate;
    Health health;
    Energy energy;
    //InputAction LookField;
    //bool LookFieldPressed;
    int TouchId;
    [Range(0, 2)]
    public float CameraXSensetivity = 0.6f;
    [Range(0, 2)]
    public float CameraYSensetivity = 0.2f;
    [SerializeField] float EnergyToUlt = 100;
    [SerializeField] float TeleportationCooldown = 1f; //seconds
    public GameObject Bullet;
    float CameraXAngle;
    float LastTeleportTime;
    const float CameraMinXAngle = 45;
    const float CameraMaxXAngle = 315;
    const float CameraRotationSpeed = 360; //degrees per second
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        energy = GetComponent<Energy>();
        playerInput = GetComponent<PlayerInput>();
        Move = playerInput.actions["Move"];
        Fire = playerInput.actions["Fire"];
        Look = playerInput.actions["Look"];
        Ultimate = playerInput.actions["Ultimate"];
        LastTeleportTime = -TeleportationCooldown;
        //LookField = playerInput.actions["LookField"];
    }
    void OnEnable(){
        // EnhancedTouchSupport.Enable();
        Move.canceled += StopPlayer;
        // LookField.performed += FieldPressed;
        // LookField.canceled += FieldReleased;
        Fire.performed += Shoot;
        Ultimate.performed += DoCoolStuff;
        health.Die += Die;
    }
    void OnDisable(){
        // EnhancedTouchSupport.Disable();
        Move.canceled -= StopPlayer;
        // LookField.performed -= FieldPressed;
        // LookField.canceled -= FieldReleased;
        Fire.performed -= Shoot;
        Ultimate.performed += DoCoolStuff;
        health.Die -= Die;
    }
    void Update(){
        JoystickInput = Move.ReadValue<Vector2>();
        //Move Player
        if(JoystickInput.magnitude > 0){
            MoveStep = cam.transform.right * JoystickInput.normalized.x + cam.transform.forward * JoystickInput.normalized.y;
            agent.SetDestination(transform.position + MoveStep);
        }
    }
    void LateUpdate()
    {
        cam.transform.position = transform.position;

        //Rotate camera  
        LookDelta = new Vector3(-Look.ReadValue<Vector2>().y * Time.deltaTime  * CameraYSensetivity, Look.ReadValue<Vector2>().x * Time.deltaTime  * CameraXSensetivity, 0);
        cam.transform.Rotate(LookDelta * CameraRotationSpeed);
        //Limit Camera vertical rotation
        CameraXAngle = cam.transform.localEulerAngles.x;
        if(CameraXAngle > CameraMinXAngle && CameraXAngle < 180) CameraXAngle = CameraMinXAngle;
        else if(CameraXAngle < CameraMaxXAngle && CameraXAngle > 180) CameraXAngle = CameraMaxXAngle; 
        //Prevent rolling
        cam.transform.rotation = Quaternion.Euler(CameraXAngle,cam.transform.localEulerAngles.y, 0);

        /*//Look around without second joystick
        var activeTouches = Touch.activeTouches;
        //Rotate Camera
        if(LookFieldPressed){
            LookDelta = new Vector3(-activeTouches[TouchId].delta.y * CameraYSensetivity, activeTouches[TouchId].delta.x * CameraXSensetivity, 0);
            cam.transform.Rotate(LookDelta);
            //Limit Camera vertical rotation
            CameraXAngle = cam.transform.localEulerAngles.x;
            if(CameraXAngle > CameraMinXAngle && CameraXAngle < 180) CameraXAngle = CameraMinXAngle;
            else if(CameraXAngle < CameraMaxXAngle && CameraXAngle > 180) CameraXAngle = CameraMaxXAngle; 
            //Prevent rolling
            cam.transform.rotation = Quaternion.Euler(CameraXAngle,cam.transform.localEulerAngles.y, 0);
        }*/
    }
    void StopPlayer(InputAction.CallbackContext context){
        agent.SetDestination(transform.position);
    }
    /*void FieldPressed(InputAction.CallbackContext context){
        LookFieldPressed = true;
        TouchId = Touch.activeTouches.Count-1;
    }
    void FieldReleased(InputAction.CallbackContext context){
        LookFieldPressed = false;
    }*/
    void Shoot(InputAction.CallbackContext context){
        var b = Instantiate(Bullet, cam.transform.position + (cam.transform.forward * 0.1f), cam.transform.rotation, BulletContainer.bulletContainer);
        b.GetComponent<Rigidbody>().AddForce(b.transform.forward * 10, ForceMode.VelocityChange);
        b.GetComponent<Bullet>().OwnerHelthPercentage = health.health/health.MaxHealth;
        Destroy(b,3);
    }
    void DoCoolStuff(InputAction.CallbackContext context){
        if(energy.energy >= EnergyToUlt){
            energy.RemoveEnergy(EnergyToUlt);
            EnemiesManager.Ultimate();
        }
    }
    void Die(int modifier){
        GameManager.OnGameOver?.Invoke();
        Debug.Log("Player Dead");
    }
    void OnTriggerEnter(Collider other){
        //Teleport is rech arena edge
        if(AcceptableCollisionLayer == (AcceptableCollisionLayer | (1 << other.gameObject.layer))){
            if(Time.time - LastTeleportTime > TeleportationCooldown){
                transform.position += (other.transform.position - transform.position) * 2;
                cam.transform.Rotate(new Vector3(0, 180, 0), Space.World);
                LastTeleportTime = Time.time;
            }
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + MoveStep, 0.1f);
    }
}
