using System;
using System.Linq.Expressions;

namespace Sombra.Core
{
    internal sealed class ExpressionDelegateVisitor : ExpressionVisitor
    {
        private readonly Func<Expression, Expression> _visitor;
        private readonly bool _recursive;

        public static Expression Visit(Expression exp, Func<Expression, Expression> visitor, bool recursive)
        {
            return new ExpressionDelegateVisitor(visitor, recursive).Visit(exp);
        }

        private ExpressionDelegateVisitor(Func<Expression, Expression> visitor, bool recursive)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _recursive = recursive;
        }

        public override Expression Visit(Expression node)
        {
            if (_recursive)
            {
                return base.Visit(_visitor(node));
            }
            var visited = _visitor(node);
            if (visited == node) return base.Visit(visited);
            return visited;
        }

    }
}
