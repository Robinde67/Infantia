using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Edible : MonoBehaviour {

    [System.Serializable]
    public struct Effects
    {
        public short hunger,
        poison,
        pain,
        taste;
    }

    public float m_elapsed_time;
    public float m_spawn_time;
    public short m_amount;
    public short m_max_amount;

    public Effects m_effects;

	// Use this for initialization
	void Start ()
    {
        m_elapsed_time = 0;
        m_amount = 1;

        if(m_spawn_time == 0)
        m_spawn_time = 60;

        if (m_max_amount < 1)
        {
            m_max_amount = 1;
        }
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (m_spawn_time == 0)
        {
            return;
        }
        if (m_elapsed_time >= m_spawn_time)
        {
			GetComponent<SpriteRenderer>().enabled = true;
            m_elapsed_time = 0;
            if(m_amount < m_max_amount)
            {
                m_amount++;
            }
        }
        m_elapsed_time += Time.fixedDeltaTime;
	}

    public Effects Eat()
    {
        if (m_amount != 0)
        {
            m_amount--;
            if (m_amount == 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            return m_effects;
        }
        else
        {
            return new Effects();
        }
    }
    //-----------------------------------------------------------------
    //random range, so you get a general idea by watching it, you will most likely see if something is very deadly (but you could also fail and see it as healthy).
    //memory will take precedence over perception once it have gained a memory of it.
    //-----------------------------------------------------------------
    public Effects Sense()
    {
        Effects percieve;
        percieve.hunger = (short)Random.Range(0, m_effects.hunger);
        percieve.pain = (short)Random.Range(0, m_effects.pain);
        percieve.poison = (short)Random.Range(0, m_effects.poison);
        percieve.taste = (short)Random.Range(0, m_effects.taste);
        return percieve;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		other.SendMessage("OnFoundEdible", gameObject, SendMessageOptions.DontRequireReceiver);

	}
}
