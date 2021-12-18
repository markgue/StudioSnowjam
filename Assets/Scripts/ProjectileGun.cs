using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProjectileGun : MonoBehaviour
{
    //bullet
    public GameObject bullet;
    public AudioClip shotSound;
    public AudioClip zeroAmmoVoice;
    public AudioClip reloadSound1;
    public AudioClip reloadSound2;
    public AudioClip reloadSound3;

    //bullet force
    public float shootForce;
    public float upwardForce;

    //Gun Stats
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;
    public int totalAmmo;
    public int magazineSize;
    public int bulletsPerTap;
    public bool allowButtonHold;

    public int extraAmmo;
    int bulletsLeft;
    int bulletsShot;

    //bools
    bool shooting;
    bool readyToShoot;
    bool reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;

    public Text ammunitionDisplay;

    //Animation
    Animator animator;

    //bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        extraAmmo = totalAmmo - magazineSize;
        readyToShoot = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MyInput();

        //set ammo display, if it exists :D
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.text = "" + bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap;
        }
    }

    private void MyInput()
    {
        //check if allowed to hold down button and take corresponding input
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        } else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }
        //Reload automatically when trying to shoot without ammo
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0 && extraAmmo > 0)
        {
            Reload();
            gameObject.GetComponent<AudioSource>().PlayOneShot(zeroAmmoVoice);
        }

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //set bullets shot to 0
            bulletsShot = 0;
            gameObject.GetComponent<AudioSource>().PlayOneShot(shotSound);
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        } else
        {
            targetPoint = ray.GetPoint(75);
        }

        //Calculate directionfrom attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        // gameObject.GetComponent<AudioSource>().clip = shotSound;
        

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsShot++;

        //Play animation
        animator.SetTrigger("Fire");

        //invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            //Allow shooting and invoking again
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        int testing = Random.Range(1, 4);
        switch (testing)
        {
            case 1:
                gameObject.GetComponent<AudioSource>().PlayOneShot(reloadSound1);
                break;
            case 2:
                gameObject.GetComponent<AudioSource>().PlayOneShot(reloadSound2);
                break;
            case 3:
                gameObject.GetComponent<AudioSource>().PlayOneShot(reloadSound3);
                break;
            default:
                gameObject.GetComponent<AudioSource>().PlayOneShot(reloadSound3);
                break;
        }

        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        //fill magazine
        int bulletsNeeded = magazineSize - bulletsLeft;
        if (extraAmmo >= bulletsNeeded)
        {
            bulletsLeft = magazineSize;
            extraAmmo -= bulletsNeeded;
        } else
        {
            bulletsLeft += extraAmmo;
            extraAmmo = 0;
        }
        
        reloading = false;
    }

    public void EquipAmmo(int amt)
    {
        extraAmmo += amt;
    }
}
