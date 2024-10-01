import random

def generate_random_points(num_points: int, max_value: float, precision: int) -> list:
    points = []
    for _ in range(num_points):
        x = round(random.uniform(min_value, max_value), precision)
        y = round(random.uniform(min_value, max_value), precision)
        points.append(f"{x:.{precision}f} {y:.{precision}f}")
    return points

def save_points_to_file(filename: str, points: list):
    with open(filename, 'w') as file:
        # Write the number of points on the first line
        file.write(f"{len(points)}\n")
        # Write each point on a new line
        for point in points:
            file.write(f"{point}\n")

# Generate 100,000 points with values between 0 and 10, with up to 2 decimal places
num_points = 100000
max_value = 100000.0
min_value = -100000.0
precision = 2

points = generate_random_points(num_points, max_value, precision)
save_points_to_file("points.txt", points)
