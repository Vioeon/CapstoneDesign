using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    Vector3 input;
    private PlayerCharacter controls; //control

    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private CharacterController controller = null;

    public PlayerInput _playerInput => playerInput;

    // Start is called before the first frame update
    void Awake() {
        controls = new PlayerCharacter();
    }

    void OnEnable() {
        controls.Enable();
    }

    void OnDisable() {
        controls.Disable();
    }

    void Start()
    {
        //body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // h = Input.GetAxisRaw("Horizontal");
        // v = Input.GetAxisRaw("Vertical");
        Vector2 inputx = controls.Player.Move.ReadValue<Vector2>();
        Vector3 input = new Vector3(inputx.x, 0, inputx.y).normalized;
        //Debug.Log(input);

        var movement = new InputAction();

        //input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));  //keybinding later...
        transform.LookAt(transform.position + input);
        transform.position = transform.position + (input * Time.deltaTime * speed);

        if (controls.Player.Fire.IsPressed())
        {
            Debug.Log("Fire");
        }
    }
}
