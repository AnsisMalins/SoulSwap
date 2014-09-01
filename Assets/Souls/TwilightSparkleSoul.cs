using System.Collections;
using UnityEngine;

// Twilight Sparkle jumps up in the air and teleports in a random direction.

public class TwilightSparkleSoul : RobotSoul
{
    public Vector2 teleportDistance;
    public string[] thoughts;
    public float timeToTeleport;
    public Vector2 turnRange;
    public Vector2 turnTime;
    public Vector2 waitInterval;

    private void Start()
    {
        StartCoroutine(StudyFriendship());
    }

    private IEnumerator StudyFriendship()
    {
        while (true)
        {
            yield return new WaitForSeconds(Util.Random(waitInterval));
            yield return StartCoroutine(Think(thoughts.Random()));

            yield return new WaitForSeconds(Util.Random(waitInterval));
            yield return StartCoroutine(Jump());
            yield return new WaitForSeconds(timeToTeleport);
            var dest = Random.onUnitSphere * Util.Random(teleportDistance);
            dest.y = 0;
            yield return StartCoroutine(Teleport(dest));
            yield return new WaitForSeconds(timeToTeleport);
            yield return StartCoroutine(Rotate(Util.Random(turnRange), Util.Random(turnTime)));
        }
    }
}