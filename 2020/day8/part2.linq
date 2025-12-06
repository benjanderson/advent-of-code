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

	for (int i = 0; i < instructions.Count; i++)
	{
		List<Instruction> altInstructions = instructions.Select(a => new Instruction { Type = a.Type, Value = a.Value}).ToList();
		if (instructions[i].Type == OpType.Acc)
		{
			continue;
		}
		if (instructions[i].Type == OpType.Jmp)
		{
			altInstructions[i].Type = OpType.Nop;
		}
		else if (instructions[i].Type == OpType.Nop)
		{
			altInstructions[i].Type = OpType.Jmp;
		}
		
		var hash = new HashSet<int>();
		int index = 0;
		int value = 0;

		while (index < altInstructions.Count && !hash.Contains(index))
		{
			hash.Add(index);
			$"{altInstructions[index].Type}\t\t{altInstructions[index].Value}\t\t|\t\t{value}".Dump();
			switch (altInstructions[index].Type)
			{
				case OpType.Acc:
					value += altInstructions[index].Value;
					index++;
					break;
				case OpType.Jmp:
					index += altInstructions[index].Value;
					break;
				case OpType.Nop:
					index++;
					break;
			}
		}
		
		if (index == instructions.Count)
		{
			value.Dump("solution");
			break;
		}
	}
}

public class Instruction
{
	public OpType Type {get; set;}
	
	public int Value {get; set;}
}

public enum OpType {Nop, Acc, Jmp};


