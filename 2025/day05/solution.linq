<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	//var input = new[] {
	//	"3-5"
	//	,"10-14"
	//	,"16-20"
	//	,"12-18"
	//	,""
	//	,"1"
	//	,"5"
	//	,"8"
	//	,"11"
	//	,"17"
	//	,"32"};

	var blank = input
		.Select((value, index) => new { index, value })
		.Where(combo => string.IsNullOrEmpty(combo.value))
		.Select(i => i.index)
		.First();

	var ranges = input
		.Take(blank)
		.Select(i => i.Split("-", StringSplitOptions.RemoveEmptyEntries))
		.Select(i => new Range { Start = long.Parse(i[0]), End = long.Parse(i[1]) })
		.ToList();

	var nums = input
		.Skip(blank + 1)
		.Select(i => long.Parse(i))
		.ToList();

	//ranges.Dump();
	//nums.Dump();

	//PartOne(ranges, nums);
	ranges = ranges
		.OrderBy(r => r.Start)
		.ToList();
	var newMerged = MergeList(ranges);
	newMerged.Dump("newMerged");

	newMerged.Sum(m => m.Count()).Dump("grand total");
}

public static List<Range> MergeList(List<Range> range)
{
	if (range.Count <= 1)
	{
		return range;
	}
	if (range.Count == 2)
	{
		return MergeIfIntersect(range[0], range[1]);
	}
	//if (range.Count == 3)
	//{
	//	var a = MergeIfIntersect(range[0], range[1]);
	//	if (a.Count == 1)
	//	{
	//		return MergeIfIntersect(a[0], range[2]);
	//	}
	//	var b = MergeIfIntersect(range[2], range[3]);
	//	if (b.Count == 1)
	//	{
	//		return MergeIfIntersect(range[1], b[0]);
	//	}
	//	return range;
	//}
	var halfMark = range.Count / 2;
	var firsthalf = MergeList(range.Take(halfMark).ToList());
	var firsthalf_last = firsthalf.Last();

	var secondhalf = MergeList(range.Skip(halfMark).ToList());
	var secondhalf_first = secondhalf.First();

	var mid = MergeIfIntersect(firsthalf_last, secondhalf_first);
	return firsthalf.Take(firsthalf.Count - 1).Union(mid).Union(secondhalf.Skip(1)).ToList();
}

public static List<Range> MergeIfIntersect(Range a, Range b)
{
	// Normalize in case inputs are reversed
	if (a.Start > a.End) (a.Start, a.End) = (a.End, a.Start);
	if (b.Start > b.End) (b.Start, b.End) = (b.End, b.Start);

	// No overlap
	if (a.End < b.Start || b.End < a.Start)
		return new List<Range> { a, b };

	// Merge
	return new List<Range>
	{
		new Range
		{
			Start = Math.Min(a.Start, b.Start),
			End = Math.Max(a.End, b.End)
		}
	};
}

public void PartOne(List<Range> ranges, List<long> nums)
{
	var totals = 0;
	foreach (var num in nums)
	{
		if (ranges.Any(r => r.Start <= num && r.End >= num))
		{
			totals++;
			$"{num} is fresh".Dump();
		}
	}
	totals.Dump("total");
}
public class Range
{
	public long Start { get; set; }
	public long End { get; set; }
	public long Count()
	{
		return End - Start + 1;
	}
}
