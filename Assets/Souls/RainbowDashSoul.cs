using System.Collections;
using UnityEngine;

public class RainbowDashSoul : RobotSoul
{
    public Vector2 moveTime;
    public string[] thoughts;
    public Vector2 turnRange;
    public Vector2 turnTime;
    public Vector2 waitInterval;


    private void Start()
    {
        StartCoroutine(BeAwesome());
    }

    private IEnumerator BeAwesome()
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(Util.Random(waitInterval));
            yield return StartCoroutine(Think(thoughts.Random()));

            yield return new WaitForSeconds(Util.Random(waitInterval));
            var rotation = Util.Random(turnTime);
            rotation = Mathf.Sign(rotation) * (180 - Mathf.Abs(rotation));
            yield return StartCoroutine(Rotate(rotation, Util.Random(turnTime)));
            yield return StartCoroutine(Move(Vector2.up, Util.Random(moveTime)));
        }
    }
}