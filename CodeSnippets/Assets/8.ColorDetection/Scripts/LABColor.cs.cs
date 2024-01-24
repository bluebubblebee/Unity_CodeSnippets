﻿using UnityEngine;
[System.Serializable]

public struct LABColor
{
    // This script provides a Lab color space in addition to Unity's built in Red/Green/Blue colors.
    // Lab is based on CIE XYZ and is a color-opponent space with L for lightness and a and b for the color-opponent dimensions.
    // Lab color is designed to approximate human vision and so it aspires to perceptual uniformity.
    // The L component closely matches human perception of lightness.
    // Put LABColor.cs in a 'Plugins' folder to ensure that it is accessible to other scripts.

    private float m_L;
    private float m_A;
    private float m_B;

    // lightness accessors
    public float L
    {
        get { return m_L; }
        set { m_L = value; }
    }

    // a color-opponent accessor
    public float A
    {
        get { return m_A; }
        set { m_A = value; }
    }

    // b color-opponent accessor
    public float B
    {
        get { return this.m_B; }
        set { this.m_B = value; }
    }

    // constructor - takes three floats for lightness and color-opponent dimensions
    public LABColor(float l, float a, float b)
    {
        m_L = l;
        m_A = a;
        m_B = b;
    }

    // constructor - takes a Color
    public LABColor(Color col)
    {
        LABColor temp = FromColor(col);
        m_L = temp.L;
        m_A = temp.A;
        m_B = temp.B;
    }

    // static function for linear interpolation between two LABColors
    public static LABColor Lerp(LABColor a, LABColor b, float t)
    {
        return new LABColor(Mathf.Lerp(a.L, b.L, t), Mathf.Lerp(a.A, b.A, t), Mathf.Lerp(a.B, b.B, t));
    }

    // static function for interpolation between two Unity Colors through normalized colorspace
    public static Color Lerp(Color a, Color b, float t)
    {
        return (Lerp(FromColor(a), FromColor(b), t)).ToColor();
    }

    // static function for returning the color difference in a normalized colorspace (Delta-E)
    public static float Distance(LABColor a, LABColor b)
    {
        return Mathf.Sqrt(Mathf.Pow((a.L - b.L), 2f) + Mathf.Pow((a.A - b.A), 2f) + Mathf.Pow((a.B - b.B), 2f));
    }

    // static function for converting from Color to LABColor
    public static LABColor FromColor(Color c)
    {
        float D65x = 0.9505f;
        float D65y = 1.0f;
        float D65z = 1.0890f;
        float rLinear = c.r;
        float gLinear = c.g;
        float bLinear = c.b;
        float r = (rLinear > 0.04045f) ? Mathf.Pow((rLinear + 0.055f) / (1f + 0.055f), 2.2f) : (rLinear / 12.92f);
        float g = (gLinear > 0.04045f) ? Mathf.Pow((gLinear + 0.055f) / (1f + 0.055f), 2.2f) : (gLinear / 12.92f);
        float b = (bLinear > 0.04045f) ? Mathf.Pow((bLinear + 0.055f) / (1f + 0.055f), 2.2f) : (bLinear / 12.92f);
        float x = (r * 0.4124f + g * 0.3576f + b * 0.1805f);
        float y = (r * 0.2126f + g * 0.7152f + b * 0.0722f);
        float z = (r * 0.0193f + g * 0.1192f + b * 0.9505f);
        x = (x > 0.9505f) ? 0.9505f : ((x < 0f) ? 0f : x);
        y = (y > 1.0f) ? 1.0f : ((y < 0f) ? 0f : y);
        z = (z > 1.089f) ? 1.089f : ((z < 0f) ? 0f : z);
        LABColor lab = new LABColor(0f, 0f, 0f);
        float fx = x / D65x;
        float fy = y / D65y;
        float fz = z / D65z;
        fx = ((fx > 0.008856f) ? Mathf.Pow(fx, (1.0f / 3.0f)) : (7.787f * fx + 16.0f / 116.0f));
        fy = ((fy > 0.008856f) ? Mathf.Pow(fy, (1.0f / 3.0f)) : (7.787f * fy + 16.0f / 116.0f));
        fz = ((fz > 0.008856f) ? Mathf.Pow(fz, (1.0f / 3.0f)) : (7.787f * fz + 16.0f / 116.0f));
        lab.L = 116.0f * fy - 16f;
        lab.A = 500.0f * (fx - fy);
        lab.B = 200.0f * (fy - fz);
        return lab;
    }

    // static function for converting from LABColor to Color
    public static Color ToColor(LABColor lab)
    {
        float D65x = 0.9505f;
        float D65y = 1.0f;
        float D65z = 1.0890f;
        float delta = 6.0f / 29.0f;
        float fy = (lab.L + 16f) / 116.0f;
        float fx = fy + (lab.A / 500.0f);
        float fz = fy - (lab.B / 200.0f);
        float x = (fx > delta) ? D65x * (fx * fx * fx) : (fx - 16.0f / 116.0f) * 3f * (delta * delta) * D65x;
        float y = (fy > delta) ? D65y * (fy * fy * fy) : (fy - 16.0f / 116.0f) * 3f * (delta * delta) * D65y;
        float z = (fz > delta) ? D65z * (fz * fz * fz) : (fz - 16.0f / 116.0f) * 3f * (delta * delta) * D65z;
        float r = x * 3.2410f - y * 1.5374f - z * 0.4986f;
        float g = -x * 0.9692f + y * 1.8760f - z * 0.0416f;
        float b = x * 0.0556f - y * 0.2040f + z * 1.0570f;
        r = (r <= 0.0031308f) ? 12.92f * r : (1f + 0.055f) * Mathf.Pow(r, (1.0f / 2.4f)) - 0.055f;
        g = (g <= 0.0031308f) ? 12.92f * g : (1f + 0.055f) * Mathf.Pow(g, (1.0f / 2.4f)) - 0.055f;
        b = (b <= 0.0031308f) ? 12.92f * b : (1f + 0.055f) * Mathf.Pow(b, (1.0f / 2.4f)) - 0.055f;
        r = (r < 0) ? 0 : r;
        g = (g < 0) ? 0 : g;
        b = (b < 0) ? 0 : b;
        return new Color(r, g, b);
    }

    // function for converting an instance of LABColor to Color
    public Color ToColor()
    {
        return ToColor(this);
    }

    // override for string
    public override string ToString()
    {
        return "L:" + m_L + " A:" + m_A + " B:" + m_B;
    }

    // are two LABColors the same?
    public override bool Equals(System.Object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        return (this == (LABColor)obj);
    }

    // override hashcode for a LABColor
    public override int GetHashCode()
    {
        return m_L.GetHashCode() ^ m_A.GetHashCode() ^ m_B.GetHashCode();
    }

    // Equality operator
    public static bool operator ==(LABColor item1, LABColor item2)
    {
        return (item1.L == item2.L && item1.A == item2.A && item1.B == item2.B);
    }

    // Inequality operator
    public static bool operator !=(LABColor item1, LABColor item2)
    {
        return (item1.L != item2.L || item1.A != item2.A || item1.B != item2.B);
    }
}
