// using System.Collections;
// using UnityEngine;

// public class WeirdControl : MonoBehaviour
// {
//     private RocketScript rs;
//     private void OnCollisionEnter2D(Collision2D col)
//     {
//         if (rs == null)
//         {
//             rs = col.collider.attachedRigidbody.gameObject.GetComponent<RocketScript>();
//         }
//         rs.isBroken = true;
//         StartCoroutine(RepairRocketCoroutine());
//     }
//     
//     private IEnumerator RepairRocketCoroutine()
//     {
//         yield return new WaitForSeconds(5);
//         rs.isBroken = false;
//     }
// }
