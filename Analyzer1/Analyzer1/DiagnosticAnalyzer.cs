using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Analyzer1
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer1Analyzer : DiagnosticAnalyzer
    {
        public const int ConstField = 0;
        public const int ConstProp = 0;
        public const int ConstPar = 0;
        public const int ConstNameSp = 0;
        public const int ConstMeth = 0;
        public const int ConstClass = 0;
        public const string DiagnosticId = "Analyzer1";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeClassNode,  SyntaxKind.ClassDeclaration );
            context.RegisterSyntaxNodeAction(AnalyzeVarNode, SyntaxKind.VariableDeclaration);
            //context.RegisterSemanticModelAction(AnalyzeSymbol);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Field);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Namespace);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Property);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Parameter);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var TypeSymbol = context.Symbol;
            if (TypeSymbol is IFieldSymbol)
            {
                if (TypeSymbol.Name.Length > ConstField)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, TypeSymbol.Locations[0]));
                }
            }
            if (TypeSymbol is IPropertySymbol)
            {
                if (TypeSymbol.Name.Length > ConstProp)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, TypeSymbol.Locations[0]));
                }
            }
            if (TypeSymbol is IMethodSymbol)
            {
                if (TypeSymbol.Name.Length > ConstMeth)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, TypeSymbol.Locations[0]));
                }
            }
            if (TypeSymbol is INamespaceSymbol)
            {
                if (TypeSymbol.Name.Length > ConstNameSp)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, TypeSymbol.Locations[0]));
                }
            }
            if (TypeSymbol is IParameterSymbol)
            {
                if (TypeSymbol.Name.Length > ConstPar)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, TypeSymbol.Locations[0]));
                }
            }
            if (TypeSymbol is IParameterSymbol)
            {
                if (TypeSymbol.Name.Length > ConstClass)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, TypeSymbol.Locations[0]));
                }
            }
        }

        private static void AnalyzeClassNode(SyntaxNodeAnalysisContext context)
        {

            var IdName = (ClassDeclarationSyntax)context.Node;
            if (IdName.Identifier.ToString().Length > 5)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, IdName.Identifier.GetLocation()));
            }
        }

        private static void AnalyzeVarNode(SyntaxNodeAnalysisContext context)
        {

            var IdName = (VariableDeclarationSyntax)context.Node;
            foreach (var variable in IdName.Variables)
            {
                if (variable.Identifier.ToString().Length > 6)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, variable.Identifier.GetLocation()));
                }
            }
        }



    }
}
