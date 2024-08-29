

names = []
proposers = []
rejectors = []
stacks = {}
maps = {}

def read_input():
    import sys
    input = sys.stdin.read
    data = input().strip().split('\n')

    n, m = map(int, data[0].split())

    # Reading data for each participant
    index = 1
    for _ in range(n):
        line = data[index].strip().split()
        index += 1

        name = line[0]
        preferences = line[1:]

        names.append(name)

        # Store preferences as a stack (reversed list)
        stacks[name] = preferences[::-1]

        # Concise way of creating a map from a list: pref becomes key, i+1 value.
        ranks = {pref: i + 1 for i, pref in enumerate(preferences)}
        maps[name] = ranks

def print_data():
    print("Names:")
    for name in names:
        print(name)

    print("\nStacks (Preferences):")
    for name, prefs in stacks.items():
        print(f"{name}: {prefs}")

    print("\nMaps (Rankings):")
    for name, ranks in maps.items():
        print(f"{name}: {ranks}")

    print("\nProposers:")
    for name in proposers:
        print(f"{name}")

    print("\nRejectors:")
    for name in rejectors:
        print(f"{name}")

def split_names():
    global proposers
    global rejectors

    proposers = names[:len(names) // 2]
    rejectors = names[len(names) // 2:]

def tinder():
    global proposers
    global rejectors
    global stacks
    global maps
    matches = {}

    remaining_proposers = set(proposers)
    while remaining_proposers:
        proposer = remaining_proposers.pop()
        #print()
        #print(f"Current proposer: {proposer}.")
        proposers_prefs = stacks[proposer]

        while proposers_prefs:
            rejector = proposers_prefs.pop()
            #print(f"Current rejector: {rejector}.")
            if rejector not in matches:
                #print(f"Matched {rejector} with {proposer}.")
                #print()
                matches[rejector] = proposer
                matches[proposer] = rejector
                break
            else:
                rejectors_prefs = maps[rejector]
                existing_match = matches[rejector]
                current_match_rank = rejectors_prefs[existing_match]
                proposer_match_rank = rejectors_prefs[proposer]
                #print(f"{rejector} already matched with {existing_match} with rank {current_match_rank}.")
                #print(f"{proposer} has a rank of {proposer_match_rank}.")
                #print()

                if current_match_rank > proposer_match_rank and proposer != rejector:
                    #print(f"{rejector} is now matched with {proposer} instead.")
                    #print()
                    matches[rejector] = proposer
                    matches[proposer] = rejector
                    remaining_proposers.add(existing_match)
                    break
    return matches

def are_disjoint(a, b):
    a_length = len(a)
    a_as_set = set(a)
    b_length = len(b)
    b_as_set = set(b)
    if a_length != len(a_as_set) or b_length != len(b_as_set):
        return False
    return a_as_set.isdisjoint(b_as_set)

def contains_none(a, b):
    return None in a or None in b

def main():
    read_input()
    split_names()
    # print_data()
    matches = tinder()
    final_proposers = []
    final_rejectors = []
    final_matches = list(map(lambda key: (key, matches.get(key)), proposers))
    for (proposer, rejector) in final_matches:
        final_proposers.append(proposer)
        final_rejectors.append(rejector)

    if are_disjoint(final_proposers, final_rejectors) and not contains_none(final_proposers, final_rejectors):
        for (proposer, rejector) in final_matches:
            print(f"{proposer} {rejector}")
    else: print("-")


if __name__ == "__main__":
    main()
