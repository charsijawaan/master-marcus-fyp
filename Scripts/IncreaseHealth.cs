using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals(CharacterManager.Instance.GetPlayableCharacter().gameObject.name))
        {
            CharacterControl control = CharacterManager.Instance.GetPlayableCharacter();
            if(control.damageDetector.hp > 9)
            {
                return;
            }
            if (control != null)
            {
                control.damageDetector.hp += 1;
                CharacterManager.Instance.GetPlayableCharacter().GetComponent<DamageHealthBarLink>().healthBar.SetHealth(control.damageDetector.hp);
                this.gameObject.SetActive(false);
            }
        }
    }

}
