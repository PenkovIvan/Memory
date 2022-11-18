using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController _сontroller;
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
    {//проверка свойства контроллера canRevealCard, гарантирующа€ , что одновременно можно открыть всего две карты
        if (cardBack.activeSelf && _сontroller.canRevealCard)
        {
            cardBack.SetActive(false);
            _сontroller.CanRevealCard(this);//уведомл€ет контроллер об открытии этой карты
        }
    }
    //метод позвол€ющий компоненту SceneController снова скрыть карту вернув на место спрайт card_back
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }



}
