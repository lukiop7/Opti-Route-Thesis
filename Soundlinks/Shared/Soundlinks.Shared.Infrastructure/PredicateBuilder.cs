using System;
using System.Linq.Expressions;

namespace Soundlinks.Shared.Infrastructure
{
    /// <summary>
    /// Klasa implementująca mechanizm budowania wyrażenia LINQ.
    /// </summary>
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() => f => true;

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> firstExpression,
            bool condition,
            Func<Expression<Func<T, bool>>> secondExpression)
        {
            return condition
                ? firstExpression.And(secondExpression())
                : firstExpression;
        }

        public static Expression<Func<T, bool>> And<T>(
           this Expression<Func<T, bool>> firstExpression,
           Expression<Func<T, bool>> secondExpression)
        {
            var secondBody = secondExpression.Body.Replace(
                secondExpression.Parameters[0],
                firstExpression.Parameters[0]);

            return Expression.Lambda<Func<T, bool>>(
                  Expression.AndAlso(firstExpression.Body, secondBody),
                  firstExpression.Parameters);
        }

        public static Expression Replace(
            this Expression expression,
            Expression searchExpression,
            Expression replaceExpression)
        {
            return new ReplaceVisitor(searchExpression, replaceExpression).Visit(expression);
        }

        internal class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression from;
            private readonly Expression to;

            public ReplaceVisitor(Expression from, Expression to)
            {
                this.from = from;
                this.to = to;
            }

            public override Expression Visit(Expression node)
            {
                return node == from ? to : base.Visit(node);
            }
        }
    }
}