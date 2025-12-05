<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	//var input = new [] {
	//	"..@@.@@@@.",
	//	"@@@.@.@.@@",
	//	"@@@@@.@.@@",
	//	"@.@@@@..@.",
	//	"@@.@@@@.@@",
	//	".@@@@@@@.@",
	//	".@.@.@.@@@",
	//	"@.@@@.@@@@",
	//	".@@@@@@@@.",
	//	"@.@.@@@.@."};
	var rows = input.Select((row, y) => row.Select((item, x) => new Piece { Item = item, X = x, Y = y}).ToArray()).ToArray();

	int grandTotal = 0;
	int total;
	do
	{
		total = 0;
		for (int y = 0; y < input.Length; y++)
		{
			var row = input[y];
			for (int x = 0; x < row.Length; x++)
			{
				if (IsValid(x, y, rows))
				{				
					total++;
					rows[y][x].CanRemove = true;
				}
			}
		}
		total.Dump("total");
		Display(rows);
		RemoveRolls(rows);
		grandTotal += total;
	} while(total > 0);
	
	grandTotal.Dump("grandTotal");
}

void RemoveRolls(Piece[][] rows)
{
	foreach (var row in rows)
	{
		foreach (var p in row)
		{
			if (p.CanRemove)
			{
				p.Item = '.';
				p.CanRemove = false;
			}
		}
	}
}

public void Display(Piece[][] rows)
{
	var table = "<table><tbody>";
	for (int i = 0; i < rows.Length; i++)
	{
		table += "<tr>" + string.Join("", rows[i].Select(j => $"<td>{(j.CanRemove ? "x" : j.Item)}</td>")) + "</tr>";
	}
	table += "</tbody></table>";
	//Util.RawHtml(table).Dump();
}

public bool IsValid(int x, int y, Piece[][] rows)
{
	if (rows[y][x].Item != '@')
		return false;
	
	var count = 0;
	for (int yy = y - 1; yy <= y + 1; yy++)
	{	
		for (int xx = x - 1; xx <= x + 1; xx++)
		{
			if (!(xx == x && yy == y) && IsRoll(xx, yy, rows))
			{				
				count++;
			}
		}
	}
	//$"{x} {y} {count}".Dump();
	return count < 4;
}

public bool IsRoll(int x, int y, Piece[][] rows)
{
	if (y < 0 || y > rows.Length - 1 || x < 0 || x > rows[y].Length - 1)
	{
		return false;
	}
	return rows[y][x].Item == '@';
}

public class Piece
{
	public int X { get; set; }
	public int Y { get; set; }
	public char Item {get; set;}
	public bool CanRemove {get; set;}
}