using FSH.AspNetCore.SecurityHeaders.Headers.FeaturePolicy;

namespace FSH.AspNetCore.SecurityHeaders.Headers;

/// <summary>
///     Extension methods for adding a <see cref="FeaturePolicyHeader" /> to a <see cref="HeaderPolicyCollection" />
/// </summary>
public static class FeaturePolicyHeaderExtensions
{
    /// <summary>
    ///     Add a Feature-Policy header to all requests
    /// </summary>
    /// <param name="policies">The collection of policies</param>
    /// <param name="configure">Configure the Feature-Policy</param>
    /// <returns>The <see cref="HeaderPolicyCollection" /> for method chaining</returns>
    public static HeaderPolicyCollection AddFeaturePolicy(this HeaderPolicyCollection policies, Action<FeaturePolicyBuilder> configure) => policies.ApplyPolicy(FeaturePolicyHeader.Build(configure));
}