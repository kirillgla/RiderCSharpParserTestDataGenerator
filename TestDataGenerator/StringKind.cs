using System;

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

        public static string GetDumpedPsi(this StringKind stringKind) => stringKind switch
        {
            StringKind.Regular => @"    CSharpNonInterpolatedStringLiteralExpression
      PsiElement(STRING_LITERAL_REGULAR)('""""')",
            StringKind.Interpolated => @"    CSharpInterpolatedStringLiteralExpression
      CSharpInterpolatedStringLiteralExpressionPart
        PsiElement(INTERPOLATED_STRING_REGULAR)('$""""')",
            StringKind.Verbatim => @"    CSharpNonInterpolatedStringLiteralExpression
      PsiElement(STRING_LITERAL_VERBATIM)('@""""')",
            StringKind.VerbatimInterpolated => @"    CSharpInterpolatedStringLiteralExpression
      CSharpInterpolatedStringLiteralExpressionPart
        PsiElement(INTERPOLATED_STRING_VERBATIM)('$@""""')",
            StringKind.InterpolatedWithInsertion => @"    CSharpInterpolatedStringLiteralExpression
      CSharpInterpolatedStringLiteralExpressionPart
        PsiElement(INTERPOLATED_STRING_REGULAR_START)('$""{')
      PsiElement(IDENTIFIER)('x')
      CSharpInterpolatedStringLiteralExpressionPart
        PsiElement(INTERPOLATED_STRING_REGULAR_END)('}""')",
            StringKind.VerbatimInterpolatedWithInsertion => @"    CSharpInterpolatedStringLiteralExpression
      CSharpInterpolatedStringLiteralExpressionPart
        PsiElement(INTERPOLATED_STRING_VERBATIM_START)('$@""{')
      PsiElement(IDENTIFIER)('x')
      CSharpInterpolatedStringLiteralExpressionPart
        PsiElement(INTERPOLATED_STRING_VERBATIM_END)('}""')",
            _ => throw new ArgumentOutOfRangeException(nameof(stringKind), stringKind, null)
        };
    }
}