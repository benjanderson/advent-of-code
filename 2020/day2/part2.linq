<Query Kind="Statements" />

var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath),  @"input.txt"));
var regex = new Regex(@"(?<min>\d+)-(?<max>\d+)\s(?<letter>[a-z]):\s(?<password>[a-z]+)", RegexOptions.IgnoreCase);
var parsed = input
.Select(i => regex.Match(i))
.Select(i => new 
{
	Min = Convert.ToInt32(i.Groups["min"].Value),
	Max = Convert.ToInt32(i.Groups["max"].Value),
	Letter = i.Groups["letter"].Value[0],
	Password = i.Groups["password"].Value,
})
.Select(i =>
{
	return new
	{
		Expression = i,
		PassesPasswordPolicy = i.Password[i.Min - 1].Equals(i.Letter) ^ i.Password[i.Max - 1].Equals(i.Letter)
	};
})
.Dump();

parsed.Count(p => p.PassesPasswordPolicy).Dump("solution");
