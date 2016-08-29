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
        public int total_levels = 5;

        private float time_since_start = 0;
        private float time_to_spawn_next_boat = 0;
        private int boat_counter = 0;
        private bool all_boats_spawned=false;
        private bool game_over = false;

        // Use this for initialization
        private void Start()
        {
            //_spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            load_level();

            //StartCoroutine(StartLevel());
        }

        void load_level()
        {
            lista = null;
            boat_pos = null;

            switch (level)
            {
                case 1:
                    lista = new List<float>() { 1, 2, 3 };
                    boat_pos = new List<int>() { 1, 1, 2 }; break;
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

            time_to_spawn_next_boat = lista[0];

        }

        void FixedUpdate()
        {
            time_since_start += Time.fixedDeltaTime;

            //check if spawn boat
            if(time_since_start>= time_to_spawn_next_boat && !all_boats_spawned)
            {
                Vector2 spawn_pos = new Vector2(0, 0);
                switch (boat_pos[boat_counter])
                {
                    case 1: spawn_pos = m_spawn_pos1.transform.position; break;
                    case 2: spawn_pos = m_spawn_pos2.transform.position; break;
                    case 3: spawn_pos = m_spawn_pos3.transform.position; break;
                }

                Instantiate(Boat, spawn_pos, Boat.transform.rotation);

                boat_counter++;
                if (boat_counter >= lista.Count) all_boats_spawned = true;
                else time_to_spawn_next_boat += lista[boat_counter];
            }


            //check if level complete
            if(all_boats_spawned && !game_over)
            {
                GameObject[] boats;
                boats = GameObject.FindGameObjectsWithTag("Boat");

                int boat_counter = 0;
                foreach (GameObject boat in boats)
                {
                    boat_counter++;
                }

                if(boat_counter==0)
                {
                    //init next level
                    level++;
                    if(level>total_levels)
                    {
                        //game over
                        game_over = true;
                        Debug.Log("GameOver");
                    }
                    else
                    {
                        Debug.Log("Next Level");
                        all_boats_spawned = false;

                        //heal mirrors
                        GameObject[] mirrors;
                        mirrors = GameObject.FindGameObjectsWithTag("Mirror");

                        foreach (GameObject mirror in mirrors)
                        {
                            mirror.GetComponent<mirrorscript>().heal();
                            Debug.Log(mirror.GetComponent<mirrorscript>().Hp);
                        }

                        //new boats
                        time_since_start = 0;
                        load_level();
                    }

                    

                }
            }
        }


        /*IEnumerator StartLevel()
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

        }*/

    }
}
