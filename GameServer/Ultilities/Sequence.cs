using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice
{
    public static class Sequence
    {
        public static T[] ReadBlock<T>(T[] source, int offset, int length)
        {
            T[] outputSeq = new T[length];
            Array.Copy(source, offset, outputSeq, 0, length);

            return outputSeq;
        }

        public static T[] Concat<T>(params T[][] sequences)
        {
            return sequences.Aggregate(Concat);
        }

        private static T[] Concat<T>(T[] firstSeq, T[] secondSeq)
        {
            T[] outputSeq = new T[firstSeq.Length + secondSeq.Length];

            Array.Copy(firstSeq, 0, outputSeq, 0, firstSeq.Length);
            Array.Copy(secondSeq, 0, outputSeq, firstSeq.Length, secondSeq.Length);

            return outputSeq;
        }
    }
}
