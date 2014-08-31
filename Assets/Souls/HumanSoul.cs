using UnityEngine;

public class HumanSoul : Soul
{
    private void Update()
    {
        var creature = gameObject.Parent<Creature>();
        if (creature == null) return;
        creature.movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        creature.rotation += Input.GetAxis("Mouse X");
        if (Input.GetButtonDown("Jump")) creature.Jump();
        if (Input.GetButtonDown("Use")) creature.SwapSouls(gameObject);
    }
}