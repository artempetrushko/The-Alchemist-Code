using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratedRoomInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject room;
    [SerializeField]
    [Tooltip("���� ��������� ������� � ���������. ��� 100% ����� ������� ������ ����� �������")]
    [Range(0f, 100f)]
    private float chanceToBeEnable;
    [SerializeField]
    private List<PassageInfo> passages = new List<PassageInfo>();
    /*[SerializeField]
    private List<GeneratedRoomInfo> neighboringRooms = new List<GeneratedRoomInfo>();*/

    public bool IsActive => room.activeInHierarchy;

    public void TryEnableRoom()
    {
        room.SetActive(Random.Range(1f, 100f) > 100f - chanceToBeEnable);
    }

    public void TryEnablePassages()
    {
        foreach (var passage in passages)
        {
            passage.ToggleState(passage.LinkedRooms.Where(room => room.IsActive).Count() == passage.LinkedRooms.Count);
        }
    }
}
