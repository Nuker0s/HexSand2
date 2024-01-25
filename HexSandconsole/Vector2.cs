using System;

public class Vector2
{
    public float x { get; set; }
    public float y { get; set; }

    public Vector2(float xv, float yv)
    {
        x = xv;
        y = yv;
    }

    // Addition
    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    // Subtraction
    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }

    // Scalar multiplication
    public static Vector2 operator *(Vector2 a, float scalar)
    {
        return new Vector2(a.x * scalar, a.y * scalar);
    }

    // Scalar division
    public static Vector2 operator /(Vector2 a, float scalar)
    {
        if (Math.Abs(scalar) < float.Epsilon)
        {
            throw new ArgumentException("Division by zero is not allowed.");
        }
        return new Vector2(a.x / scalar, a.y / scalar);
    }

    // Dot product
    public static float Dot(Vector2 a, Vector2 b)
    {
        return a.x * b.x + a.y * b.y;
    }

    // Magnitude (length) of the vector
    public float Magnitude()
    {
        return (float)Math.Sqrt(x * x + y * y);
    }

    // Normalize the vector (make it a unit vector)
    public void Normalize()
    {
        float magnitude = Magnitude();
        if (magnitude > 0)
        {
            x /= magnitude;
            y /= magnitude;
        }
    }

    // Override ToString for easy debugging
    public override string ToString()
    {
        return $"({x}, {y})";
    }
}
