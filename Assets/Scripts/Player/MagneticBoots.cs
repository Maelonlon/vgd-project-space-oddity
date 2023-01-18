using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBoots : MonoBehaviour
{
    Renderer rend;
    public MagneticBootsArea magneticArea;
    public float magneticForce = 5f;
    public float magneticTorque = 2f;
    public float clampMaxForce = 100f;
    public float clampMinForce = 100f;
    public float ClampMaxTorque = 100f;
    public float ClampMinTorque = 100f;

    public float minDistanceFromMagnetic = 0.01f;

    bool active = false;

    public bool isActive
    {
        get { return active; }
    }

    bool isMagneticClose = false;

    [HideInInspector]
    public Vector2 movementInput = Vector2.zero;


    private PlayerMovement player;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        var playerGO = Locator.GetInstance().GetPlayer();
        if (!playerGO)
        {
            Debug.Log("Player reference null (MagneticBoots.cs)");
        }
        player = playerGO.GetComponent<PlayerMovement>();

    }


    public void Activate()
    {
        active = true;
        rend.enabled = true;
    }

    public void Deactivate()
    {
        active = false;
        rend.enabled = false;
        player.SetAdjustingPlayerRotation(true);
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (active)
        {
            //Setup
            int count = magneticArea.magneticObjectsInArea.Count;

            player.SetAdjustingPlayerRotation(count == 0);

            Vector2[] forces = new Vector2[count];
            float[] torques = new float[count];


            isMagneticClose = false;

            for (int i = 0; i < count; i++)
            {
                forces[i] = Vector2.zero;
                torques[i] = 0f;
            }


            for (int i = 0; i < count; i++)
            {
                Collider2D magnetic = magneticArea.magneticObjectsInArea[i];

                Vector3 point = magnetic.ClosestPoint(transform.position);
                Vector3 normal = Physics2D.Linecast(transform.position, point).normal;
                Debug.DrawLine(point, point + normal, Color.cyan, 0.05f);

                Vector2 direction = point - transform.position;
                if (direction.magnitude > minDistanceFromMagnetic)
                {
                    Vector2 force = GetMagneticForce(direction);
                    if (force.magnitude > clampMinForce)
                        forces[i] = force;

                    float torque = GetMagneticTorque(direction, normal);
                    if (Mathf.Abs(torque) > ClampMinTorque)
                        torques[i] = torque;

                }
                else
                {
                    isMagneticClose = true;
                    player.ChangeVelocity(0f);
                    break;
                }
            }

            if (!isMagneticClose)
            {
                foreach (Vector2 force in forces)
                    player.AddForce(force, transform.position);
                foreach (float torque in torques)
                    player.AddTorque(torque);

            }

        }
    }

    Vector2 GetMagneticForce(Vector2 direction)
    {
        Vector2 force = direction.normalized * magneticForce / (direction.magnitude * direction.magnitude);
        return Vector2.ClampMagnitude(force, clampMaxForce);
    }

    float GetMagneticTorque(Vector2 direction, Vector2 normal)
    {
        if (direction.magnitude < minDistanceFromMagnetic * 5f) return 0f;
        float angle = Vector2.SignedAngle(player.transform.up, normal);
        float torque = angle * magneticTorque / (direction.magnitude * direction.magnitude);
        return Mathf.Clamp(torque, -ClampMaxTorque, ClampMaxTorque);
    }




}
