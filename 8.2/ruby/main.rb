raw_instructions, raw_nodes = File.read("../../8.input").split("\n\n")

nodes = raw_nodes.split("\n").reduce({}) do |acc, node|
  key, values = node.split(" = ")
  left, right = values[1...-1].split(", ")
  acc[key] = [left, right]

  acc
end


def find_cycle_point(instructions, nodes, start_node)
  current_node = start_node
  count = 1
  i = instructions.cycle
  possible_stop_points = []

  while true do
    instruction = i.next

    left, right = nodes[current_node]

    case instruction 
    when "L"
      current_node = left
    when "R"
      current_node = right
    else
      raise "Unknown instruction: #{instruction}"
    end

    if current_node.end_with?("Z")
      # Checked manually that the instructions repeat after the first found Z-node
      # If this was not the case, the calculations would be a bit worse (but still possible)
      return count #, current_node if count % instructions.count == 0
    end

    count += 1
  end
end

instructions = raw_instructions.split("")
start_nodes = nodes.keys.filter { |key| key.end_with?("A") }
stop_points = start_nodes.map do |start_node|
  find_cycle_point(instructions, nodes, start_node)
end

# Calculate the least common multiple of all the stop points
puts stop_points.reduce(1, :lcm)