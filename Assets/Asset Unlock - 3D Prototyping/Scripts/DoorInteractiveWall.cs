using UnityEngine;


public class DoorInteractiveWall : InteractiveWals
{
  
    protected override void MoveWall()
    {
      
        var mysticwallMovePositionLeft = new Vector3(4.5f, 6, 19);
        transform.position = Vector3.Lerp(transform.position, mysticwallMovePositionLeft, Time.deltaTime * .118f); // TODO Stop after finish 
        if (transform.position.y >= mysticwallMovePositionLeft.y)
            isAnswered = false;
    }
}