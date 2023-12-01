#!/usr/bin/env ruby

lines = File.readlines('../input')

PATTERN = /(one|two|three|four|five|six|seven|eight|nine|\d)/
REVERSE_PATTERN = /(eno|owt|eerht|ruof|evif|xis|neves|thgie|enin|\d)/

def get_number_from_word(word)
  case word
  in "one" | "eno" | "1"
    1
  in "two" | "owt" | "2"
    2
  in "three" | "eerht" | "3"
    3
  in "four" | "ruof" | "4"
    4
  in "five" | "evif" | "5"
    5
  in "six" | "xis" | "6"
    6
  in "seven" | "neves" | "7"
    7
  in "eight" | "thgie" | "8"
    8
  in "nine" | "enin" | "9"
    9
  else
    raise "Unknown word: #{word}"
  end
end

def get_calibration_value(line)
  first_number = get_number_from_word(line[PATTERN])
  last_number = get_number_from_word(line.reverse[REVERSE_PATTERN])

  "#{first_number}#{last_number}".to_i
end

puts lines.map { |line| get_calibration_value(line) }.reduce(:+)
