using System;
using System.Collections.Generic;

namespace TestDataGenerator
{
    public enum StringKind
    {
        Regular,
        Interpolated,
        Verbatim,
        VerbatimInterpolated,
        InterpolatedWithInsertion,
        VerbatimInterpolatedWithInsertion
    }

    public static class StringKindExtensions
    {
        public static string GetCommentPresentation(this StringKind stringKind) => stringKind switch
        {
            StringKind.Regular => "regular",
            StringKind.Interpolated => "interpolated",
            StringKind.Verbatim => "verbatim",
            StringKind.VerbatimInterpolated => "verbatim interpolated",
            StringKind.InterpolatedWithInsertion => "interpolated with insertion",
            StringKind.VerbatimInterpolatedWithInsertion => "verbatim interpolated with insertion",
            _ => throw new ArgumentOutOfRangeException(nameof(stringKind), stringKind, null)
        };

        public static string GetCodeExample(this StringKind stringKind) => stringKind switch
        {
            StringKind.Regular => "\"\"",
            StringKind.Interpolated => "$\"\"",
            StringKind.Verbatim => "@\"\"",
            StringKind.VerbatimInterpolated => "$@\"\"",
            StringKind.InterpolatedWithInsertion => "$\"{x}\"",
            StringKind.VerbatimInterpolatedWithInsertion => "$@\"{x}\"",
            _ => throw new ArgumentOutOfRangeException(nameof(stringKind), stringKind, null)
        };

        public static IEnumerable<string> GetDumpedPsi(this StringKind stringKind)
        {
            switch (stringKind)
            {
                case StringKind.Regular:
                    yield return @"CSharpNonInterpolatedStringLiteralExpression";
                    yield return @"  PsiElement(STRING_LITERAL_REGULAR)('""""')";
                    break;
                case StringKind.Interpolated:
                    yield return @"CSharpInterpolatedStringLiteralExpression";
                    yield return @"  CSharpInterpolatedStringLiteralExpressionPart";
                    yield return @"    PsiElement(INTERPOLATED_STRING_REGULAR)('$""""')";
                    break;
                case StringKind.Verbatim:
                    yield return @"CSharpNonInterpolatedStringLiteralExpression";
                    yield return @"  PsiElement(STRING_LITERAL_VERBATIM)('@""""')";
                    break;
                case StringKind.VerbatimInterpolated:
                    yield return @"CSharpInterpolatedStringLiteralExpression";
                    yield return @"  CSharpInterpolatedStringLiteralExpressionPart";
                    yield return @"    PsiElement(INTERPOLATED_STRING_VERBATIM)('$@""""')";
                    break;
                case StringKind.InterpolatedWithInsertion:
                    yield return @"CSharpInterpolatedStringLiteralExpression";
                    yield return @"  CSharpInterpolatedStringLiteralExpressionPart";
                    yield return @"    PsiElement(INTERPOLATED_STRING_REGULAR_START)('$""{')";
                    yield return @"  PsiElement(IDENTIFIER)('x')";
                    yield return @"  CSharpInterpolatedStringLiteralExpressionPart";
                    yield return @"    PsiElement(INTERPOLATED_STRING_REGULAR_END)('}""')";
                    break;
                case StringKind.VerbatimInterpolatedWithInsertion:
                    yield return @"CSharpInterpolatedStringLiteralExpression";
                    yield return @"  CSharpInterpolatedStringLiteralExpressionPart";
                    yield return @"    PsiElement(INTERPOLATED_STRING_VERBATIM_START)('$@""{')";
                    yield return @"  PsiElement(IDENTIFIER)('x')";
                    yield return @"  CSharpInterpolatedStringLiteralExpressionPart";
                    yield return @"    PsiElement(INTERPOLATED_STRING_VERBATIM_END)('}""')";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringKind), stringKind, null);
            }
        }
    }
}