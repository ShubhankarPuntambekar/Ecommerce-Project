using CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>>? _criteria;

        protected BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>>criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<T, bool>>? Criteria => _criteria;

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        public bool IsDistinct { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>>? orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDesc(Expression<Func<T, object>>? orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }
    }

    public class BaseSpecification<T,TResult>(Expression<Func<T, bool>> criteria)
        :BaseSpecification<T>(criteria), ISpecification<T,TResult>
    {
        protected BaseSpecification():this(null!)
        {

        }
        public Expression<Func<T,TResult>>? Select { get; private set; }

        protected void AddSelect(Expression<Func<T,TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }

    
}
