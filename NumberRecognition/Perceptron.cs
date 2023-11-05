using System.Reflection;

namespace NumberRecognition;

public class Perceptron
{
    public char RecognizedSymbol { get; }
    
    private const double LearningConst = 0.1;
    private readonly Random _random;
    private IList<double> _weights = new List<double>();
    private readonly Dictionary<char, IList<IList<int>>> _learningData = new();
    private double _biasWeight;
    private readonly double _threshold;


    public Perceptron(char recognizedSymbol, int imageWidth = 5, int imageHeight = 7)
    {
        _random = new Random();
        RecognizedSymbol = recognizedSymbol;
        _biasWeight = _random.NextDouble() * 2 - 1;
        for (var i = 0; i < (imageHeight * imageWidth); i++)
        {
            var randomWeight = _random.NextDouble() * 2 - 1;
            _weights.Add(randomWeight);
        }

        _threshold = 0;
    }

    public override string ToString()
    {
        return $"-{RecognizedSymbol}-";
    }

    public void Train()
    {
        Console.WriteLine($"Train {RecognizedSymbol}");
        LoadLearningData();

        var i = 0L;
        var maxLifeTime = 0;
        var currentLifeTime = 0;
        var weights = new List<double>();
        var biasFromMaxLifeTime = 0D;
        while (i <= 5_000)
        {
            var learningChar = GetLearningChar();
            var learningIndex = RandomNextIndexOfLearningData(learningChar);
            var learningCase = _learningData[learningChar][learningIndex];

            var target = learningChar == RecognizedSymbol ? 1 : 0;
            var error = GetError(learningCase, target);
            if (error == 0)
            {
                currentLifeTime++;
            }
            else
            {
                if (currentLifeTime > maxLifeTime)
                {
                    maxLifeTime = currentLifeTime;
                    weights = _weights.Select(x => x).ToList();
                    biasFromMaxLifeTime = _biasWeight;
                }


                for (var index = 0; index < _weights.Count; index++)
                {
                    _weights[index] += error * LearningConst * learningCase[index];
                }

                _biasWeight += LearningConst * error;
                currentLifeTime = 0;
            }

            i++;
        }

        if (currentLifeTime > maxLifeTime)
        {
            maxLifeTime = currentLifeTime;
            weights = _weights.Select(x => x).ToList();
            biasFromMaxLifeTime = _biasWeight;
        }

        _weights = weights.Select(x => x).ToList();
        _biasWeight = biasFromMaxLifeTime;

        Console.WriteLine($"Max lifetime for {RecognizedSymbol} is {maxLifeTime}");
        Console.WriteLine("end");
    }

    public int Predict(IList<int> data)
    {
        if (data.Count != _weights.Count)
        {
            throw new ArgumentException();
        }

        double sum = 0;
        for (int i = 0; i < data.Count; i++)
        {
            sum += data[i] * _weights[i];
        }

        sum += _biasWeight;

        return sum >= _threshold ? 1 : 0;
    }

    private int GetError(IList<int> input, int target)
    {
        var guess = Predict(input);
        return target - guess;
    }

    private void LoadLearningData()
    {
        var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
        
        
        var files = Directory.EnumerateFiles($"{path}{Path.DirectorySeparatorChar}Numbers");

        foreach (var file in files)
        {
            var parseImageData = ParseImage(file);
            var learnChar = file.Split("\\").Last()[0];
            if (!_learningData.ContainsKey(learnChar))
            {
                _learningData[learnChar] = new List<IList<int>>();
            }

            _learningData[learnChar].Add(parseImageData);
        }
    }

    private static IList<int> ParseImage(string file)
    {
        var image = Image.Load<Rgba32>(file);
        var parseImageData = new List<int>();
        image.ProcessPixelRows(accessor =>
        {
            for (var y = 0; y < accessor.Height; y++)
            {
                var pixelRow = accessor.GetRowSpan(y);
                foreach (ref var pixel in pixelRow)
                {
                    var color = (pixel.R + pixel.G + pixel.B) / 3;
                    parseImageData.Add(pixel.A > 0 && color < 127 ? 1 : 0);
                }
            }
        });

        return parseImageData;
    }


    private int RandomNextIndexOfLearningData(char learningChar)
    {
        return _random.Next(0, _learningData[learningChar].Count);
    }

    private char GetLearningChar()
    {
        return _learningData.ElementAt(_random.Next(0, _learningData.Count)).Key;
    }
}