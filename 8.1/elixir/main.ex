[raw_instructions, raw_nodes | _] =
  File.read!("../../8.input")
  |> String.trim()
  |> String.split("\n\n")

node_map =
  raw_nodes
  |> String.split("\n")
  |> Enum.reduce(%{}, fn line, acc ->
      [key, values] = line |> String.split(" = ")
      [left, right] = values |> String.split(", ") |> Enum.map(fn s -> Regex.replace(~r/[()]/, s, "") end)

      Map.put(acc, key, {left, right})
    end)

instructions =
  raw_instructions
  |> String.split("", trim: true)
  |> Stream.cycle()

defmodule Traverser do
  def traverse(node_map, instructions), do: traverse(node_map, instructions, "AAA", 0, 0)
  def traverse(_, _, "ZZZ", count, _), do: count
  def traverse(node_map, instructions, current_node, count, skip) do
    {left, right} = node_map[current_node]

    next_instruction =
      case instructions |> Stream.drop(skip) |> Enum.take(1) do
        ["L"] -> left
        ["R"] -> right
        i -> raise "Invalid instruction #{i}"
      end

    traverse(node_map, instructions, next_instruction, count + 1, skip + 1)
  end
end

Traverser.traverse(node_map, instructions) |> IO.inspect()
