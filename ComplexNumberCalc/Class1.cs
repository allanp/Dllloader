using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplexNumberCalc
{
    public class ComplexNumber : ICloneable
    {
        internal int real;
        internal int imaginary;

        public ComplexNumber(int real, int imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return ((ComplexNumber)obj).real == real &&
                ((ComplexNumber)obj).imaginary == imaginary;
        }
        
        public override int GetHashCode()
        {
            return real ^ imaginary;
        }

        public override string ToString()
        {
            if( imaginary >= 0)
                return string.Format("{0} + {1}i", real, imaginary);
            else
                return string.Format("{0} - {1}i", real, Math.Abs(imaginary));
        }

        object ICloneable.Clone()
        {
            return new ComplexNumber(this.real, this.imaginary);
        }
    }

    public class ComplexNumberCalc
    {
        public static ComplexNumber Plus(ComplexNumber cn1, ComplexNumber cn2)
        {
            return new ComplexNumber(cn1.real + cn2.real, cn1.imaginary + cn2.imaginary);
        }

        public static ComplexNumber Minus(ComplexNumber cn1, ComplexNumber cn2)
        {
            return new ComplexNumber(cn1.real - cn2.real, cn1.imaginary - cn2.imaginary);
        }

        public static ComplexNumber Multiply(ComplexNumber cn, int factor)
        {
            return new ComplexNumber(cn.real * factor, cn.imaginary * factor);
        }

        public static ComplexNumber Divide(ComplexNumber cn, int factor)
        {
            return new ComplexNumber(cn.real / factor, cn.imaginary / factor);
        }
    }
}

