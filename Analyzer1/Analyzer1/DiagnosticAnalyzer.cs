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
        public const string DiagnosticId = "Analyzer1";
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeNode,  SyntaxKind.ClassDeclaration );
           // context.RegisterSymbolAction(AnalyzeSymbol, );
              
        }
        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            return;
        }
        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {

            var IdName = (ClassDeclarationSyntax)context.Node;
            // Perform data flow analysis on the local declarartion.
            //var dataFlowAnalysis = context.SemanticModel.AnalyzeDataFlow(IdName);

            // Retrieve the local symbol for each variable in the local declaration
            // and ensure that it is not written outside of the data flow analysis region.
            if (IdName.Identifier.ToString().Length > 5)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, IdName.Identifier.GetLocation()));
            }
        }
                
            
        
    }
}
