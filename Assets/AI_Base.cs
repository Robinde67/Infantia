using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Base : MonoBehaviour {

	public Vector2
	m_xPos,
	m_xGoal,
	m_xNext;
	
	public int
	m_iSight,
	m_iSpeed,
	m_iCurrent;

	public List<GameObject> m_xaClosedList = new List<GameObject>();
	public List<GameObject> m_xaOpenList = new List<GameObject>();

	public Grid_Space m_xGrid;

	// Use this for initialization
	void Start () {
		m_iCurrent = 0;
	}
	
	// Update is called once per frame
	void Update () {
		clear ();
	
		sense(m_xGrid.test[(int)m_xPos.x][(int)m_xPos.y]);
		decide ();
		act ();
		
		if (m_xPos.x >= m_xGrid.x){
			m_xPos.x = m_xGrid.x - 1;
		}
		
		if (m_xPos.x < 0){
			m_xPos.x = 0;
		}
		
		if (m_xPos.y >= m_xGrid.y || m_xPos.y < 0){
			m_xPos.y = m_xGrid.y - 1;
		}
		
		if (m_xPos.y < 0){
			m_xPos.y = 0;
		}
		
		this.transform.position = m_xGrid.test[(int)m_xPos.x][(int)m_xPos.y].transform.position;
	}
	
	void sense (GameObject p_xGobj) {
		if (m_xaOpenList.Contains(p_xGobj)) {
			m_xaOpenList.Remove(p_xGobj);
		}
		
		if (!m_xaClosedList.Contains(p_xGobj)) {
			m_xaClosedList.Add(p_xGobj);
		}
		
		int x = 0;
		int y = 0;
		
		bool b = false;
		
		for (int i = 0; i < m_xGrid.test.Count; i++){
			for (int ii = 0; ii < m_xGrid.test[i].Count; ii++){
				if (m_xGrid.test[i][ii] == p_xGobj){
					x = i;
					y = ii;
					
					b = true;
				}
			}
		}
		
		if (!b){
			return;
		}
		
		for (int i = -1; i < 2; i++){
			for (int ii = -1; ii < 2; ii++){
				if (!m_xaClosedList.Contains(m_xGrid.test[x + i][y + ii])) {
					m_xaOpenList.Add(m_xGrid.test[x + i][y + ii]);
				}
			}
		}
	}
	
	void decide () {
		
	}
	
	void act () {
		
	}
	
	void clear () {
		m_xaClosedList.Clear();
		m_xaOpenList.Clear();
		
		m_xNext = m_xPos;
	}
	
	void move (Vector2 p_fVelocity) {
		if (p_fVelocity.x > 1){
			p_fVelocity.x = 1;
		}
		
		if (p_fVelocity.x < -1){
			p_fVelocity.x = -1;
		}
		
		if (p_fVelocity.y > 1){
			p_fVelocity.y = 1;
		}
		
		if (p_fVelocity.y < -1){
			p_fVelocity.y = -1;
		}
		
		/*this.transform.position.Set(
		this.transform.position.x + p_fVelocity.x,
		this.transform.position.y + p_fVelocity.y,
		0 );*/
		
		if (!(m_xPos.x + (int)p_fVelocity.x > m_xGrid.x || m_xPos.x < 0)){
			m_xPos.x+= (int)p_fVelocity.x;
		}
		else return;
		
		if (!(m_xPos.y + (int)p_fVelocity.y > m_xGrid.y || m_xPos.y < 0)){
			m_xPos.y+= (int)p_fVelocity.y;
		}
		else return;
		
		this.transform.position.Set(m_xGrid.test[(int)m_xPos.x + (int)p_fVelocity.x][(int)m_xPos.y + (int)p_fVelocity.y].transform.position.x,
		                            m_xGrid.test[(int)m_xPos.x + (int)p_fVelocity.x][(int)m_xPos.y + (int)p_fVelocity.y].transform.position.y,
		                            0);
	}
}
