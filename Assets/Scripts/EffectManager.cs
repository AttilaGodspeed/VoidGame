using UnityEngine;

public class EffectManager : MonoBehaviour {

    [SerializeField] private EffectStack parentStack;
    [SerializeField] private ParticleSystem effect;

    // Use this for initialization
    void Awake () {
        restack();
	}

    void FixedUpdate () {
        //if(gameObject.IsActive()) {
            if (!effect.IsAlive())
                restack();
        //}
    }
	
    public void playAt(Vector3 pos) {
        gameObject.SetActive(true);
        gameObject.transform.position = pos;
        effect.Play();
    }

    public void restack() {
        gameObject.SetActive(false);
        parentStack.push(this);
    }
}
