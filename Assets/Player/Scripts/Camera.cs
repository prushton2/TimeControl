using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Camera : MonoBehaviour {

    public Gun attachedGun;
    public Player playerScript;
    public Canvas canvas;

    public GameObject trackedEnemy = null;
    public GameObject bossBar;
    public GameObject healthBar;

    public Transform player;
    float ChgInY = 0;
    public double viewHeight = 1.5;
    private GameObject mgr;

    void Start () {
        bossBar = canvas.transform.Find("BossBar").gameObject;
        healthBar = canvas.transform.Find("HealthBar").gameObject;
        playerScript = transform.parent.GetComponent<Player>();
    }

    void Update() {
        
        updateHealthBars();

        if(playerScript.rewindingDeath) {
            return;
        }

        ChgInY += Input.GetAxisRaw("Mouse Y");
        ChgInY = Math.Clamp(ChgInY, -90, 90);
        
        transform.rotation = Quaternion.Euler(-ChgInY, player.transform.rotation.eulerAngles.y, 0);        

        if(Input.GetKey(KeyCode.Mouse0)) {
            GameObject enemy = attachedGun.fire();
            if(enemy != null) {
                trackedEnemy = enemy;
            }
        }

    }

    void updateHealthBars() {
        if(trackedEnemy != null) {
            bossBar.SetActive(true);
            bossBar.transform.Find("HealthBar").gameObject.GetComponent<Slider>().value = trackedEnemy.GetComponent<HealthPool>().getPercentage();
            bossBar.transform.Find("Name").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = trackedEnemy.GetComponent<HealthPool>().attachedTo.enemyName;
        }

        healthBar.transform.Find("HealthBar").gameObject.GetComponent<Slider>().value = this.transform.parent.GetComponent<HealthPool>().getPercentage();
        healthBar.transform.Find("HealthCount").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = this.transform.parent.GetComponent<HealthPool>().health + "/" + this.transform.parent.GetComponent<HealthPool>().maxHealth;
        
    }
}

