using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] Weapons;
    [SerializeField] Transform weaponShootPoint;
    [SerializeField] float maxShootDistance;

    GameObject currentWeapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //si pulsas 1
        {
            ChangeWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //si pulsas 2
        {
            ChangeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) //si pulsas 3
        {
            ChangeWeapon(2);
        }

        PointToTarget();
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
        RaycastHit hit;
        Ray ray = new Ray(weaponShootPoint.position, Input.mousePosition);
        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            Vector3 direction = hit.point - weaponShootPoint.position;

            currentWeapon.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
