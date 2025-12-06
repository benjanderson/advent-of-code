f = open('input.txt', 'r')
lines = f.readlines()
f.close()

index = 0

rights = [1, 3, 5, 7, 1]
downs = [1, 1, 1, 1, 2]
row = 0
total = 0

for x in range(5):
    numTrees = 0
    for line in lines:
        if downs[x] == 2 and row % 2 == 1:
            continue
        target = line[index]
        if target == '#':
            numTrees = numTrees + 1
            line = line[:index] + 'X' + line[index + 1:]
        else:
            line = line[:index] + 'O' + line[index + 1:]

        print(line, end =" ")        
        index = index + rights[x]
        index = index % (len(line) - 1)
        row = row + 1
    print()
    print('Solution{x}: {value}'.format(value=numTrees, x=x))
    if total == 0:
        total = numTrees
    else:
        total * numTrees
print('Solution{x}: {value}'.format(value=numTrees, x=x))