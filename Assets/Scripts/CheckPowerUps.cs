using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPowerUps : MonoBehaviour {

    private const string doubleShotPowerUpName = "DoubleShotPowerUp";
    private const string speedUpPowerUpName = "SpeedPowerUp";
    private const string ShieldPowerUpName = "ShieldPowerUp";

    public event EventHandler OnDowbleShootPowerUP;
    public event EventHandler OnSpeedUpPowerUP;
    public event EventHandler OnEnableShieldPowerUP;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Split("(")[0].Trim() == doubleShotPowerUpName) {
            OnDowbleShootPowerUP?.Invoke(this, EventArgs.Empty);
            Destroy(other.gameObject);

        } else if (other.gameObject.name.Split("(")[0].Trim() == speedUpPowerUpName) {
            OnSpeedUpPowerUP?.Invoke(this, EventArgs.Empty);
            Destroy(other.gameObject);

        } else if (other.gameObject.name.Split("(")[0].Trim() == ShieldPowerUpName) {
            OnEnableShieldPowerUP?.Invoke(this, EventArgs.Empty);
            Destroy(other.gameObject);
        }
    }
}
