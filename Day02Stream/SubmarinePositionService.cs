using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02Stream
{
    public class SubmarinePositionService
    {
        public List<(string, int)> ProcessData (string[] data)
        {
            var list = new List<(string,int)> ();

            foreach (var item in data)
            {
                var d = item.Split(" ");
                var n = int.Parse(d[1]);

                list.Add((d[0], n));
            }

            return list;
        }

        public (int, int) MoveTheSub(List<(string command, int value)> instructions)
        {
            int subDistance = 0;
            int subDepth = 0;

            foreach(var item in instructions)
            {
                if(string.Equals(item.command, "forward", StringComparison.OrdinalIgnoreCase))
                {
                    subDistance += item.value;
                }
                else if(string.Equals(item.command, "down", StringComparison.OrdinalIgnoreCase))
                {
                    subDepth += item.value;
                }
                else if(string.Equals (item.command, "up", StringComparison.OrdinalIgnoreCase))
                {
                    subDepth -= item.value;
                }
            }

            return (subDistance, subDepth);
        }

        public (int, int, int) TryAgain(List<(string command, int value)> instructions)
        {
            int subDistance = 0;
            int subDepth = 0;
            int subAim = 0;

            foreach (var item in instructions)
            {
                if (string.Equals(item.command, "forward", StringComparison.OrdinalIgnoreCase))
                {
                    subDistance += item.value;
                    subDepth += item.value * subAim;
                }
                else if (string.Equals(item.command, "down", StringComparison.OrdinalIgnoreCase))
                {
                    subAim += item.value;
                }
                else if (string.Equals(item.command, "up", StringComparison.OrdinalIgnoreCase))
                {
                    subAim -= item.value;
                }
            }

            return (subDistance, subDepth, subAim);
        }
    }
}
