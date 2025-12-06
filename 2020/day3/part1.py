f = open('input.txt', 'r')
lines = f.readlines()
f.close()

index = 0
numTrees = 0
for line in lines:
    target = line[index]
    if target == '#':
        numTrees = numTrees + 1
        line = line[:index] + 'X' + line[index + 1:]
    else:
        line = line[:index] + 'O' + line[index + 1:]
    print(line, end =" ")        
    index = index + 3
    index = index % (len(line) - 1)

print()
print('Solution: {value}'.format(value=numTrees))