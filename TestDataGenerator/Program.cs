using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestDataGenerator
{
    internal static class Program
    {
        private const string TestDataLocation =
            @"C:\w\dotnet-products\Rider\Frontend\rider-test-cases\testData\psi\parsing\CSharpDummyParserConcatenationTest";

        private static void Main()
        {
            GenerateTestData();
        }

        private static void GenerateTestData()
        {
            int currentIndex = 0;
            foreach (var leftParameter in Enum.GetValues<StringKind>())
            {
                foreach (var rightParameter in Enum.GetValues<StringKind>())
                {
                    string fileName = $"concatenation of interpolated strings {currentIndex:00}.txt";
                    var contentBuilder = new StringBuilder()
                        .AppendLine("CSharpFile")
                        .AppendLine("  CSharpDummyNode")
                        .AppendLine("    CSharpExpression");
                    leftParameter.GetDumpedPsi().ForEach(line => contentBuilder.Append("      ").AppendLine(line));
                    contentBuilder
                        .AppendLine("      PsiWhiteSpace(' ')")
                        .AppendLine("      PsiElement(PLUS)('+')")
                        .AppendLine("      PsiWhiteSpace(' ')");
                    rightParameter.GetDumpedPsi().ForEach(line => contentBuilder.Append("      ").AppendLine(line));
                    string contents = contentBuilder.ToString();
                    File.WriteAllText($"{TestDataLocation}\\{fileName}", contents);
                    currentIndex += 1;
                }
            }
        }

        private static void GenerateTestSources()
        {
            int currentIndex = 0;
            foreach (var leftParameter in Enum.GetValues<StringKind>())
            {
                foreach (var rightParameter in Enum.GetValues<StringKind>())
                {
                    string fileName = $"concatenation of interpolated strings {currentIndex:00}.cs";
                    string contents = $"{leftParameter.GetCodeExample()} + {rightParameter.GetCodeExample()}";
                    File.WriteAllText($"{TestDataLocation}\\{fileName}", contents);
                    currentIndex += 1;
                }
            }
        }

        private static IEnumerable<string> GenerateMethodNames()
        {
            int currentIndex = 0;
            foreach (var leftParameter in Enum.GetValues<StringKind>())
            {
                foreach (var rightParameter in Enum.GetValues<StringKind>())
                {
                    yield return
                        $"fun `test concatenation of interpolated strings {currentIndex:00}`() = doTest() // " +
                        $"{leftParameter.GetCommentPresentation()} + {rightParameter.GetCommentPresentation()}";
                    currentIndex += 1;
                }
            }
        }
    }
}