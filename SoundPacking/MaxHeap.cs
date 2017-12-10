using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class MaxHeap<T> where T : IComparable<T>
     {
         private List<T> vals;
         public MaxHeap()
         {
             vals = new List<T>();
 
         }
         public MaxHeap(int n)
         {
             vals = new List<T>(n);
 
         }
         public MaxHeap(List<T> L)
         {
             vals = L;
             BuildHeap();
 
         }
         private void Heapfy(int i)
         {
             int l = i * 2 + 1, r, mx = i;
             r = l + 1;
             if (l<vals.Count)
             {
                 if (vals[l].CompareTo(vals[i]) >0 ) mx = l;
                
             }
             if (r > vals.Count)
             {
                 if (vals[r].CompareTo(vals[mx]) > 0) mx = r;
 
 
             }
             if (mx != i)
             {
                 T tmp = vals[mx];
                 vals[mx] = vals[i];
                 vals[i] = tmp;
                 Heapfy(mx);
             }
 
 
         }
         private void BuildHeap()
         {
             for (int i = (vals.Count - 1) / 2; i > -1; i--)
                 Heapfy(i);
         }
 
         public T Top()
         {
             return vals[0];
         }
         public T ExtraxtTop()
         {
             if (vals.Count< 1) return default(T);
             T mx = vals[0];
               vals[0]=  vals[vals.Count - 1];
             vals.RemoveAt(vals.Count - 1);
             Heapfy(0);
             return mx;
         }
         private static int Perant(int i)
         {
             return ((i & 1)==1) ? i / 2 : i / 2 - 1;
         }
         public void IncreaseKey(int i, T key)
         {
             if (key.CompareTo(vals[i]) < 0) return;
             vals[i] = key;
             while (i > 0 && vals[Perant(i)].CompareTo(vals[i]) < 0)
             {
                 T tmp = vals[Perant(i)];
                 vals[Perant(i)] = vals[i];
                 vals[i] = tmp;
                 i = Perant(i);
             }
 
         }
 
         public void Add(T key)
         {
             vals.Add(key);
             int i = vals.Count - 1;
             while (i > 0 && vals[Perant(i)].CompareTo(vals[i]) < 0)
             {
                 T tmp = vals[Perant(i)];
                 vals[Perant(i)] = vals[i];
                 vals[i] = tmp;
                 i = Perant(i);
             }
 
         }
 
     }
}
