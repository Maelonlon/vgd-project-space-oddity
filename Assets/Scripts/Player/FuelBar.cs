using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FuelBar : MonoBehaviour
{
    private Image fuelBar;
    private float currentFuel;
    private float maxFuel;

    private void Start()
    {
        fuelBar = GetComponent<Image>();
        GameObject player = Locator.GetInstance().GetPlayer();
        if (!player)
        {
            Debug.Log("Player reference null (AlienShoot.cs)");
        }
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        maxFuel = playerMovement.maxFuel;
        playerMovement.OnFuelChanged += OnFuelChanged;
        currentFuel = maxFuel;
    }

    public void OnFuelChanged(float newValue)
    {
        newValue = Mathf.Clamp(newValue, 0f, maxFuel);
        currentFuel = newValue;
        fuelBar.fillAmount = currentFuel / maxFuel;

    }
}
