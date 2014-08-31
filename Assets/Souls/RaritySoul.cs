using System.Collections;
using UnityEngine;

// Rarity just sits in place and turns a bit.

public class RaritySoul : RobotSoul
{
    public Vector2 waitInterval;
    public Vector2 turnRange;
    public Vector2 turnTime;
    public string[] thoughts;

    private void Start()
    {
        StartCoroutine(Rarara());
    }

    private IEnumerator Rarara()
    {
        while (true)
        {
            yield return new WaitForSeconds(Util.Random(waitInterval));
            yield return StartCoroutine(Think(thoughts.Random()));

            yield return new WaitForSeconds(Util.Random(waitInterval));
            yield return StartCoroutine(Rotate(Util.Random(turnRange), Util.Random(turnTime)));
        }
    }
}