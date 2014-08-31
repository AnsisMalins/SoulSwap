using System.Collections;
using UnityEngine;

// The base class for AI that contains all the convenience stuff that would have to otherwise be
// replicated in every AI class. Translates simple commands into control input. Notice that to
// support the soul swapping mechanic, I can't cache the Creature pointer. Most importatly, I
// must refresh it after every yield because it may have changed in the meantime.

public class RobotSoul : Soul
{
    protected bool isTouchingGround
    {
        get
        {
            var creature = gameObject.Parent<Creature>();
            return creature != null ? creature.isTouchingGround : false;
        }
    }

    protected IEnumerator Jump()
    {
        var creature = gameObject.Parent<Creature>();
        if (creature != null) creature.Jump();
        return Util.emptyCoroutine;
    }

    protected void Move(Vector2 movement)
    {
        var creature = gameObject.Parent<Creature>();
        if (creature != null) creature.movement = movement;
    }

    protected IEnumerator Move(Vector2 movement, float seconds)
    {
        if (seconds <= 0) yield break;
        Move(movement);
        yield return new WaitForSeconds(seconds);
        Move(Vector2.zero);
    }

    // This method is not completely accurate, but after three weekends I should focus on finishing
    // instead.
    protected IEnumerator Rotate(float rotation, float seconds)
    {
        if (rotation == 0) yield break;
        if (seconds > 0)
        {
            float sign = Mathf.Sign(rotation);
            rotation = Mathf.Abs(rotation);
            float delta = rotation / seconds;
            while (rotation > 0)
            {
                float delta2 = delta * Time.deltaTime;
                var creature = gameObject.Parent<Creature>();
                if (creature != null) creature.rotation += sign * delta2;
                rotation -= delta2;
                yield return null;
            }
            if (rotation < 0)
                Debug.LogWarning("Rotate overshot by " + -rotation + " degrees.", this);
        }
        else
        {
            var creature = gameObject.Parent<Creature>();
            if (creature != null) creature.rotation += rotation;
        }
    }

    protected IEnumerator Think(string thought)
    {
        var creature = gameObject.Parent<Creature>();
        if (creature == null) return Util.emptyCoroutine;
        return creature.Think(thought);
    }

    // These two don't work like I wanted them to. :(
    protected IEnumerator WaitGroundEnter()
    {
        while (isTouchingGround) yield return null;
        while (!isTouchingGround) yield return null;
    }

    protected IEnumerator WaitGroundExit()
    {
        while (!isTouchingGround) yield return null;
        while (isTouchingGround) yield return null;
    }
}