<Query Kind="Statements" />

var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath),  @"input.txt"));
var numbers = input.Select(i => Convert.ToInt32(i)).ToList();
int first = -1, second = -1, third = -1;
for (int i = 0; i < numbers.Count; i++)
{
	for (int j = 0; j < numbers.Count; j++)
	{
		if (i==j)
		{
			continue;
		}
		for (int k = 0; k < numbers.Count; k++)
		{
			if (i == k || i == j)
			{
				continue;
			}

			if (numbers[i] + numbers[j] + numbers[k] == 2020)
			{
				first = numbers[i];
				second = numbers[j];
				third = numbers[k];
				break;
			}
		}
	}
}

first.Dump("first");
second.Dump("second");
third.Dump("third");
(first * second * third).Dump("solution");