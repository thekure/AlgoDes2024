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
                min_dist = dist
                closest_pair = (points[i], points[j])
    return closest_pair

def closest_pairs_in_middle_strip(points, delta):
    min_dist = delta
    closest_pair = None
    points.sort(key=lambda y: y[1])

    for i in range(len(points)):
        for j in range(i + 1, len(points)):
            if (points[j][1] - points[i][1]) >= min_dist:
                break

            dist = distance(points[i], points[j])

            if dist < min_dist:
                min_dist = dist
                closest_pair = (points[i], points[j])
    return closest_pair

def divide_and_conquer(points):
    if len(points) <= 128:
        return brute_force(points)

    half_length = len(points) // 2
    middle_x = points[half_length][0]

    result_left = divide_and_conquer(points[:half_length])
    result_right = divide_and_conquer(points[half_length:])

    # Handle potential None cases
    if result_left:
        p1, q1 = result_left
        delta_left = distance(p1, q1)
    else:
        delta_left = float('inf')

    if result_right:
        p2, q2 = result_right
        delta_right = distance(p2, q2)
    else:
        delta_right = float('inf')

    # Set correct delta and closest pair
    if delta_left < delta_right:
        closest_pair = result_left
        delta = delta_left
    else:
        closest_pair = result_right
        delta = delta_right

    # Create list with all points in the middle strip of the set based on delta value
    middle_strip = [point for point in points if (abs(point[0] - middle_x) < delta)]

    # If the list contains none or 1 value, disregard
    if len(middle_strip) <= 1:
        return closest_pair

    # Else, find the closest pair in the strip
    strip_pair = closest_pairs_in_middle_strip(middle_strip, delta)

    # Check for None values
    if strip_pair:
        strip_p, strip_q = strip_pair
        # If the strip pair is closer than the previously found one, return it
        if distance(strip_p, strip_q) < delta:
            return strip_p, strip_q
    # Else return the original result
    return closest_pair

def main():
    points = read_input()
    points.sort(key=lambda x: x[0])
    result = divide_and_conquer(points)
    print(f"{result[0][0]} {result[0][1]}")
    print(f"{result[1][0]} {result[1][1]}")

if __name__ == "__main__":
    main()
