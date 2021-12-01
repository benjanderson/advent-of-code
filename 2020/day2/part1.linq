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
	var actual = i.Password.Count(c => c.Equals(i.Letter));
	return new
	{
		Expression = i,
		PassesPasswordPolicy = actual >= i.Min && actual <= i.Max
	};
})
.Dump();

parsed.Count(p => p.PassesPasswordPolicy).Dump("solution");
