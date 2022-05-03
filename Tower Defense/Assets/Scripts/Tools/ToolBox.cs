using UnityEngine;

public static class ToolBox 
{
    public static Quaternion GetDesireRotation(Vector3 target, Vector3 objectLook)
    {
        Vector3 direction = target - objectLook;
        Quaternion angle = Quaternion.LookRotation(direction, Vector3.up);
        return angle;
    }
}
