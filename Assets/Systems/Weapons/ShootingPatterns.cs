using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPatterns : PlayerShooting {

	Vector2 downRight= new Vector2(1, -1);
	Vector2 upLeft = new Vector2(-1, 1);

	public Vector2 rotateDirection(Vector2 v, float rad) 
	{
		float newX = (v.x * Mathf.Cos (rad)) - (v.y * Mathf.Sin (rad));
		float newY = (v.x * Mathf.Sin (rad) + (v.y * Mathf.Cos (rad)));
		return new Vector2(newX, newY) * -1;
	}

	public void ShootHalfCircle(GameObject bulletPrefab, float Angle)
	{


		m_playerShooting.Shoot (bulletPrefab, rotateDirection(Vector2.up, Angle));
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(Vector2.one, Angle));
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(upLeft, Angle));
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(Vector2.right, Angle));
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(-Vector2.right, Angle));



	}

	public void ShootForward(GameObject bulletPrefab, Vector2 direction)
	{
        m_playerShooting.Shoot(bulletPrefab, direction);
	}

	public void ShootSide(GameObject bulletPrefab, float Angle)
	{
		float radAngle = Angle * Mathf.Deg2Rad;
        m_playerShooting.Shoot(bulletPrefab, -Vector2.right * radAngle);
        m_playerShooting.Shoot(bulletPrefab, Vector2.right * radAngle);

	}

	public void ShootFork(GameObject bulletPrefab, float Angle)
	{
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(Vector2.one, Angle));
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(Vector2.right, Angle));

	}

	public void ShootCone(GameObject bulletPrefab, float Angle)
	{


        m_playerShooting.Shoot(bulletPrefab, rotateDirection(Vector2.up, Angle));
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(Vector2.one, Angle));
        m_playerShooting.Shoot(bulletPrefab, rotateDirection(upLeft, Angle));
		
	}
	public void ShootRadial(GameObject bulletPrefab)
	{
        m_playerShooting.Shoot(bulletPrefab, Vector2.up);
        m_playerShooting.Shoot(bulletPrefab, Vector2.one);
        m_playerShooting.Shoot(bulletPrefab, Vector2.right);
        m_playerShooting.Shoot(bulletPrefab, downRight);
        m_playerShooting.Shoot(bulletPrefab, -Vector2.right);
        m_playerShooting.Shoot(bulletPrefab, -Vector2.up);
        m_playerShooting.Shoot(bulletPrefab, -Vector2.one);
        m_playerShooting.Shoot(bulletPrefab, upLeft);
	}
}
