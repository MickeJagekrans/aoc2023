#!/usr/bin/env ruby

lines = File.readlines('../input')

def get_calibration_value(line)
  "#{line[/\d/]}#{line.reverse[/\d/]}".to_i
end

sum = lines.map { |line| get_calibration_value(line) }.reduce(:+)

puts sum