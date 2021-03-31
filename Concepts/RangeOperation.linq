<Query Kind="Program" />

void Main()
{
	var stuff =new int[]{1,2,3,4,5,6,7};
	
	Console.WriteLine(String.Join(",", stuff));
	Console.WriteLine(String.Join(",", stuff[0..2])); // Starting 1 and length 2
	Console.WriteLine(String.Join(",", stuff[2..])); // Starting 3 end of array
	Console.WriteLine(String.Join(",", stuff[2..^1])); //Start at 3 end last but one

	var b =new ArraySegment<int>(stuff, 0,2); // return sub array  start 0 and length 2
	Console.WriteLine(b[0]); // Return 1
}

// Define other methods, classes and namespaces here
