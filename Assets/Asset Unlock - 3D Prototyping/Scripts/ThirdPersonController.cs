using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
     public Camera gameCamera;
    [SerializeField]private float playerSpeed;
    private float JumpForce = 1.0f;
    [SerializeField] private Joystick joystick;
    private CharacterController _mController;
    private Animator _animator;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float gravityValue = -9.81f;
    [SerializeField] private Transform followTransform;
    [SerializeField] private Canvas controllerCanvas;
    private Vector3 _angles;
    private Touch _touch;
    private Quaternion _rotationY;
    [SerializeField] private float rotateValue;
    private static readonly int Jump1 = Animator.StringToHash("Jump");
    private static readonly int MovementZ = Animator.StringToHash("MovementZ");
    private static readonly int MovementX = Animator.StringToHash("MovementX");

    private void Start()
    {
        _mController = gameObject.GetComponent<CharacterController>();
        _animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //  if (Input.touchCount > 0) // IF touched screen on phone
        //{
        // ReSharper disable once PossibleLossOfFraction
        if (GameManager.isGameStart && !GameManager.islevelEnd && !GameManager.isGameOver)
        {
            controllerCanvas.enabled = true;
            if (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2) // Rotate with using right screen on phone
                Rotate();  // TODO Need more flexibility
            else PlayerMovement();
        }
        else controllerCanvas.enabled = false;

        //}
    }
    public void manageGameStart()
    {
        GameManager.isGameStart = true;
    }

    private void PlayerMovement()
    {
        transform.rotation = Quaternion.Euler(0, followTransform.rotation.eulerAngles.y, 0);
        followTransform.transform.localEulerAngles = new Vector3(_angles.x, 0, 0);

        _groundedPlayer = _mController.isGrounded; 

        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -0.5f;
        }
      

        var input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        //trasnform input into camera space
        var forward = gameCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();
        var right = Vector3.Cross(Vector3.up, forward);

        Vector3 move = forward * input.z + right * input.x;
        move.y = 0;

        _mController.Move(move * (Time.deltaTime * playerSpeed));


        _animator.SetFloat(MovementX, input.x);
        _animator.SetFloat(MovementZ, input.z);

        if (input != Vector3.zero)
        {
            gameObject.transform.forward = forward;
        }

        // Changes the height position of the player..


        _playerVelocity.y += gravityValue * Time.deltaTime;

        _mController.Move(_playerVelocity * Time.deltaTime);
    }

    private void Rotate() // TODO
    {
        _touch = Input.GetTouch(0);
        if (Input.touchCount > 0)
            if (_touch.phase == TouchPhase.Moved)
            {
               
           var _rotationY = Quaternion.Euler(0, _touch.deltaPosition.x * rotateValue, 0) ;
           followTransform.rotation  = _rotationY * followTransform.rotation;
          
            }
    }

    public void Jump()
    {
        if (_groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(JumpForce * -3.0f * gravityValue);
            _animator.SetTrigger(Jump1);
        }
    }
}