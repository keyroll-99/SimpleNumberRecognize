using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberRecognition;

namespace NumberRecognitionViewApp.Data;

public class NumberRecognitionService
{
    private readonly List<Perceptron> _perceptrons;

    public NumberRecognitionService()
    {
        _perceptrons = new List<Perceptron>();
        // 48 - 0 in ASCII 
        // 57 - 9 in ASCII
        for (var i = 48; i <= 57; i++)
        {
            var perceptron = new Perceptron((char)i, 5, 5);
            _perceptrons.Add(perceptron);
        }
    }

    public async Task Train()
    {
        var tasks = _perceptrons.Select(perceptron => Task.Run(perceptron.Train)).ToList();
        await Task.WhenAll(tasks);
    }

    public List<char> Recognize(List<int> input)
    {
        return (
                from perceptron in _perceptrons
                where perceptron.Predict(input) == 1
                select perceptron.RecognizedSymbol)
            .ToList();
    }
}