using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    static public EffectSpawner instance;

    [SerializeField]
    private GameObject _effect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateEffect()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Instantiate(_effect, player.transform.position, Quaternion.identity);
    }
}
