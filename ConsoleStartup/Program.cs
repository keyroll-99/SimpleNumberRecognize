// See https://aka.ms/new-console-template for more information


using NumberRecognition;

// var tmp = new Perceptron('0');


var perceptrons = new List<Perceptron>();

var tasks = new List<Task>();

// 48 - 0 in ASCII 
// 57 - 9 in ASCII
for (var i = 48; i <= 57; i++)
{
    var perceptron = new Perceptron((char)i, 5, 5);
    tasks.Add(Task.Run(() => perceptron.Train()));
    perceptrons.Add(perceptron);
}

await Task.WhenAll(tasks);

var examples = new List<List<int>>()
{
    new ()
    {
        0, 0, 1, 1, 1,
        0, 0, 1, 0, 1,
        0, 0, 1, 0, 1,
        0, 0, 1, 0, 1,
        0, 0, 1, 1, 1,
    },
    new()
    {
        0, 0, 0, 0, 1,
        0, 0, 0, 1, 1,
        0, 0, 1, 0, 1,
        0, 0, 0, 0, 1,
        0, 0, 0, 0, 1,
    },
    new()
    {
        0, 0, 1, 1, 1,
        0, 0, 0, 0, 1,
        0, 0, 1, 1, 1,
        0, 0, 1, 0, 0,
        0, 0, 1, 1, 1,
    },
    new()
    {
        0, 0, 1, 1, 1,
        0, 0, 0, 0, 1,
        0, 0, 1, 1, 1,
        0, 0, 0, 0, 1,
        0, 0, 1, 1, 1,
    },
    new()
    {
        0, 0, 0, 0, 1,
        0, 0, 0, 1, 1,
        0, 0, 1, 0, 1,
        0, 1, 1, 1, 1,
        0, 0, 0, 0, 1,
    },
    new()
    {
        0, 0, 1, 1, 1,
        0, 0, 1, 0, 0,
        0, 0, 1, 1, 1,
        0, 0, 0, 0, 1,
        0, 0, 1, 1, 1,
    },
    new()
    {
        0, 0, 1, 1, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 1, 1,
        0, 0, 1, 0, 1,
        0, 0, 1, 1, 1,
    },
    new()
    {
        0, 1, 1, 1, 0,
        0, 0, 0, 1, 0,
        0, 0, 0, 1, 0,
        0, 0, 0, 1, 0,
        0, 0, 0, 0, 0,
    },
    new()
    {
        0, 1, 1, 1, 0,
        0, 1, 0, 1, 0,
        0, 1, 1, 1, 0,
        0, 1, 0, 1, 0,
        0, 1, 1, 1, 0,
    },
    new()
    {
        0, 1, 1, 1, 0,
        0, 1, 0, 1, 0,
        0, 1, 1, 1, 0,
        0, 0, 0, 1, 0,
        0, 1, 1, 1, 0,
    }
};

foreach (var perceptron in perceptrons)
{
    Console.WriteLine(perceptron);
    for (var i = 0; i < examples.Count; i++)
    {
        Console.WriteLine($"{i}-{perceptron.Predict(examples[i]) == 1}");
    }
}


