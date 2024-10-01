import sys

sys.setrecursionlimit(10000)

class TestCase:
    def __init__(self, cap, num_objects, values, weights):
        self.cap = cap
        self.num_objects = num_objects
        self.values = values
        self.weights = weights

def parse_input():
    cases = []
    
    input_data = sys.stdin.read().splitlines()
    i = 0
    while i < len(input_data):
        line = input_data[i].split()
        cap = int(line[0])
        num_objects = int(line[1])
        values = []
        weights = []
        
        for j in range(num_objects):
            i += 1
            _line = input_data[i].split()
            value = int(_line[0])
            weight = int(_line[1])
            values.append(value)
            weights.append(weight)
        
        cases.append(TestCase(cap, num_objects, values, weights))
        i += 1

    return cases

def solve(cap, values, weights, m, i):
    if i == 0 or cap == 0:
        return 0  # Base case
    
    if m[i][cap] != -1:
        return m[i][cap]  # Memoized result

    if weights[i - 1] > cap:
        m[i][cap] = solve(cap, values, weights, m, i - 1)
        return m[i][cap]
    
    drop = solve(cap, values, weights, m, i - 1)
    take = values[i - 1] + solve(cap - weights[i - 1], values, weights, m, i - 1)

    m[i][cap] = max(take, drop)
    return m[i][cap]

def find_selected_items(cap, values, weights, m):
    i = len(values)
    selected_items = []

    
    while i > 0 and cap > 0:
        if m[i][cap] != m[i - 1][cap]:
            selected_items.append(i)  
            cap -= weights[i - 1]  
        i -= 1

    return selected_items

def solve_test_case(t):
    m = [[-1 for _ in range(t.cap + 1)] for _ in range(t.num_objects + 1)]

    solve(t.cap, t.values, t.weights, m, t.num_objects)
    
    indices = find_selected_items(t.cap, t.values, t.weights, m)
    indices.reverse()
    
    print(len(indices))
    print(" ".join(str(i - 1) for i in indices))

def run():
    cases = parse_input()
    for c in cases:
        solve_test_case(c)

if __name__ == '__main__':
    run()