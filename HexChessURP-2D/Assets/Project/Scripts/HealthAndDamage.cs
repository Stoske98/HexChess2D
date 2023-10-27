using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthAndDamage : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI damage;

    public void SetStatsTxt(int _health, int _damage)
    {
        health.text = _health.ToString();
        damage.text = _damage.ToString();
    }
    public void SetHealthTxt(int _health)
    {
        health.text = _health.ToString();
    }

    public void SetDamageTxt(int _damage)
    {
        damage.text = _damage.ToString();
    }
}
