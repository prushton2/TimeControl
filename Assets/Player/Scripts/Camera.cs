using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Camera : MonoBehaviour {

    public Gun attachedGun;
    public GameObject trackedEnemy = null;
    public Canvas canvas;

    public GameObject bossBar;
    public GameObject healthBar;

    public Transform player;
    float ChgInY = 0;
    public double viewHeight = 1.5;
    private GameObject mgr;



    void Start () {
        bossBar = canvas.transform.Find("BossBar").gameObject;
        healthBar = canvas.transform.Find("HealthBar").gameObject;
    }

    void Update() {


    ChgInY += Input.GetAxisRaw("Mouse Y");
    if (ChgInY > 90f) {
      ChgInY = 90f;
    } else if(ChgInY < -90f) {
      ChgInY = -90f;
    }

    var euler = player.transform.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler(-ChgInY, euler.y, 0);        

    if(Input.GetKey(KeyCode.Mouse0)) {
        GameObject enemy = attachedGun.fire();
        if(enemy != null) {
            trackedEnemy = enemy;
        }
    }

    if(trackedEnemy != null) {
        bossBar.SetActive(true);
        bossBar.transform.Find("HealthBar").gameObject.GetComponent<Slider>().value = trackedEnemy.GetComponent<HealthPool>().getPercentage();
        bossBar.transform.Find("Name").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = trackedEnemy.GetComponent<HealthPool>().attachedTo.enemyName;
    }

    healthBar.transform.Find("HealthBar").gameObject.GetComponent<Slider>().value = this.transform.parent.GetComponent<HealthPool>().getPercentage();
    healthBar.transform.Find("HealthCount").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = this.transform.parent.GetComponent<HealthPool>().health + "/" + this.transform.parent.GetComponent<HealthPool>().maxHealth;
  }
}

