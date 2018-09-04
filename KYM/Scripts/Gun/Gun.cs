using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(GunAnimation))]
public class Gun : MonoBehaviour {

    [SerializeField] private int maxBullet;
    [SerializeField] private float fireDelay;
    [SerializeField] private string fireSound;
    [SerializeField] private Transform firePos;
    [SerializeField] bool isRight;
    [SerializeField] Slider gunBulletCountSlider;
    [SerializeField] Text gunBulletCountText;
    [SerializeField] Color noBulletUIColor;
    Color yesBulletUIColor;
    private int currentBullet;
    private float fireCoolTime;

    GunAnimation gunAni;

    // Use this for initialization
    void Start ()
    {
        currentBullet = maxBullet;
        gunBulletCountSlider.value = currentBullet / maxBullet;
        gunBulletCountText.text = currentBullet.ToString();
        yesBulletUIColor = gunBulletCountText.color;
        fireCoolTime = 0.0f;
        gunAni = GetComponent<GunAnimation>();
	}
	
	// Update is called once per  frame
	void Update ()
    {
	    if(fireCoolTime > 0.0f)
            fireCoolTime -= Time.unscaledDeltaTime;
    }

    public void Fire()
    {
        if (firePos == null)
        {
            Debug.LogWarning("Not FirePos");
            return;
        }
        if (fireCoolTime > 0.0f)
        {
            return;
        }
        if (currentBullet <= 0)
        {    
            return;
        }

        FMODSoundManager.Instance.PlayFireSound(firePos.transform.position);
        BulletManager.Instance.CreatePlayerBaseBullet(firePos);
        fireCoolTime = fireDelay;
        currentBullet -= 1;
        gunBulletCountSlider.value = (float)currentBullet / maxBullet;
        gunBulletCountText.text = currentBullet.ToString();
        if(currentBullet <= 0)
        {
            gunBulletCountText.color = noBulletUIColor;
        }
        if (gunAni != null)
        {
            gunAni.MagazieTurn(0.1f, maxBullet);
            gunAni.ShakeGun(fireDelay * 0.9f, 10.0f, 0.05f);
            gunAni.FireParticle(firePos.position + firePos.forward * 0.1f);

            if (isRight)
                gunAni.Cartridge(transform.rotation * Quaternion.Euler(0, 40, 0));
            else
                gunAni.Cartridge(transform.rotation * Quaternion.Euler(0, -40, 0));
        }

    }

    public void Reload()
    {
        currentBullet = maxBullet;
        gunBulletCountText.text = currentBullet.ToString();
        gunBulletCountSlider.value = (float)currentBullet / maxBullet;
        gunBulletCountText.color = yesBulletUIColor;
    }
}