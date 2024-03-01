using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessPanel : MonoBehaviour
{

    private int width = 250;

    // Start is called before the first frame update
    void Start()
    {
        width = (int)transform.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        double happiness = transform.parent.GetComponent<Demographic>().Happiness;

        Image image = transform.Find("BarFill").GetComponent<Image>();
        if (happiness < 33)
            image.sprite = SpriteManager.instance.GetInterfaceSprite("FillBarRed");
        else if (happiness < 66)
            image.sprite = SpriteManager.instance.GetInterfaceSprite("FillBarYellow");
        else
            image.sprite = SpriteManager.instance.GetInterfaceSprite("FillBarGreen");

        RectTransform rectTransform = transform.Find("BarFill").GetComponent<RectTransform>();
        float desiredWidth = (float)happiness / 100 * width;
        rectTransform.sizeDelta = new Vector2(desiredWidth, rectTransform.sizeDelta.y);

        rectTransform.anchoredPosition = new Vector2(-width / 2 + desiredWidth / 2, rectTransform.anchoredPosition.y); 
    }
}
