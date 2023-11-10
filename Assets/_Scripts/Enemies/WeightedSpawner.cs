using System.Collections.Generic;
using UnityEngine;

public class WeightedSpawner : MonoBehaviour
{

    public float spawnerDelay;

    public List<Spawnable> spawnables = new List<Spawnable>();

    [System.Serializable]
    public class Spawnable{
        public GameObject toSpawn;
        public float weight = 0;
    }
}
