using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;//������ �� ������� ������ ��� ���-�� ������
    [SerializeField] private string targetMessage;
    public Color highLightColor= Color.cyan;

    public void OnMouseEnter()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color= highLightColor;//������ ���� ��� ��������� ����
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
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);//��� ������� ������ ������ �������
    }

    public void OnMouseUp()
    {
        transform.localScale=Vector3.one;//������� ������ ������
        if (targetObject != null)
        {//����� ������ ���������� ��������� �������� �������
            targetObject.SendMessage(targetMessage);
        }
    }
}
