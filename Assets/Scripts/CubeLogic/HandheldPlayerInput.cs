using UnityEngine;

public class HandheldPlayerInput : IPlayerInput
{
    private int _pixelsToDetectSwipe;

    private Vector2 _touchStart;
    private Vector2 _touchEnd;

    public HandheldPlayerInput()
    {
        _pixelsToDetectSwipe = 20;
    }

    public HandheldPlayerInput(int pixelsToDetectSwipe)
    {
        _pixelsToDetectSwipe = pixelsToDetectSwipe;
    }

    public Vector2 GetInput()
    {
        var go = new Vector2();

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _touchEnd = touch.position;
            }

            float traveledDistancePixelsX = _touchEnd.x - _touchStart.x;

            if (Mathf.Abs(traveledDistancePixelsX) > _pixelsToDetectSwipe)
            {
                go.x = 1f * Mathf.Abs(traveledDistancePixelsX) / traveledDistancePixelsX;
            }
        }

        return go;
    }
}
