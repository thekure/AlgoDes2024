from sympy import false, closest_points

debug = False

def read_input():
    import sys
    input = sys.stdin.read
    data = input().strip().split('\n')

    n = int(data[0])

    if debug: print(f"N: {n}")

    # Reading data for each point
    index = 1
    points = []
    for _ in range(n):
        if debug: print(f"Index: {index}")

        x, y = map(float, data[index].split())
        points.append((x, y))
        index += 1
    return points

def print_points(points):
    for point in points:
        x, y = point
        print(x, y)

def distance(p1, p2):
    return (p1[0] - p2[0]) ** 2 + (p1[1] - p2[1]) ** 2

def brute_force(points):
    min_dist = float('inf')
    closest_pair = (points[0], points[1])
    for i in range(len(points)):
        for j in range(i + 1, len(points)):
            dist = distance(points[i], points[j])
            if dist < min_dist:
                min_dist = dist
                closest_pair = (points[i], points[j])
    return closest_pair

def closest_pairs_in_middle_strip(points, delta):
    min_dist = delta
    closest_pair = (points[0], points[1])
    points.sort(key=lambda y: y[1])
    for i in range(len(points)):
        for j in range(i + 1, len(points)):
            if (points[j][1] - points[i][1]) >= min_dist:
                break
            dist = distance(points[i], points[j])
            if dist < min_dist:
                min_distance = dist
                closest_pair = (points[i], points[j])
    return closest_pair

def divide_and_conquer(points):
    if len(points) <= 128:
        return brute_force(points)

    middle_x = len(points) // 2
    (p1, q1) = divide_and_conquer(points[:middle_x])
    (p2, q2) = divide_and_conquer(points[middle_x:])

    delta = min(distance(p1, q1), distance(p2, q2))
    closest_pair = (p1, q1) if distance(p1, q1) < distance(p2, q2) else (p2, q2)

    middle_strip = [point for point in points if (abs(point[0] - middle_x) < delta)]

def run(points):
    points.sort(key=lambda x: x[0])
    return brute_force(points)


