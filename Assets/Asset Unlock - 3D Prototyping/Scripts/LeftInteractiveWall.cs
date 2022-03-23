using UnityEngine;


public class LeftInteractiveWall : InteractiveWals
{
  
    protected override void MoveWall()
    {
      
        var mysticwallMovePositionLeft = new Vector3(-1.8f, 3, 15.48f);
        transform.position = Vector3.Lerp(transform.position, mysticwallMovePositionLeft, Time.deltaTime * wallSmoothTime); // TODO Stop after finish 
        if (transform.position.y >= mysticwallMovePositionLeft.y)
            isAnswered = false;
    }
}