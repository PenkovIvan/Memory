using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;//ссылка на целевой объект для инф-ии щелчка
    [SerializeField] private string targetMessage;
    public Color highLightColor= Color.cyan;

    public void OnMouseEnter()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color= highLightColor;//меняем цвет при наведении мыши
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer spriteRenderer =GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color= Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);//при нажатии меняем размер кномкпи
    }

    public void OnMouseUp()
    {
        transform.localScale=Vector3.one;//вернули размер кнопки
        if (targetObject != null)
        {//после щелчка отправляем сообщение целевому объекту
            targetObject.SendMessage(targetMessage);
        }
    }
}
