lines = File.read!("../../5.input")

# lines =
# "seeds: 79 14 55 13

# seed-to-soil map:
# 50 98 2
# 52 50 48

# soil-to-fertilizer map:
# 0 15 37
# 37 52 2
# 39 0 15

# fertilizer-to-water map:
# 49 53 8
# 0 11 42
# 42 0 7
# 57 7 4

# water-to-light map:
# 88 18 7
# 18 25 70

# light-to-temperature map:
# 45 77 23
# 81 45 19
# 68 64 13

# temperature-to-humidity map:
# 0 69 1
# 1 0 69

# humidity-to-location map:
# 60 56 37
# 56 93 4"

[seeds | maps] = lines |> String.split("\n\n")

defmodule Almanac do
  def parse_map(line) do
    line
    |> String.split(" ")
    |> Enum.map(&String.to_integer/1)
    |> parse_map_values()
  end

  defp parse_map_values([dest, source, range]) do
    {source, source + range - 1, dest - source}
  end

  def walk(value, []), do: value
  def walk(value, [map | maps]), do: walk(value + find_offset(map, value), maps)

  defp find_offset(map, value) do
    found_value =
      map |> Enum.find(fn {start, stop, _} -> value in start..stop end)

    case found_value do
      {_, _, offset} -> offset
      _ -> 0
    end
  end
end

maps =
  maps
  |> Enum.map(fn map ->
    map
    |> String.split("\n")
    |> tl()
    |> Enum.map(&Almanac.parse_map/1)
  end)

seeds
|> String.split(": ")
|> Enum.at(1)
|> String.split(" ")
|> Enum.map(&String.to_integer/1)
|> Enum.map(&Almanac.walk(&1, maps))
|> Enum.min()
|> IO.inspect()
