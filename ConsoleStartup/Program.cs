// See https://aka.ms/new-console-template for more information


using NumberRecognition;

// var tmp = new Perceptron('0');


var perceptrons = new List<Perceptron>();

var tasks = new List<Task>();

// 48 - 0 in ASCII 
// 57 - 9 in ASCII
for (var i = 48; i <= 52; i++)
{
    var perceptron = new Perceptron((char)i, 5, 5);
    tasks.Add(Task.Run(() => perceptron.Train()));
    perceptrons.Add(perceptron);
}

await Task.WhenAll(tasks);

var example0 = new List<int>()
{
    0, 0, 1, 1, 1,
    0, 0, 1, 0, 1,
    0, 0, 1, 0, 1,
    0, 0, 1, 0, 1,
    0, 0, 1, 1, 1,
};

var example1 = new List<int>()
{
    0, 0, 0, 0, 1,
    0, 0, 0, 1, 1,
    0, 0, 1, 0, 1,
    0, 0, 0, 0, 1,
    0, 0, 0, 0, 1,
};


var example2 = new List<int>()
{
    0, 0, 1, 1, 1,
    0, 0, 0, 0, 1,
    0, 0, 1, 1, 1,
    0, 0, 1, 0, 0,
    0, 0, 1, 1, 1,
};

var example3 = new List<int>()
{
    0, 0, 1, 1, 1,
    0, 0, 0, 0, 1,
    0, 0, 1, 1, 1,
    0, 0, 0, 0, 1,
    0, 0, 1, 1, 1,
};

var example4 = new List<int>()
{
    0, 0, 0, 0, 1,
    0, 0, 0, 1, 1,
    0, 0, 1, 0, 1,
    0, 1, 1, 1, 1,
    0, 0, 0, 0, 1,
};




Console.WriteLine(perceptrons[0].ToString());
Console.WriteLine("0 " + perceptrons[0].Predict(example0));
Console.WriteLine("1 " + perceptrons[0].Predict(example1));
Console.WriteLine("2 " + perceptrons[0].Predict(example2));
Console.WriteLine("3 " + perceptrons[0].Predict(example3));
Console.WriteLine("4 " + perceptrons[0].Predict(example4));

Console.WriteLine(perceptrons[1].ToString());
Console.WriteLine("0 " + perceptrons[1].Predict(example0));
Console.WriteLine("1 " + perceptrons[1].Predict(example1));
Console.WriteLine("2 " + perceptrons[1].Predict(example2));
Console.WriteLine("3 " + perceptrons[1].Predict(example3));
Console.WriteLine("4 " + perceptrons[1].Predict(example4));

Console.WriteLine(perceptrons[2].ToString());
Console.WriteLine("0 " + perceptrons[2].Predict(example0));
Console.WriteLine("1 " + perceptrons[2].Predict(example1));
Console.WriteLine("2 " + perceptrons[2].Predict(example2));
Console.WriteLine("3 " + perceptrons[2].Predict(example3));
Console.WriteLine("4 " + perceptrons[2].Predict(example4));

Console.WriteLine(perceptrons[3].ToString());
Console.WriteLine("0 " + perceptrons[3].Predict(example0));
Console.WriteLine("1 " + perceptrons[3].Predict(example1));
Console.WriteLine("2 " + perceptrons[3].Predict(example2));
Console.WriteLine("3 " + perceptrons[3].Predict(example3));
Console.WriteLine("1 " + perceptrons[3].Predict(example4));

Console.WriteLine(perceptrons[4].ToString());
Console.WriteLine("0 " + perceptrons[4].Predict(example0));
Console.WriteLine("1 " + perceptrons[4].Predict(example1));
Console.WriteLine("2 " + perceptrons[4].Predict(example2));
Console.WriteLine("3 " + perceptrons[4].Predict(example3));
Console.WriteLine("1 " + perceptrons[4].Predict(example4));
