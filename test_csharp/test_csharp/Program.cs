using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_csharp
{

    class Program
    {



        static void Main(string[] args)
        {
            int[] nums1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] nums2 = { 1, 3, 5, 7, 9 };
            var query = from a in nums1
                        from b in nums2
                        let sum = a + b
                        where sum == 12
                        select new { a, b, sum };
            foreach (var n in query)
            {
                Console.WriteLine(n);
            }
        }
    }
}
