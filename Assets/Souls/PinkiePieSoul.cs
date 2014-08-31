using System.Collections;
using UnityEngine;

// Pinkie Pie bounces around.

public class PinkiePieSoul : RobotSoul
{
    public string[] thoughts;
    public Vector2 turnRange;
    public Vector2 turnTime;
    public Vector2 waitInterval;
    public int jumpCountMin;
    public int jumpCountMax;
    public float intervalBetweenJumps;

    private void Start()
    {
        StartCoroutine(PartyInternally());
    }

    private IEnumerator PartyInternally()
    {
        while (true)
        {
            yield return new WaitForSeconds(Util.Random(waitInterval));
            yield return StartCoroutine(Think(thoughts.Random()));

            yield return new WaitForSeconds(Util.Random(waitInterval));
            yield return StartCoroutine(Rotate(Util.Random(turnRange), Util.Random(turnTime)));
            var jumpCount = Random.Range(jumpCountMin, jumpCountMax);
            Move(Vector2.up);
            for (var i = 0; i < jumpCount; i++)
            {
                //yield return StartCoroutine(WaitGroundEnter());
                yield return new WaitForSeconds(intervalBetweenJumps);
                yield return StartCoroutine(Jump());
            }
            Move(Vector2.zero);
        }
    }
}