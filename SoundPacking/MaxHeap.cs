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
        public List<T> GETLIST()
        {
            return vals;
        }
        public int GETSIZE()
        {
            return vals.Count;
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

         private int PARENT(int i)
         {
             return ( i - 1 ) / 2;
         }
         private int LEFT(int i)
         {
             return (i * 2) + 1;
         }
         private int RIGHT(int i)
         {
            return (i * 2) + 2;
         }

        private void Heapfy_down(int i)
         {
             int l, r, mx = i;
             l = LEFT(i);
             r = RIGHT(i);
             if (l<vals.Count && vals[l].CompareTo(vals[i])>0)
                 mx = l;
                 
             if (r < vals.Count && vals[r].CompareTo(vals[mx]) > 0)
                 mx = r;
 
             if (mx != i)
             {
                 SWAP(mx, i);
                 Heapfy_down(mx);
             }
         }
        private void SWAP(int i,int j)
        {
            T tmp = vals[j];
            vals[j] = vals[i];
            vals[i] = tmp;
        }

        private void Heapfy_up(int i)
        {
            
            if (i!=0 && vals[i].CompareTo(vals[PARENT(i)])>0)
            {
                SWAP(PARENT(i), i);
                Heapfy_up(PARENT(i));
            }
        }



        private void BuildHeap()
         {
             for (int i = (vals.Count - 1) / 2; i > -1; i--)
                 Heapfy_down(i);
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
             Heapfy_down(0);
             return mx;
         }
         
         public void IncreaseKey(int i, T key)
         {
             if (key.CompareTo(vals[i]) < 0) return;
             vals[i] = key;
             while (i > 0 && vals[PARENT(i)].CompareTo(vals[i]) < 0)
             {
                 T tmp = vals[PARENT(i)];
                 vals[PARENT(i)] = vals[i];
                 vals[i] = tmp;
                 i = PARENT(i);
             }
 
         }
 
         public void PUSH(T key)
         {
             vals.Add(key);
             int i = vals.Count - 1;
             Heapfy_up(i);
 
         }

        public void POP()
        {
            vals[0] = vals.Last();
            vals.RemoveAt(vals.Count - 1);
            Heapfy_down(0);

        }

    }
}
