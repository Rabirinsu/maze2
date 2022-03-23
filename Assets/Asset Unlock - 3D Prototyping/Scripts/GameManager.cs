using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private ParticleSystem confetiOne;
    [SerializeField] private ParticleSystem confetiTwo;
    [SerializeField] private ParticleSystem explodeChar;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform targetLocation;
    [SerializeField] private GameObject mainChar;
    [SerializeField] private AudioSource cheersSound;
    [SerializeField] private AudioClip cheersClip;
    [SerializeField] private AudioSource gameover_SFX;
    [SerializeField] private AudioClip gameover_Clip;
    [SerializeField] private Canvas questionCanvas;
    [SerializeField] private Canvas questionCanvasLeft;
    [SerializeField] private Canvas questionCanvasLevelTwo;
    [SerializeField] private GameObject Lights;
    [SerializeField] private GameObject mazeGuardian;
    [SerializeField] private GameObject FallTrigger;
    [SerializeField] private GameObject finishGateTrigger;
    [SerializeField] private GameObject controllerCanvas;
    [SerializeField] private GameObject levelEnd_UI;
    [SerializeField] private GameObject ThreeLights;
    [SerializeField] private Rigidbody FallingStone;
    private float UIAppearTime;
    public static bool isGameOver;
    public static bool isGameStart;
    private float speed = 1.5f;
    public static bool islevelEnd;
    private static readonly int Jump = Animator.StringToHash("Jump");

    private void Awake()
    {
        Application.targetFrameRate = 30;
        ResetGameValues();
    }
    private void Start()
    {
        UIAppearTime = 4;
    }
    private void ResetGameValues()
    {
        isGameOver = false;
        isGameStart = false;
        islevelEnd = false;
        questionCanvas.enabled = false;
        questionCanvasLeft.enabled = false;
        questionCanvasLevelTwo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (islevelEnd)
        {
            StartCoroutine(nameof(LevelEnding));
        }
       if (isGameStart)
        {
            Lights.SetActive(true);
            mazeGuardian.SetActive(true);
        }
     if (isGameOver)
        {
            StartCoroutine(nameof(GameOver));
        }
        if (LeftInteractiveWall.isAnswered)
            finishGateTrigger.SetActive(true);
    }
    private IEnumerator GameOver()
    {
        Lights.SetActive(false);
        mazeGuardian.SetActive(false);
        controllerCanvas.SetActive(false);
        playerAnimator.SetTrigger("End");
        questionCanvas.enabled = false;
        questionCanvasLeft.enabled = false;
        questionCanvasLevelTwo.enabled = false;
        explodeChar.Play();
        gameover_SFX.PlayOneShot(gameover_Clip, .1f);
        yield return new WaitForSeconds(UIAppearTime);
        gameOverUI.SetActive(true);
        gameObject.SetActive(false);


    }   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("finishGate"))
        {
            islevelEnd = true;
            confetiOne.Play();
            confetiTwo.Play();
            cheersSound.PlayOneShot(cheersClip, 3);
            playerAnimator.SetTrigger(Jump);
        }

        if (other.gameObject.CompareTag("wallQuestionTrigger"))
        {
            
            questionCanvas.enabled = true;
            questionCanvasLevelTwo.enabled = true;
        }
        if (other.gameObject.CompareTag("wallQuestionTriggerLeft"))
        {
            
            questionCanvasLeft.enabled = true;
        }
        if (other.gameObject.CompareTag("FallTrigger"))
        {
            isGameOver = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("wallQuestionTrigger") || other.gameObject.CompareTag("wallQuestionTriggerLeft"))
        {
          
            questionCanvas.enabled = false;
            questionCanvasLeft.enabled = false;
            questionCanvasLevelTwo.enabled = false;
        }
    }

     private IEnumerator LevelEnding()
    {
       
        var step = speed * Time.deltaTime;
        mainChar.transform.position = Vector3.MoveTowards(  mainChar.transform.position, targetLocation.position, step);
        playerAnimator.SetTrigger(Jump);
        Lights.SetActive(false);
        mazeGuardian.SetActive(false);
        ThreeLights.SetActive(true);
        yield return new WaitForSeconds(UIAppearTime);
        levelEnd_UI.SetActive(true);
    }
  
}
