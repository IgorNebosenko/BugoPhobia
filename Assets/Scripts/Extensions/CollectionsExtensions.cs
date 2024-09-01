using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ElectrumGames.Extensions
{
    public static class CollectionsExtensions
    {
        public static bool ContainsAll<T>(this IList<T> array1, IList<T> array2)
        {
            for (var i = 0; i < array2.Count; i++)
            {
                if (!array1.Contains(array2[i]))
                    return false;
            }

            return true;
        }

        public static void DeleteCollection<T>(this ICollection<T> collection) where T : MonoBehaviour
        {
            foreach (var item in collection)
            {
                var itemTransform = item.gameObject;
                Object.Destroy(itemTransform);
            }

            collection.Clear();
        }

        public static string PrintDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            var str = "";
            foreach (var keyValuePair in dictionary)
            {
                str += $"Key: {keyValuePair.Key}  --  Value: {keyValuePair.Value}\n";
            }

            return str;
        }

        public static string Print(this IEnumerable collection)
        {
            var str = "";
            foreach (var element in collection)
            {
                if (element != null)
                    str += element + "\n";
                else
                    str += "Null\n";
                
            }

            return str;
        }
        
        public static string Print<T>(this IEnumerable<T> collection, Func<T, string> toString)
        {
            var str = "";
            foreach (var element in collection)
            {
                if (element != null)
                    str += toString(element) + "\n";
                else
                    str += "Null\n";
            }

            return str;
        }

        public static bool IsNotEmpty<T>(ICollection collection)
        {
            return collection != null && collection.Count != 0;
        }

        public static List<T> RemoveNullObjectsFromList<T>(this List<T> list)
        {
            list.RemoveAll(obj => obj == null);
            return list;
        }

        public static T PickRandom<T>(this IEnumerable<T> list)
        {
            var count = list.Count();
            if (count == 0)
            {
                Debug.LogError("List is empty");
                return default;
            }

            return list.ElementAt(Random.Range(0, count));
        }
        
        public static T PickRandomParamByWeight<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector)
        {
            var totalWeight = sequence.Sum(weightSelector);
            if (Mathf.Abs(sequence.Sum(weightSelector) - 1f) >= 0.01f)
            {
                Debug.LogWarning(
                    $"Incorrect sum of weights! Sum must be equals to 1f, at sequence - sum is {totalWeight}");
            }

            return PickRandomByWeight(sequence, weightSelector);
        }
        
        public static T PickRandomByWeight<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector)
        {
            var totalWeight = sequence.Sum(weightSelector);

            var itemWeightIndex = Random.Range(0f, 1f) * totalWeight;
            var currentWeightIndex = 0f;

            foreach (var item in from weightedItem in sequence
                select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
            {
                currentWeightIndex += item.Weight;

                if (currentWeightIndex >= itemWeightIndex)
                    return item.Value;
            }

            return sequence.LastOrDefault();
        }

        public static T PickRandom<T>(this IEnumerable<T> list, System.Random random)
        {
            var count = list.Count();
            if (count == 0)
            {
                Debug.LogError("List is empty");
                return default(T);
            }

            return list.ElementAt(random.Next(0, count));
        }

        public static T PickRandom<T>(this IEnumerable<T> list, params float[] probabilityValues)
        {
            var count = list.Count();
            if (count == 0)
            {
                Debug.LogError("List is empty");
                return default(T);
            }

            var r = UnityEngine.Random.Range(0, probabilityValues.Sum());

            float s = 0;
            for (var i = 0; i < probabilityValues.Length; i++)
            {
                s += probabilityValues[i];
                if (r < s)
                    return list.ElementAt(i);
                
            }

            return list.Last();
        }

        public static T PickRandom<T>(this IEnumerable<T> list, System.Random random, params float[] probabilityValues)
        {
            var count = list.Count();
            if (count == 0)
            {
                Debug.LogError("List is empty");
                return default(T);
            }

            var r = random.NextFloat(0, probabilityValues.Sum());

            var s = 0f;
            for (var i = 0; i < probabilityValues.Length; i++)
            {
                s += probabilityValues[i];
                if (r < s)
                {
                    return list.ElementAt(i);
                }
            }

            return list.Last();
        }
        

        public static void Insert<T>(this LinkedList<T> list, int position, T item)
        {
            if (list.Count <= position)
            {
                list.AddLast(item);
                return;
            }

            var curr = list.First;
            var i = 0;
            while (i < position)
            {
                curr = curr.Next;
                i++;
            }

            list.AddBefore(curr, item);
        }
        
        public static IList<T> Shuffle<T>(this IList<T> list)  
        {  
            var n = list.Count;  
            while (n > 1) 
            {  
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);
                var value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }

            return list;
        }
	
        public static IList<T> Shuffle<T>(this IList<T> list, System.Random random)  
        {  
            var n = list.Count;  
            while (n > 1) 
            {  
                n--;  
                var k = random.Next(n + 1);  
                var value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
            return list;
        }
        
        public static ReadOnlyList<T> ToReadonlyList<T>(this IEnumerable<T> collection)
        {
            if (collection is List<T> list) return new ReadOnlyList<T>(list);
            return new ReadOnlyList<T>(collection.ToList());
        }
    }
}