using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class Methods
    {

        public static LinkedList<Folder> worstFitLS(List<AudioFile> input, int maxcap) //O(N x M)
        {
            LinkedList<Folder> myFolders = new LinkedList<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.AddLast(firstFolder);
            for (int i = 0; i < input.Count; i++) //O(N)
            {
                int max_remain_cap = 0;
                Folder max_remain_folder = null;

                LinkedListNode<Folder> current = myFolders.First;
                while (current!=null) //O(M)
                {
                    if (current.Value.remaincap > max_remain_cap)
                    {
                        max_remain_cap = current.Value.remaincap;
                        max_remain_folder = current.Value;
                    }
                    current = current.Next;
                }

                if ((max_remain_folder != null) &&
                    (max_remain_folder.remaincap >= (int)input[i].Duration.TotalSeconds))
                {
                    max_remain_folder.addFile(input[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(input[i]);
                    myFolders.AddLast(folder);
                }

            }

            return myFolders;
        }

        public static LinkedList<Folder> worstFitDecreasingLS(List<AudioFile> input, int maxcap)
        {         
            // convert the input list to an array
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))

            LinkedList<Folder> myFolders = new LinkedList<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.AddLast(firstFolder);
            for (int i = 0; i < inputArray.Length; i++)//O(N)
            {
                int max_remain_cap = 0;
                Folder max_remain_folder = null;
                LinkedListNode<Folder> current = myFolders.First;
                for (int j = 0; j < myFolders.Count; j++)//O(M)
                {
                    if (current.Value.remaincap > max_remain_cap)
                    {
                        max_remain_cap = current.Value.remaincap;
                        max_remain_folder = current.Value;
                    }
                    current = current.Next;
                }

                if ((max_remain_folder != null) &&
                    (max_remain_folder.remaincap >= (int)inputArray[i].Duration.TotalSeconds))
                {
                    max_remain_folder.addFile(inputArray[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(inputArray[i]);
                    myFolders.AddLast(folder);
                }

            }

            return myFolders;
        }

        public static List<Folder> worstFitDecreasingHEAP(List<AudioFile> input, int maxcap)
        {
            MaxHeap<AudioFile> Audios = new MaxHeap<AudioFile>(input);
            MaxHeap<Folder> myFolders = new MaxHeap<Folder>();
            Folder firstFolder = new Folder(maxcap);
            Folder temp;
            myFolders.PUSH(firstFolder);
            while (input.Count > 0)
            {
                if(Audios.Top().Duration.TotalSeconds <= myFolders.Top().remaincap)
                {
                    myFolders.Top().addFile(Audios.Top());
                    temp = myFolders.Top();
                    myFolders.POP();
                    myFolders.PUSH(temp);

                }
                else
                {
                    temp = new Folder(maxcap);
                    temp.addFile(Audios.Top());
                    myFolders.PUSH(temp);
                }
                Audios.POP();

            }
                return myFolders.GETLIST();
        }

        public static List<Folder> worstFitHEAP(List<AudioFile> input, int maxcap)
        {
            MaxHeap<Folder> myFolders = new MaxHeap<Folder>();
            Folder firstFolder = new Folder(maxcap);
            Folder temp;
            myFolders.PUSH(firstFolder);
            for(int i=0;i<input.Count;i++)
            {
                if (input[i].Duration.TotalSeconds <= myFolders.Top().remaincap)
                {
                    myFolders.Top().addFile(input[i]);
                    temp = myFolders.Top();
                    myFolders.POP();
                    myFolders.PUSH(temp);

                }
                else
                {
                    temp = new Folder(maxcap);
                    temp.addFile(input[i]);
                    myFolders.PUSH(temp);
                }
                
            }
            return myFolders.GETLIST();
        }

        public static LinkedList<Folder> bestFitLS(List<AudioFile> input, int maxcap)
        {
            LinkedList<Folder> myFolders = new LinkedList<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.AddLast(firstFolder);
            for (int i = 0; i < input.Count; i++) //O(N)
            {
                int min_remain_cap = maxcap;
                Folder min_remain_folder = null;
                LinkedListNode<Folder> current = myFolders.First;
                for (int j = 0; j < myFolders.Count; j++) //O(M)
                {
                    if ((current.Value.remaincap <= min_remain_cap) &&
                        (current.Value.remaincap >= (int)input[i].Duration.TotalSeconds))
                    {
                        min_remain_cap = current.Value.remaincap;
                        min_remain_folder = current.Value;
                    }
                    current = current.Next;
                    
                }

                if ((min_remain_folder != null))
                {
                    min_remain_folder.addFile(input[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(input[i]);
                    myFolders.AddLast(folder);
                }

            }

            return myFolders;
        }

        public static LinkedList<Folder> firstFitLS(List<AudioFile> input, int maxcap)
        {
            LinkedList<Folder> myFolders = new LinkedList<Folder>();
            AudioFile[] inputArray = input.ToArray();
            for (int i = 0; i < inputArray.Length; i++)//O(N)
            {
                Folder remain_folder = null;
                LinkedListNode<Folder> current = myFolders.First;
                for (int j = 0; j < myFolders.Count; j++)//O(M)
                {
                    if (current.Value.remaincap >= (int)inputArray[i].Duration.TotalSeconds)
                    {

                        remain_folder = current.Value;
                        break;
                    }
                    current = current.Next;
                }

                if ((remain_folder != null))
                {
                    remain_folder.addFile(inputArray[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(inputArray[i]);
                    myFolders.AddLast(folder);
                }

            }

            return myFolders;

        }

        public static LinkedList<Folder> firstFitDecreasingLS( List<AudioFile> input, int maxcap)
        {
            LinkedList<Folder> myFolders = new LinkedList<Folder>();
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))         
            for (int i = 0; i < inputArray.Length; i++)//O(N)
            {          
                Folder remain_folder = null;
                LinkedListNode<Folder> current = myFolders.First;
                for (int j = 0; j < myFolders.Count; j++)//O(M)
                {
                    if (current.Value.remaincap >= (int)inputArray[i].Duration.TotalSeconds)
                    {
     
                        remain_folder = current.Value;
                        break;
                    }
                    current = current.Next;
                }

                if ((remain_folder != null))
                {
                    remain_folder.addFile(inputArray[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(inputArray[i]);
                    myFolders.AddLast(folder);
                }

            }
            
            return myFolders;

        }
        
        public static LinkedList<Folder> NextFitLS(List<AudioFile> input, int maxcap)
        {
            LinkedList<Folder> myFolders = new LinkedList<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.AddLast(firstFolder);
            AudioFile[] inputArray = input.ToArray();
            Folder temp;
            for (int i = 0; i < inputArray.Length; i++)
            {
                if(inputArray[i].Duration.TotalSeconds<=myFolders.Last().remaincap)
                {
                    myFolders.Last().addFile(inputArray[i]);
                }
                else
                {
                    temp = new Folder(maxcap);
                    temp.addFile(input[i]);
                    myFolders.AddLast(temp);
                }
            }
            return myFolders;
        }

        public static LinkedList<Folder> NextFitDecreasingLS(List<AudioFile> input, int maxcap)
        {
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))
            return NextFitLS(inputArray.ToList<AudioFile>(), maxcap);
        }

        public static LinkedList<Folder> bestFitDecreasingLS(List<AudioFile> input, int maxcap)
        {
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))
            return bestFitLS(inputArray.ToList<AudioFile>(), maxcap);
        }

        //attempting folder filling algorithm using DP with pseudo-polynomial algorithm
        //0-1 knapsack
        // O(N^2 x D)
        public static LinkedList<Folder> folderFilling(LinkedList<AudioFile> input, int maxcap) 
        {
            LinkedList<Folder> myFolders = new LinkedList<Folder>();
            
            while (input.Count != 0) //O(N)
            {
                int N = input.Count;
                bool[] taken = new bool[N+1];
                Folder[,] Timeline = new Folder[N+1, maxcap + 1];
                LinkedListNode<AudioFile> current = input.First;
                for (int i = 0; i <= N; i++) //O(N)
                {
                    for (int w = 0; w <= maxcap; w++) //O(D)
                    {
                        if (i == 0 || w == 0)
                            Timeline[i, w] = new Folder(maxcap);
                        else if (current.Value.Duration.TotalSeconds <= w)
                        {
                            Folder folder2 = Timeline[i - 1, w];
                            Folder folder1 = Timeline[i - 1, w - (int)current.Value.Duration.TotalSeconds];
                            /////////////////////////////////////////////////////////////////////////
                            if (taken[i] != true && folder1.remaincap >= current.Value.Duration.TotalSeconds &&
                                !folder1.files.Contains(current.Value))
                                folder1.addFile(current.Value);
                            else
                                folder1 = new Folder(maxcap);
                            ////////////////////////////////////////////////////////////////////////
                            if (folder1.remaincap <= folder2.remaincap)
                            {
                                Timeline[i, w] = folder1;
                            }
                            else
                                Timeline[i, w] = folder2;
                        }
                        else
                            Timeline[i, w] = Timeline[i - 1, w];
                    }
                    //
                    if(i!=0)
                        current=current.Next;
                    //
                }

                myFolders.AddLast(Timeline[N, maxcap]);
                LinkedListNode<AudioFile> current2 = Timeline[N, maxcap].files.First;
                for (int y = 0; y < Timeline[N, maxcap].files.Count; y++)
                {
                    int index = input.Select((item, inx) => new { item, inx }).First(x => x.item == current2.Value).inx;
                    taken[index] = true;
                    input.Remove(current2.Value);
                    current2 = current2.Next;
                   
                }

                

            }


            return myFolders;
        }

        public static LinkedList<Folder> folderFilling2(List<AudioFile> input, int maxcap)
        {
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))
            LinkedList<AudioFile> inputLL = new LinkedList<AudioFile>(inputArray);
            return folderFilling(inputLL, maxcap);
        }

        /*
        public static LinkedList<Folder> folderFilling3(List<AudioFile> input, int maxcap)
        {
            List<AudioFile> temp;
            LinkedList<Folder> myFolders = new LinkedList<Folder>();
           
            while (input.Count != 0) //O(N)
            {
                temp = new List<AudioFile>(input);
                MaxHeap<AudioFile> Audios = new MaxHeap<AudioFile>(temp);
                int N = input.Count;
                bool[] taken = new bool[N + 1];
                Folder[,] Timeline = new Folder[N + 1, maxcap + 1];
                for (int i = 0; i <= N; i++) //O(N)
                {
                    for (int w = 0; w <= maxcap; w++) //O(D)
                    {

                        if (i == 0 || w == 0)
                            Timeline[i, w] = new Folder(maxcap);
                        else if (Audios.Top().Duration.TotalSeconds <= w)
                        {
                            Folder folder2 = Timeline[i - 1, w];
                            Folder folder1 = Timeline[i - 1, w - (int)Audios.Top().Duration.TotalSeconds];
                            /////////////////////////////////////////////////////////////////////////
                            if (taken[i] != true && folder1.remaincap >= Audios.Top().Duration.TotalSeconds &&
                                !folder1.files.Contains(Audios.Top()))
                                folder1.addFile(Audios.Top());
                            else
                                folder1 = new Folder(maxcap);
                            ////////////////////////////////////////////////////////////////////////
                            if (folder1.remaincap <= folder2.remaincap)
                            {
                                Timeline[i, w] = folder1;
                            }
                            else
                                Timeline[i, w] = folder2;
                        }
                        else
                            Timeline[i, w] = Timeline[i - 1, w];

                    }

                    //
                    if (i != 0)
                        Audios.POP();
                    //

                }


                myFolders.AddLast(Timeline[N, maxcap]);
                LinkedListNode<AudioFile> current2 = Timeline[N, maxcap].files.First;
                for (int y = 0; y < Timeline[N, maxcap].files.Count; y++)
                {
                    int index = input.Select((item, inx) => new { item, inx }).First(x => x.item == current2.Value).inx;
                    taken[index] = true;
                    input.Remove(current2.Value);
                    current2 = current2.Next;
                   
                }

            }



            return myFolders;
        }
        */
        // recursive method O(2^N) too large
        /*private static Folder findBest(ref List<AudioFile> input, int maxcap,int remaincap, int n) 
        {
            int i, w;
            

            
            // Base Case
            if (n == 0 || remaincap == 0)
                return new Folder(maxcap);
            // If weight of the nth item is more than Knapsack capacity W, then
            // this item cannot be included in the optimal solution
            if (input[n - 1].Duration.TotalSeconds > remaincap)
                return findBest(ref input, maxcap, remaincap, n - 1);
            // Return the maximum of two cases: 
            // (2) not included
            else
            {
                Folder folder2 = findBest(ref input, maxcap, remaincap, n - 1);
                Folder folder1 = findBest(ref input, maxcap,  remaincap - (int)input[n-1].Duration.TotalSeconds, n - 1);
                folder1.addFile(input[n - 1]);
                if (folder1.remaincap < folder2.remaincap)  
                    return folder1;
                else
                    return folder2;           
            }         
            


        }*/


    }
}
