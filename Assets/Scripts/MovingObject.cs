using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Script for movement and rotation cycles.
[System.Serializable]
public class MovementCommand
{
	public enum CommandType
	{
		Motion,
		Despawn,
		Kill
	}
	public CommandType type;                 //Determines the type of action in this command.
	[Tooltip("Also determines after how long the object should respawn if 'Despawn' is selected. 0 is never.")]
	public float duration = 1;
	public bool freezePosition = false;      //Should position be locked?
	public bool freezeRotation = false;      //Should rotation be locked?

	[Tooltip("Whether or not the speed vectors should be interpreted as target position/rotation.")]
	public bool interpretSpeedAsGoal;        //If interpreted as goal, it uses either local or worldspace coordinates, based on the movementSpace setting.
	[Tooltip("Moves the GameObject.")]
	public Vector3 speed;                    //Linear speed in this Motion event. Does nothing for the other 2 types. If interpreted as goal, this is the target position.
	[Tooltip("Rotates the GameObject.")]
	public Vector3 angularSpeed;             //Angular speed in this Motion event. Does nothing for the other 2 types. If interpreted as goal, this is the target rotation.
    [Tooltip("Sound Effect.")]
    public AudioClip sound;                  //Sound that plays when the event begins.

    public MovementCommand(CommandType type, bool interpretSpeedAsGoal, float duration, Vector3 speed, Vector3 angularSpeed, bool freezePosition, bool freezeRotation, AudioClip sound)
	{
		this.type = type;
		this.duration = duration;
		this.speed = speed;
		this.angularSpeed = angularSpeed;
		this.interpretSpeedAsGoal = interpretSpeedAsGoal;
		this.freezePosition = freezePosition;
		this.freezeRotation = freezeRotation;
        this.sound = sound;
	}
}



public class MovingObject : MonoBehaviour
{
	public enum PathType
	{
		Looping,
		PingPong,
		Single
	}

	[HideInInspector]
	public bool active = false;          //Is the movement routine active?

	public PathType type;                //MovingObject supports single-execution of the queue, looping execution, as well as ping-pong execution.
	public Space movementSpace;          //Whether we're in local or world space. Impacts how goal values are interpreted.

	private bool inRoutine = false;      //Are we currently executing an inividual movement event?
	public MovementCommand[] steps;      //List of commands which are cycled through.
	private int currentPos = 0;          //Current position in the list.

	private Vector3 originalPosition;    //Respawns to this position after a Despawn event with a time > 0.
	private Quaternion originalRotation; //Respawns in this rotation after a Despawn event with a time > 0.

    private Battery attachedBattery;     //We can attach these to batteries as well!
	private bool useBattery;             //If a battery is attached, let's rely on it for active state!

    private AudioSource a;               //In case we want to play sound effects.

	void Start()
	{
		originalPosition = transform.position;
		originalRotation = transform.rotation;
        a = GetComponent<AudioSource>();

		attachedBattery = GetComponent<Battery>();
		useBattery = attachedBattery != null;

		if (type == PathType.PingPong)
		{
			MovementCommand[] newSteps = new MovementCommand[steps.Length * 2];
			int pos = 0;
			foreach (MovementCommand step in steps)
			{
				newSteps[pos] = step;
				newSteps[newSteps.Length - 1 - pos] = new MovementCommand(step.type, step.interpretSpeedAsGoal, step.duration, -step.speed, -step.angularSpeed, step.freezePosition, step.freezeRotation, step.sound);

				pos += 1;
			}
			steps = newSteps;
		}

		Activate();
	}

	private void Update()
	{
		if (!active && useBattery && !inRoutine)
		{
			if (attachedBattery.GetActive())
			{
				Activate();
			}
		}
	}

	public void Activate()
	{
		active = true;
		if (!inRoutine && steps != null && steps.Length > 0)
		{
			StartNext();
		}
	}

	public void Pause()
	{
		active = false;
	}

	public void Reset()
	{
		active = false;
		inRoutine = false;
		transform.position = originalPosition;
		transform.rotation = originalRotation;

		if (type != PathType.Single)
		{
			Activate();
		} else
		{
			if (useBattery && currentPos == 0)
			{
				attachedBattery.PowerOff();
			}
		}

	}

	private void StartNext()
	{
		if (inRoutine && active)
		{
			int lastPos = currentPos - 1;
			if (currentPos == 0)
			{
				lastPos = steps.Length - 1;
			}
			MovementCommand m = steps[lastPos];
			OnEventEnd(lastPos, m.type, m.duration, m.interpretSpeedAsGoal, m.speed, m.angularSpeed, m.freezePosition, m.freezeRotation);
		}
		inRoutine = false;

		if (active)
		{
			MovementCommand m = steps[currentPos];
			inRoutine = true;
			OnEventStart(currentPos,m.type, m.duration, m.interpretSpeedAsGoal, m.speed, m.angularSpeed, m.freezePosition, m.freezeRotation, m.sound);
			switch (m.type)
			{
			case MovementCommand.CommandType.Motion:
				StartCoroutine(Motion(m.duration, m.speed, m.angularSpeed, m.interpretSpeedAsGoal, m.freezePosition, m.freezeRotation, m.sound));
				break;
			case MovementCommand.CommandType.Despawn:
				StartCoroutine(Despawn(m.duration, m.sound));
				break;
			case MovementCommand.CommandType.Kill:
				StartCoroutine(Kill(m.sound));
				break;
			}
			currentPos = (currentPos + 1) % steps.Length;

			if (currentPos == 0 && type == PathType.Single)
			{
				active = false;
			}
		} else {
            if (attachedBattery)
            {
                attachedBattery.Deactivate();
            }
        }
	}
    
    void PlaySound(AudioClip sound)
    {
        if (sound)
        {
            if (a != null)
            {
                a.clip = sound;
                a.Play();
            }
        }
    }

	private IEnumerator Motion(float time, Vector3 speed, Vector3 angularSpeed, bool isGoal, bool freezeP, bool freezeR, AudioClip sound)
	{

		float startTime = 0;
		Vector3 modifiedSpeed = speed;
		Vector3 modifiedAngular = angularSpeed;

		if (isGoal)
		{
			if (movementSpace == Space.Self)
			{
				modifiedSpeed = (speed - transform.localPosition) / time;
				modifiedAngular = (angularSpeed - transform.localEulerAngles) / time;
			}
			else
			{
				modifiedSpeed = (speed - transform.position) / time;
				modifiedAngular = (angularSpeed - transform.eulerAngles) / time;
			}
        }
        yield return StartCoroutine(Wait());

        while (startTime < time)
		{
			float t = Time.deltaTime;
			if (!freezeP)
			{
				transform.Translate(Vector3.Scale(modifiedSpeed, transform.lossyScale) * t, movementSpace);
			}
			if (!freezeR)
			{
				transform.Rotate(modifiedAngular * t, movementSpace);
			}
			startTime += t;
            if (startTime >= time && isGoal)
            {
                if (!freezeP)
                {
                    transform.localPosition = speed;
                }
                if (!freezeR)
                {
                    transform.localEulerAngles = speed;
                }
            }
            if (sound && startTime - t == 0)
            {
                PlaySound(sound);
            }
            yield return StartCoroutine(Wait());
        }

		if (isGoal)
		{
			if (!freezeP)
			{
				if (movementSpace == Space.Self)
				{
					transform.localPosition = speed;
				} else
				{
					transform.position = speed;
				}
			}
			if (!freezeR)
			{
				if (movementSpace == Space.Self)
				{
					transform.localEulerAngles = speed;
				}
				else
				{
					transform.eulerAngles = speed;
				}
			}
		}

		StartNext();
	}

	private IEnumerator Despawn(float time, AudioClip sound)
    {
        PlaySound(sound);
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
		if (time > 0)
		{
			yield return new WaitForSeconds(time);
			Reset();

			foreach (Transform child in children)
			{
				child.gameObject.SetActive(true);
			}
		}
	}

	private IEnumerator Kill(AudioClip sound)
    {
        PlaySound(sound);
        enabled = false;
		yield return null;
	}

	private IEnumerator Wait()
	{
		if (useBattery)
		{
			while (!attachedBattery.GetActive())
			{
				yield return null;
			}
		}
		yield return null;
	}

    //You can derive from the MovingObject class to create objects which execute special commands when an event starts or ends.
	public virtual void OnEventStart(int index, MovementCommand.CommandType type, float duration, bool isGoal, Vector3 speed, Vector3 angularSpeed, bool freezeP, bool freezeR, AudioClip sound)
	{

	}

	public virtual void OnEventEnd(int index, MovementCommand.CommandType type, float duration, bool isGoal, Vector3 speed, Vector3 angularSpeed, bool freezeP, bool freezeR)
	{

	}



	/*private IEnumerator Rotate(float time, Vector3 speed, bool isGoal)
    {
        float startTime = 0;
        Vector3 modifiedSpeed = speed;

        if (isGoal)
        {
            modifiedSpeed /= time;
        }

        while (startTime < time)
        {
            float t = Time.deltaTime;
            transform.Rotate(modifiedSpeed * t, movementSpace);
            startTime += t;
            yield return StartCoroutine(Wait());
        }

        if (isGoal)
        {
            transform.eulerAngles = speed;
        }

        StartNext();
    }

    private IEnumerator Wait(float time)
    {
        float startTime = 0;
        while (startTime < time)
        {
            startTime += Time.deltaTime;
            yield return StartCoroutine(Wait());
        }
        StartNext();
    }*/
}