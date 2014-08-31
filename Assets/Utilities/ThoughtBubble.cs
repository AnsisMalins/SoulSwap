using System.Collections;
using UnityEngine;

// The 3D equivalent of the good old message box without buttons but with a cypewriter effect. I
// used it to differentiate the various AIs.

public class ThoughtBubble : MonoBehaviour
{
    public float showTime = 2;
    public float typingInterval = 0.2f;

    public static IEnumerator Show(GameObject parent, Vector3 localPosition, string text)
    {
        var gameObject = Scene.current.thoughtBubble.Clone();
        var textMesh = gameObject.Require<TextMesh>();
        var thoughtBubble = gameObject.Require<ThoughtBubble>();

        gameObject.transform.parent = parent.transform;
        gameObject.transform.localPosition = localPosition;

        if (thoughtBubble.typingInterval > 0)
        {
            foreach (var c in text)
            {
                yield return new WaitForSeconds(thoughtBubble.typingInterval);
                textMesh.text += c;
            }
        }
        else
        {
            textMesh.text = text;
        }
        yield return new WaitForSeconds(thoughtBubble.showTime);

        Destroy(gameObject);
    }
}