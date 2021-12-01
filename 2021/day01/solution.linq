<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	int increased = 0, increased2 = 0;
	for (int i = 0; i < input.Length; i++)
	{
		var x4 = Convert.ToInt32(input[i]);

		int? x3 = (int?)null;
		if (i > 0)
		{
			x3 = Convert.ToInt32(input[i - 1]);
		}

		int? x2 = (int?)null;
		if (i > 1)
		{
			x2 = Convert.ToInt32(input[i - 2]);
		}

		int? x1 = (int?)null;
		if (i > 2)
		{
			x1 = Convert.ToInt32(input[i - 3]);
		}

		if (x3.HasValue && x4 > x3)
		{
			increased++;
		}
		
		if (x1.HasValue && x2.HasValue && x3.HasValue && x4 + x3 + x2 > x3 + x2 + x1)
		{
			increased2++;
		}
	}

	increased.Dump("First Solution");
	increased2.Dump("Second Solution");
}
