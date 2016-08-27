using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {

        private GameObject[] _spawnPoints;
        private GameObject _sun;

        public GameObject Boat;

        public int SpawnBoats = 4;

        // Use this for initialization
        private void Start()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            StartCoroutine(StartLevel());

        }

        IEnumerator StartLevel()
        {

            for (int i = 0; i < SpawnBoats; i++)
            {
                var rand_sp = Random.Range(0, _spawnPoints.Length);
                Vector3 randYModifier = new Vector3(0, Random.Range(-10, 10), 0);
                Instantiate(Boat, _spawnPoints[rand_sp].transform.position + randYModifier, Boat.transform.rotation);
                
                yield return new WaitForSeconds(Random.Range(1, 2));

            }
            
        }
    }
}
