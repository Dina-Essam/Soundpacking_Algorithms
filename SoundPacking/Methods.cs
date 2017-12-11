using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class Methods
    {

        public static List<Folder> worstFitLS(List<AudioFile> input, int maxcap)
        {
            List<Folder> myFolders = new List<Folder>();
            Folder firstFolder = new Folder(maxcap);
            myFolders.Add(firstFolder);
            for (int i = 0; i < input.Count; i++)
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
            for (int i = 0; i < input.Count; i++)
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



    }
}
