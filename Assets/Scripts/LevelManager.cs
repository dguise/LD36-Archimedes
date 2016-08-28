using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {

        public GameObject m_spawn_pos1;
        public GameObject m_spawn_pos2;
        public GameObject m_spawn_pos3;
        private GameObject _sun;

        public GameObject Boat;

        //public int SpawnBoats = 4;
        public List<float> lista;
        public List<int> boat_pos;
        public int level = 1;

        // Use this for initialization
        private void Start()
        {
            //_spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            switch (level)
            {
                case 1:
                    lista = new List<float>() { 10, 2, 2, 3, 1, 1, 1, 1, 1, 1, 1 };
                    boat_pos = new List<int>() { 1, 1, 2, 2, 3, 3, 1, 2, 3, 3, 2 }; break;
                case 2:
                    lista = new List<float>() { 10, 2, 2, 3, 1, 1, 1, 1, 1, 1, 1 };
                    boat_pos = new List<int>() { 1, 1, 2, 2, 3, 3, 1, 2, 3, 3, 2 }; break;
                case 3:
                    lista = new List<float>() { 10, 2, 2, 3, 1, 1, 1, 1, 1, 1, 1 };
                    boat_pos = new List<int>() { 1, 1, 2, 2, 3, 3, 1, 2, 3, 3, 2 }; break;
                case 4:
                    lista = new List<float>() { 10, 2, 2, 3, 1, 1, 1, 1, 1, 1, 1 };
                    boat_pos = new List<int>() { 1, 1, 2, 2, 3, 3, 1, 2, 3, 3, 2 }; break;
                case 5:
                    lista = new List<float>() { 10, 2, 2, 3, 1, 1, 1, 1, 1, 1, 1 };
                    boat_pos = new List<int>() { 1, 1, 2, 2, 3, 3, 1, 2, 3, 3, 2 }; break;
            }

            StartCoroutine(StartLevel());
        }


        IEnumerator StartLevel()
        {

            for (int i = 0; i < lista.Count; i++)
            {
                float time_sum = 0;

                Vector2 spawn_pos = new Vector2(0, 0);
                switch (boat_pos[i])
                {
                    case 1: spawn_pos = m_spawn_pos1.transform.position; break;
                    case 2: spawn_pos = m_spawn_pos2.transform.position; break;
                    case 3: spawn_pos = m_spawn_pos3.transform.position; break;
                }


                //var rand_sp = Random.Range(0, _spawnPoints.Length);
                //Vector3 randYModifier = new Vector3(0, Random.Range(-5, 5), 0);
                //Instantiate(Boat, _spawnPoints[rand_sp].transform.position + randYModifier, Boat.transform.rotation);

                Instantiate(Boat, spawn_pos, Boat.transform.rotation);

                yield return new WaitForSeconds(lista[i] + time_sum);

                time_sum += lista[i];

            }

        }

    }
}
