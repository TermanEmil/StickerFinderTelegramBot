using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using FluentValidation.Results;

namespace Utilities.Exceptions
{
    public class ValidationException : Exception
    {
        private readonly IDictionary<string, ICollection<string>> failures;

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            failures = new Dictionary<string, ICollection<string>>();
        }

        public ValidationException(IReadOnlyCollection<ValidationFailure> failures) : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var group in failures.GroupBy(x => x.PropertyName))
                this.failures.Add(group.Key, group.Select(x => x.ErrorMessage).ToImmutableList());
        }

        public ValidationException(string validationFailure) : this()
        {
            failures.Add("", new[] { validationFailure });
        }

        public IImmutableDictionary<string, IEnumerable<string>> Failures => failures.ToImmutableDictionary(
            x => x.Key,
            x => x.Value.AsEnumerable());
    }
}
