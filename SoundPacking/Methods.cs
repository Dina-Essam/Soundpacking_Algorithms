using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class Methods
    {

        public static List<Folder> worstFitLS(List<AudioFile> input, int maxcap) //O(N x M)
        {
            List<Folder> myFolders = new List<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.Add(firstFolder);
            for (int i = 0; i < input.Count; i++) //O(N)
            {
                int max_remain_cap = 0;
                Folder max_remain_folder = null;

                for (int j = 0; j < myFolders.Count; j++) //O(M)
                {
                    if (myFolders[j].remaincap > max_remain_cap)
                    {
                        max_remain_cap = myFolders[j].remaincap;
                        max_remain_folder = myFolders[j];
                    }
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
                    myFolders.Add(folder);
                }

            }

            return myFolders;
        }

        public static List<Folder> worstFitDecreasingLS(List<AudioFile> input, int maxcap)
        {
            // sort the input using the built-in list sorting method
            //input.Sort((x, y) => -1 * x.Duration.CompareTo(y.Duration));
         
            // convert the input list to an array
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))

            List<Folder> myFolders = new List<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.Add(firstFolder);
            for (int i = 0; i < inputArray.Length; i++)
            {
                int max_remain_cap = 0;
                Folder max_remain_folder = null;

                for (int j = 0; j < myFolders.Count; j++)
                {
                    if (myFolders[j].remaincap > max_remain_cap)
                    {
                        max_remain_cap = myFolders[j].remaincap;
                        max_remain_folder = myFolders[j];
                    }
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
                    myFolders.Add(folder);
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

        public static List<Folder> bestFitLS(List<AudioFile> input, int maxcap)
        {
            List<Folder> myFolders = new List<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.Add(firstFolder);
            for (int i = 0; i < input.Count; i++)
            {
                int min_remain_cap = maxcap;
                Folder min_remain_folder = null;

                for (int j = 0; j < myFolders.Count; j++)
                {
                    if ((myFolders[j].remaincap <= min_remain_cap) &&
                        (myFolders[j].remaincap >= (int)input[i].Duration.TotalSeconds))
                    {
                        min_remain_cap = myFolders[j].remaincap;
                        min_remain_folder = myFolders[j];
                    }
                    
                }

                if ((min_remain_folder != null))
                {
                    min_remain_folder.addFile(input[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(input[i]);
                    myFolders.Add(folder);
                }

            }

            return myFolders;
        }

        public static List<Folder> firstFitLS(List<AudioFile> input, int maxcap)
        {
            List<Folder> myFolders = new List<Folder>();
            AudioFile[] inputArray = input.ToArray();
            for (int i = 0; i < inputArray.Length; i++)
            {
                Folder remain_folder = null;
                for (int j = 0; j < myFolders.Count; j++)
                {
                    if (myFolders[j].remaincap >= (int)inputArray[i].Duration.TotalSeconds)
                    {

                        remain_folder = myFolders[j];
                        break;
                    }
                }

                if ((remain_folder != null))
                {
                    remain_folder.addFile(inputArray[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(inputArray[i]);
                    myFolders.Add(folder);
                }

            }

            return myFolders;

        }

        public static List<Folder> firstFitDecreasingLS( List<AudioFile> input, int maxcap)
        {
            List<Folder> myFolders = new List<Folder>();
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))         
            for (int i = 0; i < inputArray.Length; i++)
            {          
                Folder remain_folder = null;
                for (int j = 0; j < myFolders.Count; j++)
                {
                    if (myFolders[j].remaincap >= (int)inputArray[i].Duration.TotalSeconds)
                    {
     
                        remain_folder = myFolders[j];
                        break;
                    }
                }

                if ((remain_folder != null))
                {
                    remain_folder.addFile(inputArray[i]);
                }
                else
                {
                    Folder folder = new Folder(maxcap);
                    folder.addFile(inputArray[i]);
                    myFolders.Add(folder);
                }

            }
            
            return myFolders;

        }
        
        public static List<Folder> NextFitLS(List<AudioFile> input, int maxcap)
        {
            List<Folder> myFolders = new List<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.Add(firstFolder);
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
                    myFolders.Add(temp);
                }
            }
            return myFolders;
        }

        public static List<Folder> NextFitDecreasingLS(List<AudioFile> input, int maxcap)
        {
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))
            return NextFitLS(inputArray.ToList<AudioFile>(), maxcap);
        }

        public static List<Folder> bestFitDecreasingLS(List<AudioFile> input, int maxcap)
        {
            AudioFile[] inputArray = input.ToArray();
            MinHeap.HeapSort(inputArray); //O(Nlog(N))
            return bestFitLS(inputArray.ToList<AudioFile>(), maxcap);
        }

        //attempting folder filling algorithm using DP with pseudo-polynomial algorithm \o/
        public static List<Folder> folderFilling(List<AudioFile> input, int maxcap) 
        {

            List<Folder> myFolders = new List<Folder>();

            for( int i = 0; i <= input.Count; i++) //O(N)
            {
                myFolders.Add(findBest(ref input, maxcap, maxcap, input.Count));
                //Removing Already Added Files ;) 
                //Folder lastFolderAdded = myFolders.Last();
                //foreach (AudioFile item in lastFolderAdded.files) //O(M)
                //{
                //    input.Remove(item);
                //}
            }
             

            return myFolders;
        }
        
        //0-1 knapsack B)
       // n is the array length
        private static Folder findBest(ref List<AudioFile> input, int maxcap,int remaincap, int n) 
        {
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
        }


    }
}
