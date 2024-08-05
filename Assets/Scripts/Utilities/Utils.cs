using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities
{
    public static class Utils
    { 
        public static int WorldArea = 20;
        public static int WorldHeight = 4;

        public static Vector3 GetRandomSpawnPoint()
        {
            return new Vector3(Random.Range(-WorldArea, WorldArea), WorldHeight, Random.Range(-WorldArea, WorldArea));
        }

        public static void SetRenderLayerInChildren(Transform parent, int layerNumber)
        {
            foreach(var childTransform in parent.GetComponentsInChildren<Transform>(true))
                childTransform.gameObject.layer = layerNumber;
        }
    }
}
