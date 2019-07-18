// C# program to find lexicographic rank 
// of a string 
using System; 

namespace gfgclasses
{

public class GFG { 

	// A utility function to find factorial of n 
	static long fact(int n) 
	{ 
		return (n < 2) ? 1 : n * fact(n - 1); 
	} 

	// A function to find rank of a string in 
	// all permutations of characters 
	public long findRank(string str) 
	{ 
		int len = str.Length; 
    if (len == 0)
      return 0;
    char x0 = str[0];
    string xs = str.Substring(1, len - 1 );
    // Console.WriteLine(x0); 
    long count = 0;
    long xsLen = xs.Length;
    // for loop here
    for (int i = 0; i < xsLen; i++) {
      if (xs[i] < x0)
        count += 1;
    }
    // Console.WriteLine(fact(xs.Length)); 
    long answer = (findRank(xs) + (count * fact(xs.Length)));

    return answer;

  }


}
}
// This code is contributed nitin mittal. 
