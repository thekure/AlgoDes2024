import random

def generate_test_case():
    # Capacity between 1 and 2000
    # C = random.randint(1, 2000)
    C = 2000
    
    # Number of objects between 1 and 2000
    # n = random.randint(1, 2000)

    n = 2000
    
    test_case = f"{C} {n}\n"
    
    for _ in range(n):
        # Random value and weight between 1 and 10000
        value = random.randint(1, 10000)
        weight = random.randint(1, 10000)
        test_case += f"{value} {weight}\n"
    
    return test_case

def generate_test_cases(num_cases):
    test_cases = ""
    
    for _ in range(num_cases):
        test_cases += generate_test_case()
    
    return test_cases

if __name__ == "__main__":
    # Generate a random number of test cases between 1 and 30
    # num_cases = random.randint(1, 30)
    num_cases = 2
    
    # Generate all test cases
    test_cases = generate_test_cases(num_cases)
    
    # Save the test cases to a file called "cases.txt"
    with open("cases.txt", "w") as f:
        f.write(test_cases)
    
    print(f"Generated {num_cases} test cases and saved them to cases.txt")
