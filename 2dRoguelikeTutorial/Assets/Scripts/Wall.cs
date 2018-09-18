using UnityEngine;


public class Wall : MonoBehaviour
{
    public Sprite damageSprite;
    public int hitPoints = 4;

    private SpriteRenderer spriteRenderer;

	
	void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	

	public void DamageWall(int damageTaken)
    {
        spriteRenderer.sprite = damageSprite;
        hitPoints -= damageTaken;

        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }


}
