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

    /// <summary>
    /// Checks if a float is in a range (inclusvie)
    /// </summary>
    /// <param name="checking">The float to be checked</param>
    /// <param name="low">The float the number must be greter than or equal to</param>
    /// <param name="high">The float the number must be less than or equal to</param>
    /// <returns></returns>
   public static bool IsInRange(float checking, float low, float high)
    {
        return checking >= low && checking <= high;
    }
}
