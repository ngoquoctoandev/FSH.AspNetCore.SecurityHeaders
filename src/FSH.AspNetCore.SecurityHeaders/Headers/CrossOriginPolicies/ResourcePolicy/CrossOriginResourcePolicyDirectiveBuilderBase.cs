using Microsoft.AspNetCore.Http;

namespace FSH.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.ResourcePolicy;

/// <summary>
///     The Cross Origin Resource Policy directive builder base class
/// </summary>
public abstract class CrossOriginResourcePolicyDirectiveBuilderBase : CrossOriginPolicyDirectiveBuilderBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CrossOriginResourcePolicyDirectiveBuilderBase" /> class.
    /// </summary>
    /// <param name="directive">tfe</param>
    protected CrossOriginResourcePolicyDirectiveBuilderBase(string directive) : base(directive)
    {
    }

    /// <inheritdoc />
    internal abstract override Func<HttpContext, string> CreateBuilder();
}