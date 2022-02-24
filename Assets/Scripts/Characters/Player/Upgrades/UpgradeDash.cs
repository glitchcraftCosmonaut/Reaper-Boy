using UnityEngine;

public class UpgradeDash : MonoBehaviour
{
    //add effect here lilke SFX
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag == "Player")
       {
           Debug.Log("Upgraded dash");
           PlayerDashData.hasDash = true;
           gameObject.SetActive(false);
       }
   }
}
