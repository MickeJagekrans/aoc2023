#!/usr/bin/env python3

import re

with open('../../3.input', 'r') as f:
    lines = f.readlines()

included_numbers = []

for idx, line in enumerate(lines):
    matches = [(x.span(), x[0]) for x in re.finditer(r'\d+', line)]

    for (start, end), match in matches:
        # get lines to check (current line, previous line, next line) within bounds
        lines_to_check = lines[max(idx-1, 0):min(idx+2, len(lines))]
        
        # get adjacent squares for each match within bounds
        adjacent_substr = [line[max(start-1, 0):end+1] for line in lines_to_check]

        for substr in adjacent_substr:
            print(substr)
            if re.search(r'[^\d.\n]', substr):
                print('found non-digit', match)
                included_numbers.append(int(match))
                break

        print('')


print(sum(included_numbers))