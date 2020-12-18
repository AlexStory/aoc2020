# aoc2020

All in F# going to try and keep all solutions immutable, but going to try and get every solution under 1 second and will post runtimes, and wether i had to make them immutable.

All will be timed by (depending on wether i'm on my windows or linux machine at the time) `time dotnet [output]` or `Measure-Command { dotnet [output] }`. I'll run it three times and post the range.

day1: 204-211 ms

day2: 128-132 ms

day3: 138-143 ms

day4: 147-150 ms

day5: 147-154 ms

day6: 137-146 ms

day7: 212-225 ms

day8: 359-369 ms

day9: 168-190 ms

day10: 133-139 ms

day11: 424-439 ms

day12: 123-129 ms

day13: 158-159 ms

day14: 205-206 ms <-- used mutation (same code swapping dictionary with an immutable map took ~3 minutes)

day15: 231-244 ms <-- used mutation (version with same logic, but using a Map took ~86 seconds)

day16: 183-211 ms

day17: 438-445 ms

day 18: 205-220 ms
