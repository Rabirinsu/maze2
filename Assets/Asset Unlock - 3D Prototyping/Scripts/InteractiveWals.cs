using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveWals : MonoBehaviour
{
    [SerializeField] private Button trueAnswerRightWallButton;
    [SerializeField] private Canvas questionCanvas;
    public static bool isAnswered;
    [SerializeField] private float wallScale;
    [SerializeField] private AudioSource wallMovingSound;
    [SerializeField] private AudioClip wallMovingClip;
     protected static float wallSmoothTime;
    [SerializeField] private TextMeshProUGUI questionRightText;
    [SerializeField] private TextMeshProUGUI questionLeftText;
    private Vector3 mysticwallMovePosition;
    [SerializeField] private GameObject wallTriggers;
   
    private void Start()
    {
  
        //    questionCanvas.enabled = false;
        wallSmoothTime = .3f;
        trueAnswerRightWallButton.onClick.AddListener(TaskOnClick); // Answer Buttons
        mysticwallMovePosition = new Vector3(10.8f, 3, 15.34f); // Wall Target Position
    }

    private void TaskOnClick()
    {
      
        ChangeWallText();
        wallMovingSound.PlayOneShot(wallMovingClip, wallScale); // play sound if answer corrected
        isAnswered = true;
    }
    
    private void Update()
    {
        if (isAnswered)
        {
            MoveWall();
            Destroy(wallTriggers);
        }
    }

    private void ChangeWallText()
    {
      questionCanvas.enabled = false; // Answer Canvas False
      questionRightText.text = " ";
      questionLeftText.text = " ";
   //   questionRightText.color = Color.green; //"You are enlightened! You were allowed to pass through the pyramids by Anubis.";  // Questin Answered Correctly
    }

    protected virtual void MoveWall()
    {
        transform.position = Vector3.Lerp(transform.position, mysticwallMovePosition, Time.deltaTime * wallSmoothTime); // TODO Stop after finish 
        if (transform.position.y >= mysticwallMovePosition.y - .4f)
        {
            isAnswered = false;
           
        }
           
        
        
    }
}