using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour
{
    private Light ambientFlashLight; // La luce per l'ambiente
    private Light flashLight; // Bagliore
    public float flashDuration = 0.1f; // Durata del bagliore
    public float flashInterval = 0.05f; // Intervallo di intermittenza



    private void Start()
    {
        ambientFlashLight = GetComponent<Light>(); // Ottiene la luce dallo stesso GameObject
        flashLight = GameObject.Find("FlashLight").GetComponent<Light>();

        if (ambientFlashLight != null && flashLight != null)
        {
            ambientFlashLight.enabled = false; // Assicurati che la luce sia inizialmente disattivata
            flashLight.enabled = false;
        }
        else
        {
            Debug.LogError("Muzzle flash light is not found on the GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MuzzleFlash());
        }
    }

    private IEnumerator MuzzleFlash()
    {
        if (ambientFlashLight == null)
        {
            yield break; // Esci se la luce non è trovata
        }

        float endTime = Time.time + flashDuration;
        while (Time.time < endTime)
        {
            ambientFlashLight.enabled = !ambientFlashLight.enabled;
            flashLight.enabled = !flashLight.enabled;
            yield return new WaitForSeconds(flashInterval);
        }
        ambientFlashLight.enabled = false; // Assicurati che la luce sia spenta alla fine
        flashLight.enabled = false;
    }
}
