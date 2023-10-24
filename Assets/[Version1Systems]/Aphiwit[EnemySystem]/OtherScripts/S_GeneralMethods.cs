using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GeneralMethods 
{
    public static Vector3 GeneratePointOnNavMesh(Transform transform, float pointRange, Vector3 pointVector3, LayerMask walkableLayerMask, bool pointSet)
    {
        // Generate a random position to walk or spawn to
        float randomZ = UnityEngine.Random.Range(-pointRange, pointRange);
        float randomX = UnityEngine.Random.Range(-pointRange, pointRange);

        pointVector3 = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if point generated exists
        if (Physics.Raycast(pointVector3, -transform.up, 2f, walkableLayerMask))
        {
            pointSet = true;
        }

        Debug.Log("GMP is running!");
        return pointVector3;
    }
}
