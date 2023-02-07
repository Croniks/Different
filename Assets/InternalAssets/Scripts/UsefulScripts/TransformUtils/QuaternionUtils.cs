using UnityEngine;


namespace UsefulScripts.QuaternionUtils
{
    public static class QuaternionUtils 
    {
        // Such as Quaternion.AngleAxis(float angle, Vector3 axis)
        public static Quaternion GetRotationQuaternion(float angleInDegrees, Vector3 rotationAxis)
        {
            float angleInRadians = angleInDegrees * ((2 * Mathf.PI) / 360);
            
            if (rotationAxis.magnitude != 1) { rotationAxis = rotationAxis.normalized; }

            return new Quaternion(
                rotationAxis.x * Mathf.Sin(angleInRadians / 2),
                rotationAxis.y * Mathf.Sin(angleInRadians / 2),
                rotationAxis.z * Mathf.Sin(angleInRadians / 2),
                Mathf.Cos(angleInRadians / 2)
            );
        }
    }
}