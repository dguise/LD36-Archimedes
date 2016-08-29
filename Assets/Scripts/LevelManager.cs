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
        public GameObject m_level_text;
        public GameObject m_screen;
        public Sprite m_text_wave1;
        public Sprite m_text_wave2;
        public Sprite m_text_wave3;
        public Sprite m_text_wave4;
        public Sprite m_text_wave5;
        public Sprite m_screen_main;
        public Sprite m_screen_gameover;
        public Sprite m_screen_win;
        public float m_show_level_sign_time = 3;
        private float m_show_level_sign_timer = 3;
        public int game_state = 0;//0 main, 1 run, 2 win, 3 gameover

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
            m_screen.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);

            //_spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

            //load_level();

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

        void Update()
        {
            switch(game_state)
            {
                case 0://main
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Application.Quit();
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //start level
                        game_state = 1;
                        load_level();

                        m_screen.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    }
                }
                break;

                case 1://run
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                            Application.Quit();
                    }

                    //text fade
                    float fade_level = 0;
                    if (m_show_level_sign_timer > 2)
                    {
                        fade_level = -(m_show_level_sign_timer - 3);
                    }
                    else if (m_show_level_sign_timer > 1)
                    {
                        fade_level = 1;
                    }
                    else
                    {
                        fade_level = m_show_level_sign_timer;
                    }
                    m_level_text.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, fade_level);
                }
                break;

                case 2://win
                {
                    if(Input.GetKeyDown(KeyCode.Escape))
                            Application.LoadLevel(Application.loadedLevel);
                }
                break;

                case 3://gameover
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                        Application.LoadLevel(Application.loadedLevel);
                }
                break;

            }

            
        }

        public void gameover_call()
        {

            game_state = 3;
            game_over = true;
            Debug.Log("GameOver");

            m_screen.GetComponent<SpriteRenderer>().sprite = m_screen_gameover;
            m_screen.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        void FixedUpdate()
        {
            if (game_state != 1) return;

            time_since_start += Time.fixedDeltaTime;
            if (m_show_level_sign_timer > 0)
            {
                m_show_level_sign_timer -= Time.fixedDeltaTime;
                if (m_show_level_sign_timer < 0) m_show_level_sign_timer = 0;
            }

            //check if spawn boat
            if (time_since_start>= time_to_spawn_next_boat && !all_boats_spawned)
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

            //check if any mirrors left
            if(!game_over)
            {
                GameObject[] mirrors;
                mirrors = GameObject.FindGameObjectsWithTag("Mirror");

                int mirror_counter = 0;
                foreach (GameObject mirror in mirrors)
                {
                    if(!mirror.GetComponent<mirrorscript>().is_destroyed)
                     mirror_counter++;
                }

                if (mirror_counter == 0)
                {
                    //game over
                    gameover_call();
                    

                }
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
                        //game over = win
                        game_state = 2;
                        game_over = true;
                        Debug.Log("Win");

                        m_screen.GetComponent<SpriteRenderer>().sprite = m_screen_win;
                        m_screen.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        Debug.Log("Next Level");
                        all_boats_spawned = false;
                        m_show_level_sign_timer = m_show_level_sign_time;
                        switch(level)
                        {
                            case 1: m_level_text.GetComponent<SpriteRenderer>().sprite = m_text_wave1; break;
                            case 2: m_level_text.GetComponent<SpriteRenderer>().sprite = m_text_wave2; break;
                            case 3: m_level_text.GetComponent<SpriteRenderer>().sprite = m_text_wave3; break;
                            case 4: m_level_text.GetComponent<SpriteRenderer>().sprite = m_text_wave4; break;
                            case 5: m_level_text.GetComponent<SpriteRenderer>().sprite = m_text_wave5; break;
                        }

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
