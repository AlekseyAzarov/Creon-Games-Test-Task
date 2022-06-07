using UnityEngine;

public class PcPlayerInput : IPlayerInput
{
    public Vector2 GetInput()
    {
        var go = new Vector2();

        if (Input.GetKey(KeyCode.A)) go.x = -1f;
        else if (Input.GetKey(KeyCode.D)) go.x = 1f;

        return go;
    }
}
