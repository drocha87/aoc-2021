using System;
using System.IO;

var inputs = File.ReadAllLines("input.txt").ToList().ConvertAll(x => Int32.Parse(x));
var previous = inputs[0] + inputs[1] + inputs[2];
var counter = 0;

for (var index = 1; index < (inputs.Count() - 2); index++)
{
    var sum = inputs[index] + inputs[index + 1] + inputs[index + 2];
    if (sum > previous)
    {
        counter += 1;
    }
    previous = sum;
}

Console.WriteLine(counter);
