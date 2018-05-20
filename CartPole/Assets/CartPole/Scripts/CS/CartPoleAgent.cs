using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartPoleAgent : Agent
{
    public GameObject pole;
    Rigidbody poleRB;
    float angle_z;
    float cart_x;
    float anguVelo_z;

    public override void InitializeAgent()
    {
        base.InitializeAgent();
        poleRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        cart_x = transform.localPosition.x;
        angle_z = pole.transform.localRotation.eulerAngles.z;
        anguVelo_z = poleRB.angularVelocity.z;
        
        float limit_angl = 30f;
        float limit_x = 3f;

        if (180f < angle_z && angle_z < 360f)
        {
            angle_z -= 360f;
        }

        if (-limit_angl < angle_z && angle_z < limit_angl)
        {
            AddReward(0.01f);
        }

        if ((-180f < angle_z && angle_z < -limit_angl) || (limit_angl < angle_z && angle_z < 180f))
        {
            AddReward(-1f);
            Done();
        }

        if (cart_x < -limit_x || limit_x < cart_x)
        {
            AddReward(-1f);
            Done();
        }
    }
    
    public override void CollectObservations()
    {
        AddVectorObs(cart_x);
        AddVectorObs(angle_z);
        AddVectorObs(anguVelo_z);
    }

    public void MoveAgent(float[] act)
    {
        //0:left 1:right
        int action = Mathf.FloorToInt(act[0]);

        if (action == 0)
        {
            transform.Translate(-0.05f, 0f, 0f);
        }
        if (action == 1)
        {
            transform.Translate(0.05f, 0f, 0f);
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        MoveAgent(vectorAction);
    }

    public override void AgentReset()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
        pole.transform.localPosition = new Vector3(0f, 1f, 0f);
        pole.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        poleRB.velocity = new Vector3(0f, 0f, 0f);
        poleRB.angularVelocity = new Vector3(0f, 0f, Random.Range(-0.5f, 0.5f));
    }

    public override void AgentOnDone()
    {

    }
}
