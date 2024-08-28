test1 = "a<bc<"
test2 = "foss<<rritun"
test3 = "a<a<a<aa<<"

def fix_backspace(stream):
    for c in stream:
        if c == '<':

    return stream


print("Result: " + fix_backspace(test1))
print("Should be 'b'")

print("Result: " + fix_backspace(test2))
print("Should be 'forritun'")

print("Result: " + fix_backspace(test3))