using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01Stream
{
    public class DataProcessingService
    {
        public List<int> ConvertToInts (string[] depthsString)
        {
            List<int> list = new List<int>();

            foreach(string depth in depthsString)
            {
                var d = int.Parse(depth);
                list.Add(d);
            }

            return list;
        }

        public int Part1Processing(List<int> depths)
        {
            int depthIncreaseCount = 0;

            for (int i = 1; i < depths.Count; i++)
            {
                if(depths[i] > depths[i - 1])
                {
                    depthIncreaseCount++;
                }

            }

            return depthIncreaseCount;
        }

        public int Part2Processing(List<int> depths)
        {
            int depthIncreaseCount = 0;

            for (int i = 3; i < depths.Count; i++)
            {
                if ((depths[i] + depths[i - 1] + depths[i - 2]) > (depths[i - 1] + depths[i - 2] + depths[i - 3]))
                {
                    depthIncreaseCount++;
                }

            }

            return depthIncreaseCount;
        }
    }
}
