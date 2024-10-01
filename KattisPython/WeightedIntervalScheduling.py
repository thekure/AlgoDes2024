# Inspired by https://www.geeksforgeeks.org/weighted-job-scheduling-log-n-time/

import sys

n = 0

class Interval:
    def __init__(self, start, finish, weight):
        self.start  = start
        self.finish = finish
        self.weight  = weight

def read_intervals():
    global n
    n = int(sys.stdin.readline().strip())
    lst = []

    for _ in range(n):
        s, f, w = map(int, sys.stdin.readline().strip().split())
        interval = Interval(s, f, w)
        lst.append(interval)
    return lst

def solve(input):
    sorted_intervals = sorted(input, key=lambda i: i.finish)
    m = [0] * n
    m[0] = sorted_intervals[0].weight

    for i in range(1, n):
        cur_weight = sorted_intervals[i].weight
        prev_non_overlapping_interval = search(i, sorted_intervals)

        # Search returns -1 if there are no more non-overlapping results
        if prev_non_overlapping_interval != -1:
            cur_weight += m[prev_non_overlapping_interval]

        m[i] = max(cur_weight, m[i - 1])

    return m[n - 1]

def search(index, intervals):

    low, high = 0, index - 1

    while low <= high:
        # print(f"\n")
        middle = (low + high) // 2
        # print(f"Low: {low}, High: {high}, Middle: {middle}")
        # print(f"(intervals[middle].finish) {intervals[middle].finish} <= {intervals[index].start} (intervals[index].start)")
        if intervals[middle].finish <= intervals[index].start:
            # print(f"(intervals[middle + 1].finish) {intervals[middle + 1].finish} <= {intervals[index].start} (intervals[index].start)")
            if intervals[middle + 1].finish <= intervals[index].start:
                # print(f"Low = {middle} + 1")
                low = middle + 1
            else:
                # print(f"Returning middle: {middle}")
                return middle
        else:
            # print(f"high = {middle} - 1")
            high = middle - 1

    return -1

if __name__ == "__main__":
    intervals = read_intervals()
    result = solve(intervals)
    print(result)


