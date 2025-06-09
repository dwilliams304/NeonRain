using UnityEngine;

public enum Axis { X, Y, Z }

public static class VectorExtensions
{
    
    /// <summary>
    /// Zero out a certain axis on a given Vector3
    /// </summary>
    /// <param name="vector">Vector that we want to zero an axis of</param>
    /// <param name="axis">Which axis we want to be zeroed out</param>
    /// <returns></returns>
    public static Vector3 ZeroAxis(this Vector3 vector, Axis axis){
        switch(axis){
            case Axis.X:
                return new Vector3(0f, vector.y, vector.z);
            case Axis.Y:
                return new Vector3(vector.x, 0f, vector.z);
            case Axis.Z:
                return new Vector3(vector.x, vector.y, 0f);
            default:
                return new Vector3(vector.x, vector.y, vector.z);
        }
    }

    /// <summary>
    /// Randomize a Vector3's X, Y, Z values
    /// </summary>
    /// <param name="vector3">The original Vector3</param>
    /// <param name="randomVector">The random values we'd like to use to randomize</param>
    /// <returns></returns>
    public static Vector3 RandomizeVector(this Vector3 vector3, Vector3 randomVector){
        return vector3 + new Vector3(
            Random.Range(-randomVector.x, randomVector.x),
            Random.Range(-randomVector.y, randomVector.y),
            Random.Range(-randomVector.z, randomVector.z)
        );
    }
}