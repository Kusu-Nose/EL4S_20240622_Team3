using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MelonInitator : MonoBehaviour
{
    private GameObject _player;
    public GameObject _melonPrefab;

    public float MaxCD = 2.0f;
    public float _initCD = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
  
        if (_melonPrefab != null && _initCD <= 0)
        {
            float offsetZ = Random.Range(40.0f, 80.0f);
            int offsetX = Random.Range(-1, 2)  * 5;
            Vector3 melonInitePos = new Vector3(offsetX, 0.0f, _player.transform.position.z+offsetZ);


            _initCD = Random.Range(0.1f, MaxCD);
            GameObject.Instantiate(_melonPrefab, melonInitePos, Quaternion.identity);
        }

        _initCD -= Time.deltaTime;

    }
}
