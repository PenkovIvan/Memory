using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public const int gridRows = 2;//количество €чеек(строк)
    public const int gridColumns = 4;//количество €чеек(колонок)
    public const float offSetX = 2.0f;//рассто€ние между строками
    public const float offSetY = 2.5f;//рассто€ние между колонками

    private MemoryCard _firstReveald;//первое открытие
    private MemoryCard _secondReveald;//второе открытие

    [SerializeField] private MemoryCard originalCard;//ссылка на карты в сцене
    [SerializeField] private Sprite[] images;//ћассив дл€ ссылок на ресурсы -спрайты

    [SerializeField] private TMP_Text scoreLabel;// при этом подключаем using TMPro;


    private int _score = 0;//счет

    public bool canRevealCard
    {
        get { return _secondReveald == null; }//ф-ци€ чтени€, возвращ. значение false, если второа€ карта открыта
    }



    void Start()
    {
        Vector3 startPos = originalCard.transform.position;//положение первой карты, положение остальных отсчитываетс€ от этой

        int[] num = { 0, 0, 1, 1, 2, 2, 3, 3 };//объ€вл€ем целочисленный массив с парами идентификаторов дл€ всех четырех спрайтов

        num = ShuffleArray(num);

        //int id=Random.Range(0, images.Length);
        //originalCard.SetCard(id, images[id]);//вызов открытого метода, добавленного в сценарий MemoryCard
        for (int i = 0; i < gridColumns; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;//ссылка на контейнер с исх картой

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
                card.SetCard(id, images[id]);//вызов открытого метода, добавленного в сценарий MemoryCard

                float posX = (offSetX * i) + startPos.x;
                float posY = -(offSetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    public void CanRevealCard(MemoryCard card)
    {
        //—охран€ем карты в одну из двух переменных в зависимости от того, кака€ из них свободна
        if (_firstReveald == null)
        {
            _firstReveald = card;
        }
        else
        {
            _secondReveald = card;
            //Debug.Log("Match?" + (_firstReveald==_secondReveald) );//сравнимваем идентификаторы двух карт
            StartCoroutine(CheckMatch());//вызываем сопрограмму после открыти€ двух карт
        }
    }
    private IEnumerator CheckMatch()
    {
        if (_firstReveald.Id == _secondReveald.Id)
        {
            _score++;//увеличиваем счет на единицу при совпадении идентификаторов
            scoreLabel.text = $"Score: {_score}";
        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstReveald.Unreveal();//закрываем несовпадающие карты
            _secondReveald.Unreveal();//закрываем несовпадающие карты
        }
        _firstReveald = null;
        _secondReveald = null;
    }
    //–еализаци€ алгоритма тасовани€  нута
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
