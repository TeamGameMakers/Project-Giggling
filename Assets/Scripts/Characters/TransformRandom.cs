using System;
using System.Collections.Generic;
using Base;
using Function;
using UnityEngine;

namespace Characters
{
    public class TransformRandom : SingletonMono<TransformRandom>
    {
        public List<Transform> spawnPoints = new List<Transform>();
        protected readonly List<Vector3> positions = new List<Vector3>();

        public string spawnSubObj = "spawnPoint";
        
        protected override void Start()
        {
            foreach (var p in spawnPoints)
            {
                if (string.IsNullOrEmpty(spawnSubObj))
                {
                    positions.Add(p.position);
                }
                else
                {
                    positions.Add(p.Find(spawnSubObj).position);
                }
            }
        }

        public Vector3 GetRandomPosition()
        {
            return RandomSelector.RandomData<Vector3>(positions);
        }
    }
}