#!/usr/bin/env python3

with open('../../1.input', 'r') as f:
    lines = f.read().splitlines()


def get_calibration_value(line):
    for char in line:
        if char.isdigit():
            first_digit = char
            break

    for char in reversed(line):
        if char.isdigit():
            last_digit = char
            break

    return int(first_digit + last_digit)


sum = sum([get_calibration_value(line) for line in lines])

print(sum)
