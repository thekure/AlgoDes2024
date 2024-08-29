arr = [0, 1, 2, 3]

# 1. Get a sublist from index 1 to 2 (inclusive)
sublist1 = arr[1:3]  # [1, 2]
print(sublist1)

# 2. Get a sublist from the beginning to index 1 (exclusive)
sublist2 = arr[:2]  # [0, 1]
print(sublist2)

# 3. Get a sublist from index 2 to the end
sublist3 = arr[2:]  # [2, 3]
print(sublist3)

# 4. Get every second element
sublist4 = arr[::2]  # [0, 2]
print(sublist4)

# 5. Reverse the list
sublist5 = arr[::-1]  # [3, 2, 1, 0]
print(sublist5)

# 6. Get the last two elements
sublist6 = arr[-2:]  # [2, 3]
print(sublist6)

# 7. Get all elements except the last one
sublist7 = arr[:-1]  # [0, 1, 2]
print(sublist7)

# 8. Get a sublist from index 1 to 2 with a step of 2
sublist8 = arr[1:3:2]  # [1]
print(sublist8)
