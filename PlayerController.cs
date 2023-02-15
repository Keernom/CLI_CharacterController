using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        _startPosition = transform.position;
    }

    public void Jump(params string[] settings)
    {
        if (settings[0].Contains(','))
            settings[0] = settings[0].Replace(',', '.');

        float jumpForce = float.Parse(settings[0], CultureInfo.InvariantCulture);
        _rigidbody2D.AddForce(Vector2.up * 200 * jumpForce);
    }

    public void Run(params string[] settings)
    {
        _rigidbody2D.AddForce(_directions[settings[0]] * 150);
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
    }
}
