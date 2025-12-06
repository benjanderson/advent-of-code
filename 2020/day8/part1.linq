<Query Kind="Program" />

void Main()
{
	var regex = new Regex(@"(?<type>(nop|acc|jmp))\s(?<value>(\+|\-)\d+)");
	var input = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), @"input.txt"));
	var instructions = input
	.Select(i => regex.Match(i))
	.Select(i => new Instruction
	{
		Type = (OpType) Enum.Parse(typeof(OpType), i.Groups["type"].Value, true),
		Value = Convert.ToInt32(i.Groups["value"].Value)
	}).ToList();
	
	var hash = new HashSet<int>();
	int index = 0;
	int value = 0;
	while(!hash.Contains(index))
	{
		hash.Add(index);
		$"{instructions[index].Type}\t\t{instructions[index].Value}\t\t|\t\t{value}".Dump();
		switch (instructions[index].Type)
		{
			case OpType.Acc:
				value += instructions[index].Value;
				index++;
				break;
			case OpType.Jmp:
				index += instructions[index].Value;
				break;
			case OpType.Nop:
				index++;
				break;
		}
	}

	value.Dump("solution");
}

public class Instruction
{
	public OpType Type {get; set;}
	
	public int Value {get; set;}
}

public enum OpType {Nop, Acc, Jmp};


