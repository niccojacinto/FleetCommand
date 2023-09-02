using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{

    public static HUD Instance;
    [SerializeField]
    HUDSignature hudsignaturePrefab;
    [SerializeField]
    PlayerShip playerShip;
    FlightController flightController;
    ShootingController shootingController;


    List<HUDSignature> hudsignatures = new List<HUDSignature>();

    [SerializeField]
    TMP_Text speed;
    [SerializeField]
    TMP_Text damage;
    [SerializeField]
    TMP_Text resources;
    [SerializeField]
    TMP_Text gunAmmoCount;
    [SerializeField]
    TMP_Text msslAmmoCount;
    [SerializeField]
    Caption captionHolder;
    AudioSource captionSource;
    

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }

        flightController = playerShip.GetComponent<FlightController>();
        shootingController = playerShip.GetComponent<ShootingController>();
        captionSource = captionHolder.GetComponent<AudioSource>();
    }

    void Update()
    {
        speed.text = Mathf.Round(flightController.CurrentSpeed).ToString();
        damage.text = (100-playerShip.Health).ToString();
        gunAmmoCount.text = shootingController.gunAmmo.ToString();
        msslAmmoCount.text = shootingController.missileAmmo.ToString();
    }

    public HUDSignature CreateSignature(Ship owner, string name, string designation, Color c)
    {
        HUDSignature hudsig = Instantiate(hudsignaturePrefab, transform) as HUDSignature;
        hudsig.name = owner.name + " signature";
        hudsig.owner = owner;
        hudsig.signatureName.text = name;
        hudsig.signatureDesignation.text = designation;

        hudsig.signatureName.color = new Color(c.r, c.g, c.b, hudsig.signatureName.color.a);
        hudsig.signatureDesignation.color = new Color(c.r, c.g, c.b, hudsig.signatureDesignation.color.a);
        hudsignatures.Add(hudsig);
        return hudsig;
    }

    public void SetResources(int amount)
    {
        resources.text = string.Format("C: +{0}", amount);
    }


    public void Say(string speaker, string message, AudioClip clip, bool isPriority)
    {
        StartCoroutine(SayCR(speaker, message, clip, isPriority));
    }
    // If priority is true.. stops all playing dialogue and plays clip
    public IEnumerator SayCR(string speaker, string message, AudioClip clip, bool isPriority)
    {
        if (isPriority) 
        {
            captionSource.Stop();
        }
        else
        {
            if (captionSource.isPlaying) yield break;
        }
        captionHolder.gameObject.SetActive(true);
        captionHolder.textSpeaker.text = speaker;
        captionHolder.textDialogue.text = string.Format("<< {0} >>", message);;
        captionSource.clip = clip;
        captionSource.Play();
        if (clip == null)
        {
            yield return new WaitForSeconds(5f);
        }
        else
        {
            yield return new WaitForSeconds(clip.length);
        }
        captionHolder.gameObject.SetActive(false);
    }
}
