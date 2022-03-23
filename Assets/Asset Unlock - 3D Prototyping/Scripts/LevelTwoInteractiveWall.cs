using UnityEngine;


public class LevelTwoInteractiveWall : InteractiveWals
{

    protected override void MoveWall()
    {

        var mysticwallMovePositionLeft = new Vector3(4.5f, 3, 4);
        transform.position = Vector3.Lerp(transform.position, mysticwallMovePositionLeft, Time.deltaTime * .1f); // TODO Stop after finish 
        if (transform.position.y >= mysticwallMovePositionLeft.y - .1f)
            isAnswered = false;
        if (GameManager.isGameOver)
            transform.position = mysticwallMovePositionLeft;
    }
}
