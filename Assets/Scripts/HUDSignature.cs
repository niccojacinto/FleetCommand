
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDSignature : MonoBehaviour
{
    public Ship owner;
    public Image rangeFinder;
    public Slider healthBar;
    public TMP_Text signatureName;
    public TMP_Text signatureDesignation;
    public TMP_Text signatureRangeMeter;

    Transform rangeTarget;
    Color defaultColor;
    float distanceToPlayer;
    CanvasRenderer[] canvasRenderers;

    private void Start()
    {
        rangeTarget = Player.Instance.transform;
        defaultColor = rangeFinder.color;
        healthBar.maxValue = owner.Health;
        canvasRenderers = GetComponentsInChildren<CanvasRenderer>();
    }

    private void Update()
    {
        if (!owner) return;
        transform.position = Camera.main.WorldToScreenPoint(owner.transform.position);
        healthBar.value = owner.CurrentHealth;
        distanceToPlayer = Vector3.Distance(owner.transform.position, rangeTarget.position);
        if (distanceToPlayer > 100f)
        {
            foreach (CanvasRenderer cr in canvasRenderers)
            {
                cr.SetAlpha(0);
            }
        }
        else
        {
            foreach (CanvasRenderer cr in canvasRenderers)
            {
                cr.SetAlpha(1 - distanceToPlayer / 100f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (signatureRangeMeter.gameObject.activeSelf || owner != null) signatureRangeMeter.text = distanceToPlayer.ToString("G5");
    }

    public void SetLockOn()
    {
        rangeFinder.color = new Color(Color.red.r, Color.red.g, Color.red.b, rangeFinder.color.a);
    }

    public void SetNoLockOn()
    {
        rangeFinder.color = defaultColor;
    }
}
