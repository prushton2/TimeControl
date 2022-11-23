using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public TimeController timeController;
    public RectTransform rectTransform;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        this.timeController = GameObject.Find("Time Controller").GetComponent<TimeController>();
        this.rectTransform = gameObject.GetComponent<RectTransform>();
        this.image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.timeController.isControlling) {
            this.image.color = new Color(0f, 1f, 0f, 1f);
        } else {
            this.image.color = new Color(.5f, .5f, .5f, .5f);
        }
        this.rectTransform.localRotation = Quaternion.Euler(0, 0, -timeController.time%360);
    }
}
