using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SUtilities
{
    /// <summary>
    /// Checks if a position is in a rectangle drawn from a bottom left and top right corner (inclusive)
    /// </summary>
    /// <param name="position">The point of the object</param>
    /// <param name="bottomLeft">Bottom left of the rectangle</param>
    /// <param name="topRight">Top right of the rectangle</param>
    /// <returns>Whether or not the point is in the rectangle</returns>
    public static bool IsInRange(Vector3 position, Vector3 bottomLeft, Vector3 topRight)
    {
        return (position.x >= bottomLeft.x && position.x <= topRight.x && position.y >= bottomLeft.y && position.y <= topRight.y);
    }
}
