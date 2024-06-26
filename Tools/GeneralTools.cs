using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BOStuffPack.Tools
{
    public static class GeneralTools
    {
        public static T RandomElement<T>(this IEnumerable<T> e)
        {
            if (e == null)
                return default;

            var l = e.ToArray();

            if (l.Length <= 0)
                return default;

            return l[Random.Range(0, l.Length)];
        }

        public static bool TryReadIntegerReference(object obj, out IntegerReference intRef)
        {
            if(obj is IntegerReference i)
            {
                intRef = i;

                return true;
            }

            if(obj is IIntegerReferenceHolder h)
            {
                intRef = h.GetIntegerReference();

                return true;
            }

            intRef = null;
            return false;
        }

        public static bool TryReadBooleanReference(object obj, out BooleanReference boolRef)
        {
            if (obj is BooleanReference b)
            {
                boolRef = b;

                return true;
            }

            if (obj is IBooleanReferenceHolder h)
            {
                boolRef = h.GetBooleanReference();

                return true;
            }

            boolRef = null;
            return false;
        }

        public static bool MeetsIntCondition(int i, IntCondition condition)
        {
            return condition switch
            {
                IntCondition.Positive => i > 0,
                IntCondition.Negative => i < 0,

                IntCondition.NonZero => i != 0,

                _ => true
            };
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var exists = go.GetComponent<T>();

            if (exists != null)
                return exists;

            return go.AddComponent<T>();
        }

        public static T GetOrAddComponent<T>(this Component comp) where T : Component
        {
            return comp.gameObject.GetOrAddComponent<T>();
        }

        public static T AddComponent<T>(this Component comp) where T : Component
        {
            return comp.gameObject.AddComponent<T>();
        }

        public enum IntOperation
        {
            Set,

            Add,
            Subtract,

            Multiply,
            Divide
        }

        public enum BoolOperation
        {
            Set,

            Or,
            And,
            Xor
        }

        public enum IntCondition
        {
            None,

            Positive,
            Negative,

            NonZero
        }
    }
}
