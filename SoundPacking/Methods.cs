using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundPacking
{
    class Methods
    {

        public static List<Folder> worstFitDecreasingLS(List<AudioFile> input, int maxcap)
        {
            input.Sort((x, y) => -1 * x.Duration.CompareTo(y.Duration));
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
    }
}
