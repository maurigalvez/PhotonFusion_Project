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
    }
}
