#!/usr/bin/env python3

import re


with open('../input', 'r') as f:
    lines = f.read().splitlines()


pattern = r"(one|two|three|four|five|six|seven|eight|nine|\d)"
reverse_pattern = r"(eno|owt|eerht|ruof|evif|xis|neves|thgie|enin|\d)"

def convert_word_to_number(word):
    match word:
        case "one" | "eno" | "1":
            return 1
        case "two" | "owt" | "2":
            return 2
        case "three" | "eerht" | "3":
            return 3
        case "four" | "ruof" | "4":
            return 4
        case "five" | "evif" | "5":
            return 5
        case "six" | "xis" | "6":
            return 6
        case "seven" | "neves" | "7":
            return 7
        case "eight" | "thgie" | "8":
            return 8
        case "nine" | "enin" | "9":
            return 9
        case _:
            raise ValueError(f"Unknown word: {word}")


def get_calibration_value(line):
    first_match = re.search(pattern, line)
    last_match = re.search(reverse_pattern, line[::-1])
    num_first = convert_word_to_number(first_match[0])
    num_last = convert_word_to_number(last_match[0])

    return int(f'{num_first}{num_last}')


sum = sum([get_calibration_value(line) for line in lines])

print(sum)
