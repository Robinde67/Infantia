using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path {
	public Path(int p_iG, int p_iX, int p_iY, int p_iGoalX, int p_iGoalY, GameObject p_xGobj){
		m_iX = p_iX;
		m_iY = p_iY;
		
		m_iGoalX = p_iGoalX;
		m_iGoalY = p_iGoalY;
		
		m_iF = (m_iG = p_iG) + (m_iH = get_H(p_iX, p_iY));
		
		m_xGameObject = p_xGobj;
	}
	
	public bool m_bSearch;
	
	public int 
		m_iX, m_iY, 
		m_iF, m_iG, m_iH,
		m_iGoalX, 
		m_iGoalY;
	
	public GameObject 
		m_xGameObject,
		m_xParent;
	
	int get_H(int Hx, int Hy){
		Vector2 _xV;
		_xV.x = (m_iGoalX - Hx);
		_xV.y = (m_iGoalY - Hy);
		
		if (_xV.x < 0){
			_xV.x *= (-1);
		}
		
		if (_xV.y < 0){
			_xV.y *= (-1);
		}
		
		return (int)(_xV.x + _xV.y);
	}
};

public class AI_Base : MonoBehaviour {
	
	public Vector2
		m_xPos,
		m_xGoal,
		m_xNext;
	
	public List<Path> m_xaPath = new List<Path>();
	
	public int
		m_iSight,
		m_iSpeed,
		m_iCurrent;
	
	public List<Path> m_xaClosedList = new List<Path>();
	public List<Path> m_xaOpenList = new List<Path>();
	
	public Grid_Space m_xGrid;
	
	// Use this for initialization
	void Start () {
		m_iCurrent = m_iSpeed;
	}

	void FixedUpdate () {
		//sense(m_xGrid.test[(int)m_xPos.x][(int)m_xPos.y], 0);
		if (m_iCurrent >= m_iSpeed){
			m_xaPath.Clear();
			sense (new Path(0, (int)m_xPos.x, (int)m_xPos.y, (int)m_xGoal.x, (int)m_xGoal.y, m_xGrid.test[(int)m_xPos.x][(int)m_xPos.y]));
			m_iCurrent = 0;
		}
		
		decide ();
		act ();
		
		clear ();
		
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
		
		if ((int)m_xPos.x != (int)m_xGoal.x && (int)m_xPos.y != (int)m_xPos.y){
			print (m_xPos.x);
			print (m_xPos.y);
			print (" ");
		}
		
		gameObject.transform.position = m_xGrid.test[(int)m_xPos.x][(int)m_xPos.y].transform.position;
	}
	
	void sense (Path p_xGobj) {
		if (m_xaOpenList.Contains(p_xGobj)){
			m_xaOpenList.Remove(p_xGobj);
		}
		
		if (!(m_xaClosedList.Contains(p_xGobj))) {
			m_xaClosedList.Add(p_xGobj);
		}
		
		for (int i = 0; i < m_xaOpenList.Count; i++){
			m_xaOpenList[i].m_bSearch = false;
		}
		
		int x = 0;
		int y = 0;
		
		bool b = false;
		
		for (int i = 0; i < m_xGrid.test.Count; i++){
			for (int ii = 0; ii < m_xGrid.test[i].Count; ii++){
				if (m_xGrid.test[i][ii] == p_xGobj.m_xGameObject){
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
				if (!(i + x >= m_xGrid.x || i + x < 0)){
					if (!(ii + y >= m_xGrid.y || ii + y < 0)){
						//if (!(i == 0 && ii == 0)) {
						m_xaOpenList.Add(new Path(p_xGobj.m_iG + get_G(10, i, ii, m_xGrid.test[x + i][y + ii]), i + x, ii + y, (int)m_xGoal.x, (int)m_xGoal.y, m_xGrid.test[x + i][y + ii]));
						//}
					}
				}
			}
		}
		
		if (m_xaOpenList.Count > 0){
			int 
				_iF = int.MaxValue,
				_iI = 0;
			
			for (int i = 0; i < m_xaOpenList.Count; i++){				
				if (m_xaOpenList[i].m_iF < _iF){
					_iF = m_xaOpenList[i].m_iF;
					
					_iI = i;
				}
			}
			
			m_xaPath.Add(m_xaOpenList[_iI]);
			
			/*print (m_xaOpenList[_iI].m_iG);
			
			print (m_xaOpenList[_iI].m_iX);
			print (m_xaOpenList[_iI].m_iY);
			
			print ((int)m_xGoal.x);
			print ((int)m_xGoal.y);
			
			print ("-");*/
			
			if ((m_xaOpenList[_iI].m_iX == (int)m_xGoal.x && m_xaOpenList[_iI].m_iY == (int)m_xGoal.y) || (m_xaOpenList[_iI].m_iG >= m_iSight)){
				return;
			}
			else {
				sense (m_xaOpenList[_iI]);
			}
		}
	}
	
	void decide () {		
		/*if (m_xaPath.Count >= 0){
			m_xNext = m_xaPath[0].m_xGameObject;
			m_xaPath.Remove(m_xaPath[0]);
		}*/
	}
	
	void act () {
		/*print (m_iCurrent);
		print (m_xaPath[0].m_iX);
		print (m_xaPath[0].m_iY);
		
		print ("-");*/
		
		if (m_xaPath.Count > 0){
			m_xPos.x = m_xaPath[0].m_iX;
			m_xPos.y = m_xaPath[0].m_iY;
			
			m_xaPath.Remove(m_xaPath[0]);
			
			m_iCurrent++;
		}
	}
	
	void clear () {
		m_xaClosedList.Clear();
		m_xaOpenList.Clear();
		
		//m_xNext = m_xPos;
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
	
	int get_G(int p_iG, int p_iX, int p_iY, GameObject p_xGobj){
		if (p_iX / 1 == p_iY / 1 && !(p_iX == 0 || p_iY == 0)){
			p_iG += 4;
		}
		/*
		p_iG+=
		+ m_xGrid.is_in_critters(10, p_xGobj)
		+ m_xGrid.is_in_edible(10, p_xGobj)
		+ m_xGrid.is_in_impassable(50, p_xGobj)
		+ m_xGrid.is_in_passable(5, p_xGobj)
		+ m_xGrid.is_in_predators(50, p_xGobj)
		+ m_xGrid.is_in_water(25, p_xGobj);
		*/
		return p_iG;
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
