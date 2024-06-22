using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class SR_ItemBox : MonoBehaviour
{
    [SerializeField][Tooltip("スピードアップに必要なアイテム数")][Min(1)] int _speedUpThreshold = 3;

    [SerializeField]int _itemCount;
    [SerializeField]int _itemCountForSpeedUp;

    private BoxCollider _boxCol;

    void Start()
    {
        TryGetComponent<BoxCollider>(out _boxCol);
        _boxCol.isTrigger = true;

        _itemCount = 0;

    }

    private void Update()
    {
        SpeedUpProcess();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            ItemGetProcess();

            GameObject.Destroy(other.gameObject);
        }
    }

    private void SpeedUpProcess()
    {
        if (_itemCountForSpeedUp >= _speedUpThreshold)
        {
            //速度アップ呼び出し

            Stamina.instance.ChangeDecValue();

            _itemCountForSpeedUp = 0;
        }
    }

    private void ItemGetProcess()
    {
        /*アイテム取得数を増やす*/
        _itemCount++;
        _itemCountForSpeedUp++;

        Stamina.instance.AddStamina();
    }
}
