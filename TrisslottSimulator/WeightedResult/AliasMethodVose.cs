using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.WeightedResult
{
    public class AliasMethodVose
    {
        private readonly Random _random;
        private readonly List<long> _probabilities;
        private readonly List<int> _alias;
        private readonly long _probabilitySum;
        private readonly bool _even;

        public int Probabilities { get; }

        public AliasMethodVose(IEnumerable<int> probabilities)
        {
            if (probabilities == null)
            {
                throw new ArgumentNullException(nameof(probabilities));
            }

            _probabilities = new List<long>();
            _alias = new List<int>();
            _probabilitySum = 0;
            _random = new Random();
            _even = false;

            var tempProbabilities = probabilities.Select(p => (long)p).ToArray();
            Probabilities = tempProbabilities.Length;
            var maxProbability = -1L;
            var minProbability = -1L;
            foreach (var probability in tempProbabilities)
            {
                if (probability < 0)
                {
                    throw new ArgumentException($"Probability must be equal to 0 or higher, was '{probability}'");
                }

                maxProbability = maxProbability < 0 || probability > maxProbability
                    ? probability
                    : maxProbability;
                minProbability = minProbability < 0 || probability < minProbability
                    ? probability
                    : minProbability;

                _probabilitySum += probability;
            }

            _even = maxProbability == minProbability;

            if (_even)
            {
                return;
            }

            for (var i = 0; i < tempProbabilities.Length; i++)
            {
                tempProbabilities[i] *= Probabilities;
                _alias.Add(0);
                _probabilities.Add(0);
            }

            var small = new List<int>();
            var large = new List<int>();
            for (var i = 0; i < tempProbabilities.Length; i++)
            {
                if (tempProbabilities[i] < _probabilitySum)
                {
                    small.Add(i);
                }
                else
                {
                    large.Add(i);
                }
            }

            while (small.Count > 0 && large.Count > 0)
            {
                var l = small[small.Count - 1];
                small.RemoveAt(small.Count - 1);
                var g = large[large.Count - 1];
                large.RemoveAt(large.Count - 1);
                _probabilities[l] = tempProbabilities[l];
                _alias[l] = g;
                var newProbability = tempProbabilities[g] + tempProbabilities[l] - _probabilitySum;
                tempProbabilities[g] = newProbability;

                if (newProbability < _probabilitySum)
                {
                    small.Add(g);
                }
                else
                {
                    large.Add(g);
                }
            }

            foreach (var g in large)
            {
                _probabilities[g] = _probabilitySum;
            }

            foreach (var l in small)
            {
                _probabilities[l] = _probabilitySum;
            }
        }

        public int NextValue()
        {
            var i = _random.Next(Probabilities);
            return _even || _random.Next((int)_probabilitySum) < _probabilities[i]
                ? i
                : _alias[i];
        }
    }
}
