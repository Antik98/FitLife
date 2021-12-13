using System;
using System.Collections.Generic;
using UnityEngine;

namespace GradingSystem
{
    public static  class GradeCalculator 
    {
        private const int minPoints = 0;
        private const int maxKnownPoints = 5; 
        private static readonly Dictionary<int, Grade> gradingMap = new Dictionary<int, Grade>  {
            {minPoints, Grade.F }, 
            {1, Grade.E },
            {2, Grade.D },
            {3, Grade.C },
            {4, Grade.B },
            {maxKnownPoints, Grade.A }
        };

        public static Grade Calculate(int points)
        {
            if (points < minPoints)
            {
                throw new ArgumentException("Points cannot be below " + minPoints);
            }
            else if (points >maxKnownPoints)
            {
                return Grade.A; 
            }

            return gradingMap[points]; 
        }
    }
}