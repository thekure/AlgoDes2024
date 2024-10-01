import sys

n = int(input())
weights = []
total = 1000
sys.setrecursionlimit(10000)

for i in range(n):
    weight = int(input())
    weights.append(weight)

memory = [[-1] * (total * 2 + 1) for _ in range(n)]


def find_optimal_weight(i, total_weight):
    if total_weight > total * 2:
        return float("inf")
    current_weight = weights[i]

    mem = memory[i][total_weight]
    if mem != -1:
        return mem

    if i == 0:
        if abs(total - (current_weight + total_weight)) <= abs(total - (total_weight)):
            result = current_weight + total_weight
        else:
            result = total_weight

        memory[i][total_weight] = result
        return result

    without_i = find_optimal_weight(i - 1, total_weight)
    with_i = find_optimal_weight(i - 1, total_weight + current_weight)

    if abs(total - with_i) < abs(total - without_i):
        result = with_i
    elif abs(total - with_i) > abs(total - without_i):
        result = without_i
    elif with_i > without_i:
        result = with_i
    else:
        result = without_i

    memory[i][total_weight] = result
    return result


print(find_optimal_weight(n - 1, 0))