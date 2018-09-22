using UnityEngine;


public class Wall : MonoBehaviour
{
    public Sprite damageSprite;
    public int hitPoints = 4;

	public AudioClip chopSound1;
	public AudioClip chopSound2;

	private SpriteRenderer spriteRenderer;

	
	void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void DamageWall(int damageTaken)
    {
		SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
        spriteRenderer.sprite = damageSprite;
        hitPoints -= damageTaken;

        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
