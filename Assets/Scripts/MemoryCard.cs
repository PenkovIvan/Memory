using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController _�ontroller;
    private int _id;
    public int Id
    {
        get { return _id; }
    }
    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void OnMouseDown()
    {//�������� �������� ����������� canRevealCard, ������������� , ��� ������������ ����� ������� ����� ��� �����
        if (cardBack.activeSelf && _�ontroller.canRevealCard)
        {
            cardBack.SetActive(false);
            _�ontroller.CanRevealCard(this);//���������� ���������� �� �������� ���� �����
        }
    }
    //����� ����������� ���������� SceneController ����� ������ ����� ������ �� ����� ������ card_back
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }



}
