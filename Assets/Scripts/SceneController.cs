using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public const int gridRows = 2;//���������� �����(�����)
    public const int gridColumns = 4;//���������� �����(�������)
    public const float offSetX = 2.0f;//���������� ����� ��������
    public const float offSetY = 2.5f;//���������� ����� ���������

    private MemoryCard _firstReveald;//������ ��������
    private MemoryCard _secondReveald;//������ ��������

    [SerializeField] private MemoryCard originalCard;//������ �� ����� � �����
    [SerializeField] private Sprite[] images;//������ ��� ������ �� ������� -�������

    [SerializeField] private TMP_Text scoreLabel;// ��� ���� ���������� using TMPro;


    private int _score = 0;//����

    public bool canRevealCard
    {
        get { return _secondReveald == null; }//�-��� ������, �������. �������� false, ���� ������� ����� �������
    }



    void Start()
    {
        Vector3 startPos = originalCard.transform.position;//��������� ������ �����, ��������� ��������� ������������� �� ����

        int[] num = { 0, 0, 1, 1, 2, 2, 3, 3 };//��������� ������������� ������ � ������ ��������������� ��� ���� ������� ��������

        num = ShuffleArray(num);

        //int id=Random.Range(0, images.Length);
        //originalCard.SetCard(id, images[id]);//����� ��������� ������, ������������ � �������� MemoryCard
        for (int i = 0; i < gridColumns; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;//������ �� ��������� � ��� ������

                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridColumns + i;
                int id = num[index];
                card.SetCard(id, images[id]);//����� ��������� ������, ������������ � �������� MemoryCard

                float posX = (offSetX * i) + startPos.x;
                float posY = -(offSetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    public void CanRevealCard(MemoryCard card)
    {
        //��������� ����� � ���� �� ���� ���������� � ����������� �� ����, ����� �� ��� ��������
        if (_firstReveald == null)
        {
            _firstReveald = card;
        }
        else
        {
            _secondReveald = card;
            //Debug.Log("Match?" + (_firstReveald==_secondReveald) );//����������� �������������� ���� ����
            StartCoroutine(CheckMatch());//�������� ����������� ����� �������� ���� ����
        }
    }
    private IEnumerator CheckMatch()
    {
        if (_firstReveald.Id == _secondReveald.Id)
        {
            _score++;//����������� ���� �� ������� ��� ���������� ���������������
            scoreLabel.text = $"Score: {_score}";
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstReveald.Unreveal();//��������� ������������� �����
            _secondReveald.Unreveal();//��������� ������������� �����
        }
        _firstReveald = null;
        _secondReveald = null;
    }
    //���������� ��������� ��������� �����
    private int[] ShuffleArray(int[] num)
    {
        int[] result = num.Clone() as int[];
        for (int i = 0; i < result.Length; i++)
        {
            int temp = result[i];
            int r = Random.Range(i, result.Length);
            result[i] = result[r];
            result[r] = temp;
        }

        return result;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Scene");
    }
}
