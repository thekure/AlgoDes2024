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
    closest_pair = None
    for i in range(len(points)):
        for j in range(i + 1, len(points)):
            dist = distance(points[i], points[j])
            if dist < min_dist:
                # print(f"Updating min_dist: {dist}")
                min_dist = dist
                closest_pair = (points[i], points[j])
    return closest_pair

def closest_pairs_in_middle_strip(points, delta):
    # print(f"Entered function. Points length: {len(points)}")
    min_dist = delta
    # print(f"Minimum distance: {min_dist}")
    closest_pair = points[0], points[1]
    points.sort(key=lambda y: y[1])
    # print(f"Points are sorted. List length: {len(points)}")
    for i in range(len(points)):
        for j in range(i + 1, len(points)):
            if (points[j][1] - points[i][1]) >= min_dist:
                # print(f"Breaking")
                break
            # print(f"Updating dist")
            dist = distance(points[i], points[j])
            if dist < min_dist:
                # print(f"Strip: Dist condition met.")
                min_dist = dist
                closest_pair = (points[i], points[j])
    return closest_pair

def divide_and_conquer(points):
    if len(points) <= 128:
        # print()
        # print(f"Length of points: {len(points)}. Brute Forcing")
        return brute_force(points)

    half_length = len(points) // 2
    middle_x = points[half_length][0]

    (p1, q1) = divide_and_conquer(points[:half_length])
    (p2, q2) = divide_and_conquer(points[half_length:])

    delta = min(distance(p1, q1), distance(p2, q2))
    # print(f"Delta: {delta}")
    closest_pair = (p1, q1) if distance(p1, q1) < distance(p2, q2) else (p2, q2)
    # print(f"Current Closest Pair: {closest_pair}")

    # for point in points:
    #     print(f"X: {point[0]} - middle_x: {middle_x} < Delta: {delta}. Result w. abs: {abs(point[0] - middle_x)}")
    middle_strip = [point for point in points if (abs(point[0] - middle_x) < delta)]
    # print(f"Middle_strip length: {len(middle_strip)}")
    if len(middle_strip) <= 1:
        return closest_pair
    (strip_p, strip_q) = closest_pairs_in_middle_strip(middle_strip, delta)
    if distance(strip_p, strip_q) < delta:
        return strip_p, strip_q
    return closest_pair

def run():
    points = read_input()
    points.sort(key=lambda x: x[0])
    result = divide_and_conquer(points)
    print(f"{result[0][0]} {result[0][1]}")
    print(f"{result[1][0]} {result[1][1]}")


