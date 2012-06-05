using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplexNumberCalc
{
    public class ComplexNumber : ICloneable
    {
        internal int _real;
        internal int _imaginary;

        public ComplexNumber(int real, int imaginary)
        {
            this._real = real;
            this._imaginary = imaginary;
        }

        public int Real
        {
            get
            {
                return _real;
            }
        }

        public int Imaginary
        {
            get
            {
                return _imaginary;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (Object.ReferenceEquals(obj, this))
                return true;

            ComplexNumber other = obj as ComplexNumber;

            return other != null && other._real == _real && other._imaginary == _imaginary;
        }
        
        public override int GetHashCode()
        {
            return _real ^ _imaginary;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}i", _real, _imaginary >= 0 ? "+" : "-", Math.Abs(_imaginary));
        }

        object ICloneable.Clone()
        {
            return new ComplexNumber(this._real, this._imaginary);
        }

        public static bool operator ==(ComplexNumber cn1, ComplexNumber cn2)
        {
            if (Object.ReferenceEquals(cn1, null) && Object.ReferenceEquals(cn2, null))
                return false;
            if (!Object.ReferenceEquals(cn1, null) && Object.ReferenceEquals(cn2, null))
                return false;
            if (Object.ReferenceEquals(cn1, null) && !Object.ReferenceEquals(cn2, null))
                return false;
            return cn1.Equals(cn2);
        }
        public static bool operator !=(ComplexNumber cn1, ComplexNumber cn2)
        {
            return !(cn1 == cn2);
        }
        public static ComplexNumber operator +(ComplexNumber cn1, ComplexNumber cn2)
        {
            return new ComplexNumber(cn1._real + cn2._real, cn1._imaginary + cn2._imaginary);
        }
        public static ComplexNumber operator -(ComplexNumber cn1, ComplexNumber cn2)
        {
            return new ComplexNumber(cn1._real - cn2._real, cn1._imaginary - cn2._imaginary);
        }
        public static ComplexNumber operator *(ComplexNumber cn, int factor)
        {
            return new ComplexNumber(cn._real * factor, cn._imaginary * factor);
        }
        public static ComplexNumber operator /(ComplexNumber cn, int factor)
        {
            return new ComplexNumber(cn._real / factor, cn._imaginary / factor);
        }
    }
}

