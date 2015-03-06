using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Base : MonoBehaviour {

	public Vector2
	m_xPos,
	m_xGoal,
	m_xNext;
	
	public List<Vector2> m_xaPath;
	
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
	
		sense(m_xGrid.test[(int)m_xPos.x][(int)m_xPos.y], 0);
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
	
	void sense (GameObject p_xGobj, int p_G) {		
		m_xaOpenList.Clear();
		
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
				if (!(i + x > m_xGrid.x || i + x < 0)){
					if (!(ii + y > m_xGrid.y || ii + y < 0)){
						if (!m_xaClosedList.Contains(m_xGrid.test[x + i][y + ii])) {
							m_xaOpenList.Add(m_xGrid.test[x + i][y + ii]);
						}
					}
				}
			}
		}
		
		if (m_xaOpenList.Count > 0){
			int 
			_iF = int.MaxValue,
			_iIx = 0,
			_iIy = 0;
			
			for (int i = 0; i < m_xGrid.x; i++){
				for (int ii = 0; ii < m_xGrid.y; ii++){
					if (m_xaOpenList.Contains(m_xGrid.test[i][ii])){
						int _iFf = get_F(10, i, ii);
						
						if (_iFf < _iF){
							_iF = _iFf;
							
							_iIx = i;
							_iIy = ii;
						}
					}
				}
			}
			
			if ((_iIx == (int)m_xGoal.x && _iIy == (int)m_xGoal.y) || (p_G >= m_iSight)){
				
			}
			else {
				sense (m_xGrid.test[_iIx][_iIy], p_G + 1);
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
	
	int get_F(int G, int Hx, int Hy){
		Vector2 _xV;
		_xV.x = (m_xGoal.x - Hx);
		_xV.y = (m_xGoal.y - Hy);
		
		if (_xV.x < 0){
			_xV.x *= (-1);
		}
		
		if (_xV.y < 0){
			_xV.y *= (-1);
		}
		
		return (int)(_xV.x + _xV.y);
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
