using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GradingSystem
{
    public static class GradeTracker
    {
        // Holding information about gained points for each subject 
        private static Dictionary<SchoolSubjectType, int> subjectMap = new Dictionary<SchoolSubjectType, int>
        {
            {SchoolSubjectType.PAI, 0},
            {SchoolSubjectType.PA1, 0},
            {SchoolSubjectType.ZMA, 0},
            {SchoolSubjectType.PS1, 0},
            {SchoolSubjectType.CAO, 0},
            {SchoolSubjectType.MLO, 0}
        };

        public static void AddPoints(SchoolSubjectType subjectType, int points)
        {
            int currentPoints = 0;
            if (subjectMap.TryGetValue(subjectType, out currentPoints))
            {
                subjectMap[subjectType] = currentPoints + points;
            }
            else
            {
                throw new ArgumentException("Cannot find subject " + subjectType.GetType().GetEnumName(subjectType));
            }
        }

        public static void ResetPoints(SchoolSubjectType subjectType)
        {
            int currentPoints = 0;
            if (subjectMap.TryGetValue(subjectType, out currentPoints))
            {
                subjectMap[subjectType] = 0;
            }
            else
            {
                throw new ArgumentException("Cannot find subject " + subjectType.GetType().GetEnumName(subjectType));
            }
        }

        public static Dictionary<SchoolSubjectType, int> GetSubjectsState()
        {
            return new Dictionary<SchoolSubjectType, int>(subjectMap);
        }

        public static int GetSubjectState(string subjectName)
        {
            var results = subjectMap.Where(s =>
                String.Equals(s.Key.ToString(), subjectName, StringComparison.CurrentCultureIgnoreCase)).ToList();

            if (results.Count == 0)
            {
                Debug.LogError("Cannot find state for subject " + subjectName);
            }

            return results[0].Value; 
        }
    }
}