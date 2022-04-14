using UnityEngine;

public interface IColliderController
{
    public void OnCollisionEnter2D(Collision2D collision);
    public void OnTriggerEnter2D(Collider2D collider2d);
}