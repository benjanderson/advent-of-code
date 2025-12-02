<Query Kind="Program" />

void Main()
{
	var numRegex = new Regex(@"\d+");
	var input = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	var ranges = input.Split(",", StringSplitOptions.RemoveEmptyEntries)
		.Select(i => new {Start = long.Parse(i.Split("-", StringSplitOptions.RemoveEmptyEntries)[0]), End = long.Parse(i.Split("-", StringSplitOptions.RemoveEmptyEntries)[1])})
		.ToList();
	long total = 0;
		
	foreach (var range in ranges)
	{
		for(var i = range.Start; i <= range.End; i++)
		{
			//if (!IsInvalid1(i))
			//{
			//	total += i;
			//}
			if (!IsInvalid2(i))
			{
				total += i;
			}
		}
	}
	total.Dump("total");
}

bool IsInvalid1(long number)
{
	bool isValid = false;
	var strNum = number.ToString();
	var length = strNum.Length;
	if (length % 2 == 1)
	{
		isValid = true;
	}
	else
	{
		for(var i = 0; i < length / 2; i++)
		{
			if (strNum[i] != strNum[i + (length / 2)])
			{
				return true;
			}
		}
	}

	if (!isValid)
	{
		$"{number} is {(isValid ? "valid" : "invalid")}".Dump();
	}
	return isValid;
}

bool IsInvalid2(long number)
{
	var strNum = number.ToString();
	var length = strNum.Length;
	if (length >= 10 && PartsMatch(strNum, 5))
	{
		$"{number} is invalid".Dump();
		return false;
	}
	if (length >= 8 && PartsMatch(strNum, 4))
	{
		$"{number} is invalid".Dump();
		return false;
	}
	if (length >= 6 && PartsMatch(strNum, 3))
	{
		$"{number} is invalid".Dump();
		return false;
	}
	if (length >= 4 && PartsMatch(strNum, 2))
	{
		$"{number} is invalid".Dump();
		return false;
	}
	if (length >= 2 && PartsMatch(strNum, 1))
	{
		$"{number} is invalid".Dump();
		return false;
	}
	return true;
}

bool PartsMatch(string strNum, int partLength)
{
	if (strNum.Length % partLength != 0)
	{
		return false;
	}
	var first = strNum.Substring(0, partLength);
	
	for (int i = partLength; i < strNum.Length; i+= partLength)
	{
		var other = strNum.Substring(i, partLength);
		if (!string.Equals(first, other))
		{
			return false;
		}
	}
	return true;
}

