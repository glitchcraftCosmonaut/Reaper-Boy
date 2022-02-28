using UnityEngine;

public class UpgradeDash : MonoBehaviour
{
    private void Awake()
    {
        if(Player.MyInstance.hasDash == true)    
        {
            gameObject.SetActive(false);
        }
    }
    //add effect here lilke SFX
   private void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag == "Player")
       {
           Debug.Log("Upgraded dash");
           Player.MyInstance.playerDashData.hasDash = true;
           gameObject.SetActive(false);
       }
   }
}
