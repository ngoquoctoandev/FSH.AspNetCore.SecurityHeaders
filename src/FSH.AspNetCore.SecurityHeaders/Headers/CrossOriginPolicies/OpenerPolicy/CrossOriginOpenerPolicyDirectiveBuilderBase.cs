using Microsoft.AspNetCore.Http;

namespace FSH.AspNetCore.SecurityHeaders.Headers.CrossOriginPolicies.OpenerPolicy;

/// <summary>
///     The Cross Origin Opener Policy directive builder base class
/// </summary>
public abstract class CrossOriginOpenerPolicyDirectiveBuilderBase : CrossOriginPolicyDirectiveBuilderBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CrossOriginOpenerPolicyDirectiveBuilderBase" /> class.
    /// </summary>
    /// <param name="directive">The name of the directive</param>
    protected CrossOriginOpenerPolicyDirectiveBuilderBase(string directive) : base(directive)
    {
    }

    /// <inheritdoc />
    internal abstract override Func<HttpContext, string> CreateBuilder();
}