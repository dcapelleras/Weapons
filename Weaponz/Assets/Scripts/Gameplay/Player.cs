using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] Weapons;
    [SerializeField] Transform weaponShootPoint;
    [SerializeField] GameObject shootMark;
    [SerializeField] float maxShootDistance;

    Camera cam;

    List<GameObject> marks = new List<GameObject>();

    GameObject currentWeapon;

    [SerializeField] float shootCooldown = 0.3f;
    float canShoot = 0;
    bool reloading = false;
    [SerializeField] int shootSoundIndex = 0;

    [SerializeField] int maxAmmo = 12;
    [SerializeField] int currentAmmo = 12;
    [SerializeField] float reloadTime = 2f;

    [SerializeField] TMP_Text cargador;

    private void Awake()
    {
        cam = Camera.main;
        for (int i = 0; i < 20; i++)
        {
            GameObject newMark = Instantiate(shootMark);
            newMark.transform.SetParent(transform);
            newMark.SetActive(false);
            marks.Add(newMark);
            cargador.text = currentAmmo.ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //si pulsas 1
        {
            Debug.Log("trying to change to weapon 1");
            ChangeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //si pulsas 2
        {
            Debug.Log("trying to change to weapon 2");
            ChangeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) //si pulsas 3
        {
            Debug.Log("trying to change to weapon 3");
            ChangeWeapon(2);
        }

        if (currentWeapon != null)
        {
            PointToTarget();
        }
    }

    void ChangeWeapon(int num)
    {
        if (Weapons[num] != null) //si existe arma x
        {
            if (Weapons[num].activeInHierarchy) //si el arma x ya esta activa
            {
                return;
            }
            if (currentWeapon != null) //si hay alguna otra arma activa
            {
                currentWeapon.SetActive(false);//desactiva el arma actual
            }
            currentWeapon = Weapons[num]; //el arma x pasa a ser la actual
            currentWeapon.SetActive(true); //activa el arma actual
        }
    }

    void PointToTarget()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
            Debug.Log(hit.transform.name);
            Vector3 direction = hit.point - weaponShootPoint.position;

            currentWeapon.transform.rotation = Quaternion.LookRotation(direction);
            if (Time.time > canShoot && Input.GetMouseButtonDown(0) && !reloading) //Shoot
            {
                if (currentAmmo <= 0)
                {
                    StartCoroutine(ReloadWeapon());
                    return;
                }
                GameObject mark = GetMark();
                mark.transform.position = hit.point;
                mark.transform.rotation = Quaternion.LookRotation(hit.normal);
                canShoot = Time.time + shootCooldown;
                currentAmmo--;
                cargador.text = currentAmmo.ToString();
                MusicManager.instance.ShootSound(shootSoundIndex);
            }
        }
    }

    IEnumerator ReloadWeapon()
    {
        reloading = true;
        //play reload animation
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        cargador.text = currentAmmo.ToString();
        reloading = false;
    }

    GameObject GetMark()
    {
        foreach (GameObject mark in marks)
        {
            if (!mark.activeInHierarchy)
            {
                mark.SetActive(true);
                return mark;
            }
        }
        GameObject firstMark = marks[0];
        marks.RemoveAt(0);
        marks.Add(firstMark);
        return firstMark;
    }
}
