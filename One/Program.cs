using System;
using System.IO;

// See https://aka.ms/new-console-template for more information
var inputs = File.ReadAllLines("input.txt").ToList().ConvertAll(x => Int32.Parse(x));
var previous = inputs[0];
inputs.RemoveAt(0);
var counter = 0;

foreach (var input in inputs)
{
    if (input > previous)
    {
        counter += 1;
    }
    previous = input;
}

Console.WriteLine(counter);
