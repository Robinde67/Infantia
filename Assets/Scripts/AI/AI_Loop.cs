using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Loop : MonoBehaviour
{
    public enum Activity
    {
        STANDBY,
        MOVE,
        EAT,
        INTERACT,
        SLEEP
    }
    
    public struct Action
    {
        public Vector3 location;
        public Activity act;
        public float weight;
    }

    public Grid_Space m_grid;
    Personality m_personality;
    Health m_health;
    Memory m_memory;
    AI_astar m_astar;

    private float sprint;
    public float maxsprint;

    Action m_current_action;
    Action m_proposed_action;
    public bool m_forced_action;

	// Use this for initialization
	void Start ()
    {
        m_personality = gameObject.GetComponent<Personality>();
        m_health = gameObject.GetComponent<Health>();
        m_memory = gameObject.GetComponent<Memory>();
        m_astar = gameObject.GetComponent<AI_astar>();

        if (maxsprint == 0)
        {
            maxsprint = 1;
        }
        sprint = 0.0f;

        m_current_action = new Action();
        m_proposed_action = new Action();
        m_forced_action = false;
       
	
	}
	
	void FixedUpdate ()
    {
        Decide();
        if (Evaluate())
        {
            Act();
        }
        sprint += Time.fixedDeltaTime;
	}

    void Decide()
    {
        //Needs to be done
        //Set current weight to 0 and action to STANDBY when the destination is reached and the action performed

        if (sprint > maxsprint){
            print ("Sprint:" + sprint);
            sprint = 0.0f;

            m_proposed_action.act = Activity.STANDBY;
			m_proposed_action.location = new Vector3();
			m_proposed_action.weight = 0;
            if(m_health.hunger < 100 + m_personality.proactive * 5)
            {
                m_proposed_action.act = Activity.EAT;
                for(int i = 0; i< m_memory.memories_edible.Count; i++)
                {
                    int val = (int)m_memory.memories_edible[i].GetValue() * 11 + m_personality.proactive;
                    float closest = float.MaxValue;
                    int index = 0;
                    for(int j = 0; j < m_memory.memories_edible[i].positions.Count; j++)
                    {
                        float dx = gameObject.transform.position.x - m_memory.memories_edible[i].positions[j].x;
                        float dy = gameObject.transform.position.y - m_memory.memories_edible[i].positions[j].y;
                        float d = Mathf.Sqrt(dx * dx + dy * dy) * 11 - m_personality.proactive;
                        if(d < closest)
                        {
                            closest = d;
                            index = j;
                        }
                    }
                    val -= (int)closest;
                    val += 255 - (int)m_health.hunger;
                    if(val > m_proposed_action.weight)
                    {
                        m_proposed_action.location = m_memory.memories_edible[i].positions[index];
                        m_proposed_action.weight = val;

                    }
                    if (m_health.hunger < 30)
                    {
                        m_forced_action = true;
                    }
                }
            }
            if(m_health.sleepy < m_health.hunger && m_health.sleepy < 50 + -m_personality.proactive * 3)
            {
                Action sleepi = new Action();
                sleepi.act = Activity.SLEEP;
                sleepi.location = gameObject.transform.position;
                sleepi.weight = 511 - m_health.sleepy * 2;

                if (sleepi.weight > m_proposed_action.weight)
                {
                    m_proposed_action = sleepi;
                }
            }
            if(m_health.boredom < 150 && m_health.boredom < m_health.hunger && m_health.boredom < m_health.sleepy)
            {
                //look for things to interact with, or other Infants to share information with.... later
            }
            if(m_proposed_action.act == Activity.STANDBY && m_current_action.act != Activity.MOVE)
            {
                Action explore = new Action();
                explore.act = Activity.MOVE;
                explore.weight = Random.Range(0,100);
                explore.location = new Vector3(Random.Range(0,m_grid.x), Random.Range(0,m_grid.x));
                if(explore.weight > m_proposed_action.weight)
                {
                    m_proposed_action = explore;
                }
            }
        }
    }
    bool Evaluate()
    {
        if (m_forced_action)
        {
            m_forced_action = false;
            m_current_action = m_proposed_action;
            return true;
        }
        if (m_current_action.act == Activity.SLEEP && m_health.sleepy < 200 + Random.Range(0,55))
        {
            return false;
        }
        if (m_proposed_action.weight > m_current_action.weight)
        {
            m_current_action = m_proposed_action;
            return true;
        } else
        {
            //m_current_action.weight+= Time.fixedDeltaTime * 5;
            return false;
        }
    }
    void Act()
    {
        m_astar.Recalculate(m_current_action.location);
        m_current_action.weight += 1;
    }

}