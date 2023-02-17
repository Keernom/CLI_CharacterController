using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using StateMachine;

public class PlayerController : MonoBehaviour
{
    private Character _characterMovementSM;

    private Rigidbody2D _rigidbody2D;
    private Dictionary<string, Vector2> _directions = new Dictionary<string, Vector2>();
    private Vector2 _startPosition;

    private void Awake()
    {
        _directions.Add("left", Vector2.left);
        _directions.Add("right", Vector2.right);
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterMovementSM = GetComponent<Character>();
        _startPosition = transform.position;
    }

    public void Jump(params string[] settings)
    {
        if (settings[0].Contains(','))
            settings[0] = settings[0].Replace(',', '.');

        float jumpForce = float.Parse(settings[0], CultureInfo.InvariantCulture);

        _characterMovementSM.JumpHeight = jumpForce;
        _characterMovementSM.StateMachine.ChangeState(_characterMovementSM.Jumping);
    }

    public void Run(params string[] settings)
    {
        _characterMovementSM.Direction = _directions[settings[0]];
        _characterMovementSM.StateMachine.ChangeState(_characterMovementSM.Running);
    }

    public void Wait(string time)
    {
        if (time.Contains(','))
            time = time.Replace(',', '.');

        float parsedTime = float.Parse(time, CultureInfo.InvariantCulture);
        int milliseconds = (int)(parsedTime * 1000);
        Thread.Sleep(milliseconds);
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
        _characterMovementSM.StateMachine.ChangeState(_characterMovementSM.Standing);
    }
}
