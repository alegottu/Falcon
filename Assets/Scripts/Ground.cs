using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Player player = null;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        player.nearGround = isPlayer(obj) ? true : player.nearGround;
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        player.nearGround = isPlayer(obj) ? false : player.nearGround;
    }

    private bool isPlayer(Collider2D obj)
    {
        return obj.name.Equals(player.name);
    }
}
