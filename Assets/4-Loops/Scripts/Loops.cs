using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoopArrays
{
    public class Loops : MonoBehaviour
    {
        public GameObject[] spawnPrefabs;
        public float frequency = 3;
        public float amplitude = 6;
        public int spawnAmount = 10;
        public float spawnRadius = 5f;
        public string message = "Print This";
        public float printTime = 2f;


        // private float timer = 0;

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        // Use this for initialization
        void Start()
        {
            SpawnObjectsWithSine();
        }

        // Update is called once per frame
        void Update()
        {

            //// Loop through until timer gets to printTime
            //while (timer <= printTime) // STICK AROUND
            //{
            //    // Count up timer in seconds
            //    timer += Time.deltaTime;
            //    print("GET TO THA CHOPPA!");
            //}
        }

        void SpawnObjectsWithSine()
        {
            /* 
            for (initialisation; condition; iteration)
            {
                // Statement(s)
            }
            */
            for (int i = 0; i < spawnAmount; i++)
            {
                // Spawned new GameObject
                int randomIndex = Random.Range(0, spawnPrefabs.Length);
                // Store randomly selected prefab
                GameObject randomPrefab = spawnPrefabs[randomIndex];
                GameObject clone = Instantiate(spawnPrefabs[randomIndex]);
                // Grab the MeshRenderer
                MeshRenderer rend = clone.GetComponent<MeshRenderer>();
                // Change the colour
                float r = Random.Range(0, 2);
                float g = Random.Range(0, 2);
                float b = Random.Range(0, 2);
                float alpha = 1;
                rend.material.color = new Color(r, g, b, alpha);
                // Generated random position within circle (sphere actually)
                float x = Mathf.Sin(i * frequency) * amplitude;
                float y = i;
                float z = Mathf.Cos(i * frequency) * amplitude;
                Vector3 randomPos = transform.position + new Vector3(x, y, z);
                // Set spawned object's position
                clone.transform.position = randomPos;
            }
        }

        void SpawnObjects()
        {
            /* 
            for (initialisation; condition; iteration)
            {
                // Statement(s)
            }
            */
            for (int i = 0; i < 10; i++)
            {
                GameObject clone = Instantiate(spawnPrefabs[0]);
                Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
                // Cancel out the z
                randomPos.z = 0;
                // Set spawned object's position
                clone.transform.position = randomPos;
            }
        }
    }
}
