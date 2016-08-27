using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {

        private GameObject[] _spawnPoints;
        private GameObject _sun;

        // Use this for initialization
        void Start ()
        {
            _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        }
	
        // Update is called once per frame
        void Update () {
            
        }
    }
}
