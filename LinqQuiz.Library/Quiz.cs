using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            // "You can solve the quiz without LINQ but you will have to write many more lines of code."
            // "I like writing code"
            /*List<int> ret = new List<int>();

            if (exclusiveUpperLimit < 1) throw new ArgumentOutOfRangeException();

            for (int i = 1; i < exclusiveUpperLimit; i++)
                if (i % 2 == 0) ret.Add(i);

            return ret.ToArray();*/
            return (from num in Enumerable.Range(1, exclusiveUpperLimit - 1)
                    where (num % 2) == 0
                    select num).ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            // "You can solve the quiz without LINQ but you will have to write many more lines of code."
            // "I like writing code"
            /*if (exclusiveUpperLimit < 1) return new int[] { };

            List<int> ret = new List<int>();

            for (int i=1; i<exclusiveUpperLimit; i++)
            {
                if (Math.Pow(i, 2) > int.MaxValue) throw new OverflowException();
                else {

                    int pow = (int)Math.Pow(i, 2);
                    if (i % 7 == 0) ret.Add(pow);
                }
            }
            ret.Reverse();

            return ret.ToArray();*/
            return (from num in Enumerable.Range(1, (exclusiveUpperLimit > 1) ? exclusiveUpperLimit - 1 : 1)
                    orderby num descending
                    where ((checked(num * num) % 7) == 0)
                    select num * num).ToArray();
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            // "You can solve the quiz without LINQ but you will have to write many more lines of code."
            // "I like writing code"
            // Code is "inspired"

            /*if (families == null) throw new ArgumentNullException();

            List<FamilySummary> familysummary = new List<FamilySummary>();

            for(int i=0; i<families.Count(); i++)
            {
                FamilySummary summary = new FamilySummary();
                summary.FamilyID = families.ElementAt(i).ID;
                if (families.ElementAt(i).Persons.Count()==0)
                {
                    summary.NumberOfFamilyMembers = 0;
                    summary.AverageAge = 0;
                }
                else
                {
                    summary.NumberOfFamilyMembers = families.ElementAt(i).Persons.Count();
                    decimal averageage = 0;
                    for (int j = 0; j < summary.NumberOfFamilyMembers; j++)
                    {
                        averageage += families.ElementAt(i).Persons.ElementAt(j).Age;
                    }
                    averageage /= families.ElementAt(i).Persons.Count();
                    summary.AverageAge = averageage;
                }
                familysummary.Append(summary);
            }

            return familysummary.ToArray();*/

            return families.Select(family => new FamilySummary {
                AverageAge = family.Persons.Count() <= 0 ? 0 : family.Persons.Average(person => person.Age),
                FamilyID = family.ID,
                NumberOfFamilyMembers = family.Persons.Count()
            }).ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            // "You can solve the quiz without LINQ but you will have to write many more lines of code."
            // "I like writing code"
            // Code is "inspired"
            /*text.ToUpper();
            text.ToCharArray();
            string newStr = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]))
                {
                    newStr += text[i];
                }
            }
            Dictionary<char, int> occs = newStr.Distinct().Select(x => new KeyValuePair<char, int>(x, newStr.Count(f => f == x))).ToDictionary(k => k.Key, v => v.Value);
            var expectedResult = new(char letter, int numberOfOccurrences)[occs.Count];
            for (int i = 0; i < occs.Count; i++)
            {
                var element = occs.ElementAt(i);
                expectedResult[i].letter = element.Key;
                expectedResult[i].numberOfOccurrences = element.Value;

            }
            return expectedResult;*/
            return text.Trim().Distinct()
                .Where(f => char.IsLetter(f))
                .Select((char let, int num) => {
                    return (let, text.Count(f => f == let));
                }).ToArray();
        }
    }
}
